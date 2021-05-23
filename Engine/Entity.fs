module Engine.Entity
open Domain
open Engine.Event

type EntityManager(world:World) =
  let mailbox = MailboxProcessor.Start(fun inbox ->
    let rec loop (state:Map<EntityId, Entity>) = async {
      let! message = inbox.Receive()
      match message with
      | Event.CreateEntity rc ->
        let newEntity = {Id=state.Count}
        rc.Reply newEntity
        return! loop (state.Add(newEntity.Id, newEntity))
      return! loop state
    }
    loop Map.empty
    )
  member this.Mailbox = mailbox