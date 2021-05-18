module Engine.API

open Engine.Component
open Engine.Domain
open Engine.Engine
open Engine.EventManager
open EntityManager

type Engine with
  member this.CreateEntity() =
    let e = EntityManager.CreateEntity
    EventManager<EntityCreated>.Post{entityCreated=e}
    e
  member this.DestroyEntity e =
    let destroyed = EntityManager.CreateEntity
    EventManager.Post{entityDestroyed=destroyed}
  member this.AddComponent<'t> entity (data:'t) =
    let c = Pool<'t>.Add entity data 
    EventManager.Post{componentUpdated=c}
    c
type Entity with
  member this.Destroy() = EntityManager.DestroyEntity this