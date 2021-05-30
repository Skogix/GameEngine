module Engine.World

open Engine.Component
open Engine.Domain
open Engine.Event
open Engine.Entity
type World() =
  let componentManager = ComponentManager()
  let entityManager = EntityManager()
  member this.Init() = ()
  member this._ComponentManager = componentManager
  member this._EntityManager = entityManager
  member this._EventManager = engineEventManager
  