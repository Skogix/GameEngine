module Engine.Entity

open Engine
open Engine.Domain
open Engine.Event

type EntityManagerCommand =
  | CreateEntity of AsyncReplyChannel<Entity>
  | DestroyEntity of Entity
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
            |> Seq.tryFind(fun (x, y) -> y.Active = false) with
          | Some (e, data) -> e, { Generation = data.Generation+1
                                   Active = true}
          | None -> entities.Count, { Generation = 0
                                      Active = true}
        EngineEvent.Post (EntityCreated newEntity)
        rc.Reply newEntity
        return! loop (entities.Add (newEntity, newEntityData))
      | DestroyEntity e ->
        EngineEvent.Post (EntityDestroyed e)
        return! loop (entities.Add(e, {entities.[e] with Active = false}))
      return! loop entities
    }
    loop Map.empty
    )
  member this.CreateEntity = agent.PostAndReply CreateEntity
  member this.Agent = agent
