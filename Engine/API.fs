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
//type Component<'T> =
//  member this.RemoveFromEntity (e:Entity): unit= 0
//  member this.AddToEntity (component:'T) (e:Entity) = 0
//  member this.TryGetFromEntity(e:Entity): 'T option = 0
type Entity with
//  member this.Get<'C>(): 'C option = 0
  member this.Set(c:'T): unit = Pool<'T>.Set this c
  member this.Remove<'C>(): unit = ()
  member this.Data = entityManager.GetData this
