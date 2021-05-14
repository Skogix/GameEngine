module Engine.Entity

open Engine.Domain
open Engine.Event

type EntityManagerCommand =
  | CreateEntity of AsyncReplyChannel<Entity>
  | DestroyEntity of Entity
  // testing:
  | GetAllActiveEntities of AsyncReplyChannel<Map<Entity, EntityData>>
  | GetAllInActiveEntities of AsyncReplyChannel<Map<Entity, EntityData>>
  | GetAllEntities of AsyncReplyChannel<Map<Entity, EntityData>>
type EntityManager() =
  let agent = MailboxProcessor.Start(fun inbox ->
    let rec loop (entities:Map<Entity, EntityData>) = async {
      let! command = inbox.Receive()
      match command with
      | CreateEntity rc ->
        let newEntity, newEntityData =
          match
            entities
            |> Map.toSeq
            |> Seq.tryFind(fun (_, y) -> y.Active = false)
            with
          | Some (e, data) -> e, { Entity = e
                                   Generation = data.Generation+1
                                   Active = true}
          | None -> {Id=entities.Count}, { Entity = {Id=entities.Count}
                                           Generation = 0
                                           Active = true}
        EngineEvent.Post EntityCreated newEntityData
        rc.Reply newEntity
        return! loop (entities.Add (newEntity, newEntityData))
      | DestroyEntity e ->
        let newEntityData = {entities.[e] with Active = false}
        EngineEvent.Post EntityDestroyed newEntityData
        return! loop (entities.Add(e, newEntityData))
      | GetAllActiveEntities rc ->
        rc.Reply (entities |> Map.filter(fun _ data -> data.Active = true))
        return! loop entities
      | GetAllInActiveEntities rc ->
        rc.Reply (entities |> Map.filter(fun _ data -> data.Active = false))
        return! loop entities
      | GetAllEntities rc ->
        rc.Reply entities
        return! loop entities
      return! loop entities
    }
    loop Map.empty
    )
  member this.CreateEntity() = agent.PostAndReply CreateEntity
  member this.DestroyEntity e = agent.Post (DestroyEntity e)
  member this.GetAllEntities = agent.PostAndReply GetAllEntities
  member this.GetAllActiveEntities = agent.PostAndReply GetAllActiveEntities
  member this.GetAllInActiveEntities = agent.PostAndReply GetAllInActiveEntities
