module Engine.EntityManager

open Engine.Domain

type EntityManagerCommands =
  | CreateAndGetEntity of AsyncReplyChannel<Entity>
type EntityManager() =
  let mailbox = MailboxProcessor.Start(fun inbox ->
    let rec loop (entities:Map<EntityId, Entity>) = async {
      let! message = inbox.Receive()
      match message with
      | CreateAndGetEntity rc ->
        let newEntity = {
          Id = entities.Count
          Generation = 0
          Active = true }
        rc.Reply newEntity
        return! loop (entities.Add(newEntity.Id, newEntity))
      return! loop entities
    }
    loop Map.empty
    )
  member this.CreateEntity() = mailbox.PostAndReply CreateAndGetEntity