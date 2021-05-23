module Engine.World

open Engine.Entity
open Engine.Event

type World() =
  let eventManager = EventManager()
  let entityManager = EntityManager()
  member this.CreateEntity() = entityManager.Mailbox.PostAndReply CreateEntity
  member this.Post<'t> event = eventManager.Post<'t> event
  member this.Listen<'t> handler = eventManager.Listen<'t> handler
  
  member this._EventManager = eventManager
  member this._EntityManager = entityManager
