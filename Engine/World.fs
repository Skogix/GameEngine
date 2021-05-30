module Engine.World

open Engine.Component
open Engine.Domain
open Engine.Event
open Engine.Entity
type SystemManager() =
  let mutable runSystems = []
  member this.Add system = runSystems <- system::runSystems
  member this._runSystems = runSystems
type World() =
  let componentManager = ComponentManager()
  let entityManager = EntityManager()
  let systemManager = SystemManager()
  member this.Init() = ()
  member this._ComponentManager = componentManager
  member this._EntityManager = entityManager
  member this._EventManager = engineEventManager
  member this._SystemManager = systemManager
  