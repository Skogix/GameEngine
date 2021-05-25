module Engine.Entity
open System.Runtime.InteropServices
open Domain
open Engine.Event

type EntityManager() =
  let mailbox = MailboxProcessor.Start(fun inbox ->
    let rec loop (state:Map<EntityId, Entity>) = async {
      let! message = inbox.Receive()
      match message with
      | CreateEntity ->
        let newEntity = {Id=state.Count}
        return! loop (state.Add(newEntity.Id, newEntity))
      return! loop state
    }
    loop Map.empty
    )
  do EventListeners.Listen (fun (x:EntityCommand) ->
    match x with
    | CreateEntity -> mailbox.Post CreateEntity
    )
