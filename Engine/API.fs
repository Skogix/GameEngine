module Engine.API

open Engine.Component
open Engine.Domain
open Engine.Engine
open EntityManager

type Entity with
  member this.Destroy() = EntityManager.DestroyEntity this
  member this.Add<'data>(data:'data) = Pool<'data>.Add this data
  member this.Remove<'data>() = Pool<'data>.HardRemove this
  member this.Get<'data>() = Pool<'data>.HardGet this
  member this.Has<'data>() = Pool<'data>.Has this
  member this.SetName name = this.Name <- name
type Engine with
  member this.CreateEntity(name) =
    let e = EntityManager.CreateEntity()
    e.SetName name
    e
  member this.DestroyEntity e = EntityManager.DestroyEntity e
  member this.AddComponent<'t> (data:'t) entity  = Pool<'t>.Add entity data
type Component<'T> with
  member this.Update<'T>(newData:'T) = Pool<'T>.Add this.Owner newData