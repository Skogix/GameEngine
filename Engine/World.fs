module Engine.World

open Engine.Component
open Engine.Event
open Engine.Command
open Engine.Entity
open Engine.System
type World() =
  let componentManager = ComponentManager()
  let entityManager = EntityManager()
  let systemManager = SystemManager()
  member this.Init() = ()
  member this._ComponentManager = componentManager
  member this._EntityManager = entityManager
  member this._EventManager = engineEventManager
  member this._CommandManager = engineCommandManager
  member this._SystemManager = systemManager
  