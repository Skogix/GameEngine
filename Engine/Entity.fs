module Engine.Entity

open Engine.Domain
open Engine.Event

type EntityManagerCommand =
  | CreateEntity of AsyncReplyChannel<Entity>
  | DestroyEntity of Entity
  | GetData of Entity * AsyncReplyChannel<Entity>
  // testing:
  | GetAllActiveEntities of AsyncReplyChannel<Map<EntityId, Entity>>
  | GetAllInActiveEntities of AsyncReplyChannel<Map<EntityId, Entity>>
  | GetAllEntities of AsyncReplyChannel<Map<EntityId, Entity>>
type EntityManager() =
  let agent = MailboxProcessor.Start(fun inbox ->
    let rec loop (entities:Map<EntityId, Entity>) = async {
      let! command = inbox.Receive()
      match command with
      | CreateEntity rc ->
        let newEntityId, newEntity =
          match
            entities
            |> Map.toSeq
            |> Seq.tryFind(fun (_, y) -> y.Active = false)
            with
          | Some (id, entity) -> id, { Id = id
                                       Generation = entity.Generation+1
                                       Active = true }
          | None -> entities.Count, { Id = entities.Count
                                      Generation = 0
                                      Active = true }
        EngineEvent.Post{createdEntity=newEntity}
        rc.Reply newEntity
        return! loop (entities.Add (newEntityId, newEntity))
      | DestroyEntity e ->
        let newEntity = {entities.[e.Id] with Active = false}
        EngineEvent.Post{destroyedEntity=newEntity}
        return! loop (entities.Add(e.Id, newEntity))
      | GetAllActiveEntities rc ->
        rc.Reply (entities |> Map.filter(fun _ entity -> entity.Active = true))
        return! loop entities
      | GetAllInActiveEntities rc ->
        rc.Reply (entities |> Map.filter(fun _ entity -> entity.Active = false))
        return! loop entities
      | GetAllEntities rc ->
        rc.Reply entities
        return! loop entities
      | GetData (e, rc) ->
        rc.Reply entities.[e.Id]
      return! loop entities
    }
    loop Map.empty
    )
  member this.CreateEntity() = agent.PostAndReply CreateEntity
  member this.DestroyEntity e = agent.Post (DestroyEntity e)
  member this.GetAllEntities = agent.PostAndReply GetAllEntities
  member this.GetAllActiveEntities = agent.PostAndReply GetAllActiveEntities
  member this.GetAllInActiveEntities = agent.PostAndReply GetAllInActiveEntities
  member this.GetData e = agent.PostAndReply (fun rc -> GetData (e, rc))
let entityManager = EntityManager()