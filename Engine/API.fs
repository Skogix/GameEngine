module Engine.API

open System
open Engine.Component
open Engine.Domain
open Engine.Entity
open Engine.Event
type Tag = {Tag:Type}
type Engine() =
  let eMan = entityManager
  let mutable runSystems: iRunSystem list = []
  let mutable listenSystems: iListenSystem list = []
  member this.CreateEntity = eMan.CreateEntity()
  member this.DestroyEntity = eMan.DestroyEntity
  member this.Post (e:'T) = EngineEvent<'T>.Post e
  member this.Listen<'T>(e:'T -> unit) = EngineEvent<'T>.Listen e
  member this.Filter1<'A>() = Filter.Filter1<'A>
  member this.Filter2<'A, 'B >() = Filter.Filter2<'A, 'B>
  member this.Filter3<'A, 'B, 'C >() = Filter.Filter3<'A, 'B, 'C>
  member this.AddComponent<'C> data entity = Pool<'C>.Add entity data; entity
  member this.AddTag<'T> (data:'A) entity = Pool<'A>.Add entity data
  member this.AddListenSystem s = listenSystems <- s :: listenSystems
  member this.AddRunSystem s = runSystems <- s :: runSystems
  member this.Init() =
    runSystems |> List.iter (fun x -> x.Init())
    listenSystems |> List.iter (fun x -> x.Init())
  member this.Run()= runSystems |> List.iter (fun x -> x.Run())
  // testing
  member this.EntityManager = eMan
  member this.EventStore = eventStore
type Entity with
  member this.TryGet<'C>() = Pool<'C>.TryGet this
  member this.Get<'C>() = Pool<'C>.Get this
  member this.Update(c:'T) = Pool<'T>.Update this c
  member this.Add(c:'T) = Pool<'T>.Add this c
  member this.AddAndGet(c:'T) = Pool<'T>.AddAndGet this c
  member this.Destroy = entityManager.DestroyEntity this
//  member this.Remove<'C>(): unit = ()
  member this.Data = entityManager.GetData this
type Component<'C> with
  member this.Update (data:'C) = Pool<'C>.Update this.Owner data