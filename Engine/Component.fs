module Engine.Component
open Engine.Event
open Domain
type Pool<'c>() =
  static let mutable pool: Map<Entity, Component<'c>> = Map.empty
  static let createComponent entity data =
    {
      Type = typedefof<'c>
      Owner = entity
      Data = data
    }
  static member AllEntities = [for x in pool do x.Key]
  static member AllComponents = [for x in pool do x.Value]
  static member ContainsEntity = pool.ContainsKey
  static member AddAndGetComponent<'c> entity (data:'c) =
    let c = createComponent entity data
    pool <- pool.Add(entity, c)
    eEvent.Post{componentUpdated=c}
    c
  static member Add<'c> entity (data:'c) =
    let c = createComponent entity data
    pool <- pool.Add(entity, c)
    eEvent.Post{componentUpdated=c}
  static member AddTag tag entity =
    let c = createComponent entity tag
    pool <- pool.Add(entity, c)
  static member HardRemove<'c> entity =
    let componentToBeRemoved = pool.[entity]
    eEvent.Post{componentRemoved=componentToBeRemoved}
    pool <- pool.Remove entity
  static member HardGet entity = pool.[entity]
  static member TryGet entity = pool.TryFind entity
  static member Has = pool.ContainsKey