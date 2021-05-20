module Engine.API

open Engine.Component
open Engine.Domain
open Engine.Engine
open EntityManager

type Entity with
  member this.Destroy() = EntityManager.DestroyEntity this
  member this.AddAndGetComponent<'data>(data:'data) = Pool<'data>.AddAndGetComponent this data
  member this.Add<'data>(data:'data) = Pool<'data>.Add this data
  member this.Remove<'data>() = Pool<'data>.HardRemove this
  member this.Get<'data>() = Pool<'data>.HardGet this
  member this.TryGet<'data>() = Pool<'data>.TryGet this
  member this.Has<'data>() = Pool<'data>.Has this
type Engine with
  member this.CreateEntity =
    let e = EntityManager.CreateEntity()
    e
  member this.DestroyEntity e = EntityManager.DestroyEntity e
  member this.AddTag (tag:string) =
    Pool<Tag>.AddTag tag
type Component<'T> with
  member this.Update<'T>(newData:'T) = Pool<'T>.Add this.Owner newData