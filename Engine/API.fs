module Engine.API

open Engine.Component
open Engine.Domain
open Engine.Engine
open Engine.Event
open EntityManager
open ComponentManager

type Engine with
  member this.CreateEntity() = EntityManager.CreateEntity()
  member this.DestroyEntity e = EntityManager.DestroyEntity e
  member this.AddComponent<'t> (data:'t) entity  = Pool<'t>.Add entity data 
type Entity with
  member this.Destroy() = EntityManager.DestroyEntity this
  member this.Add<'data>(data:'data) = Pool<'data>.Add this data
  member this.Remove<'data>() = Pool<'data>.HardRemove this
  member this.Get<'data> e = Pool<'data>.HardGet e