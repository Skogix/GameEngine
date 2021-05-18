module Engine.Engine
open Domain
type Engine() =
  let eventManager = EventManager.EventManager()
  let entityManager = EntityManager.EntityManager()
  let componentManager = ComponentManager.ComponentManager()
  member this._eventManager = eventManager
  member this._entityManager = entityManager
  member this._componentManager = componentManager
  member this.CreateEntity() =
    let e = entityManager.CreateEntity()
    eventManager.Post{entityCreated=e}
    e
  member this.DestroyEntity e =
    let destroyed = entityManager.DestroyEntity e
    eventManager.Post{entityDestroyed=destroyed}
  member this.AddComponent<'t> entity (data:'t) =
    let c = componentManager.Add entity data
    eventManager.Post{componentUpdated=c}
    c
