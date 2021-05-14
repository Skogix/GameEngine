module Engine.Entity

open Engine.Domain
open Engine.Event

type EntityManagerCommand =
  | CreateEntity of AsyncReplyChannel<Entity>
  | DestroyEntity of Entity
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
          | Some (e, data) -> e, { Generation = data.Generation+1
                                   Active = true}
          | None -> {Id=entities.Count}, { Generation = 0
                                           Active = true}
        EngineEvent.Post (EntityCreated newEntity)
        rc.Reply newEntity
        return! loop (entities.Add (newEntity, newEntityData))
      | DestroyEntity e ->
        EngineEvent.Post (EntityDestroyed e)
        return! loop (entities.Add(e, {entities.[e] with Active = false}))
      | GetAllActiveEntities rc ->
        rc.Reply (entities |> Map.filter(fun e data -> data.Active = true))
        return! loop entities
      | GetAllInActiveEntities rc ->
        rc.Reply (entities |> Map.filter(fun e data -> data.Active = false))
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
  member this.Agent = agent
