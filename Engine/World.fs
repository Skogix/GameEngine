module Engine.World

open Engine.Entity
open Engine.Event

type World() =
  let entityManager = EntityManager()
  let member 
  member this.Command<'c when 'c :> iCommand> command = eEvent.Post command
  member this.Post<'e when 'e :> iEvent> event = eEvent.Post event
  member this.Listen<'e when 'e :> iEvent> event = eEvent.Listen event
  
  member this._EntityManager = entityManager
