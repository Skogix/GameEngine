module Engine.API

open Engine.Domain
open Engine.Entity
open Engine.Event
type Engine() =
  let eMan = EntityManager()
  member this.CreateEntity = eMan.CreateEntity()
  member this.DestroyEntity = eMan.DestroyEntity
  member this.Post(e:'a -> 'T) value = EngineEvent<'T>.Post e value
  member this.Listen<'T>(e) = EngineEvent<'T>.Listen e
  
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
  member this.Set(c): unit = ()
  member this.Remove<'C>(): unit = ()
