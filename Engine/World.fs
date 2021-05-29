module Engine.World

open Engine.Component
open Engine.Domain
open Engine.Entity
type World() =
  let componentManager = ComponentManager()
  let entityManager = EntityManager()
  member this._ComponentManager = componentManager
  member this._EntityManager = entityManager
