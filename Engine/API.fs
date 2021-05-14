module Engine.API

open Engine.Component
open Engine.Domain
open Engine.Entity
open Engine.Event
type Engine() =
  let eMan = entityManager
  member this.CreateEntity = eMan.CreateEntity()
  member this.DestroyEntity = eMan.DestroyEntity
  member this.Post (e:'T) = EngineEvent<'T>.Post e
  member this.Listen<'T>(e:'T -> unit) = EngineEvent<'T>.Listen e
  
  
  member this.Run(): unit= ()
//  member this.CreateSystem():iSystem = 0
//  member this.AddSystem(system: iSystem): unit = ()
  // testing
  member this.EntityManager = eMan
  member this.EventStore = eventStore
type Entity with
  member this.TryGet<'C>() =
    Pool<'C>.TryGet this
  member this.Set(c:'T): unit = Pool<'T>.Set this c
  member this.Destroy = entityManager.DestroyEntity this
//  member this.Remove<'C>(): unit = ()
  member this.Data = entityManager.GetData this
type Component<'C> with
  member this.Update (data:'C) = Pool<'C>.Set this.Owner data