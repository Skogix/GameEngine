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
  static member Add<'c> entity (data:'c) =
    let c = createComponent entity data
    pool <- pool.Add(entity, c)
    EngineEvent.Post{componentUpdated=c}
    c
  static member HardRemove<'c> entity =
    let componentToBeRemoved = pool.[entity]
    EngineEvent.Post{componentRemoved=componentToBeRemoved}
    pool <- pool.Remove entity
  static member HardGet entity = pool.[entity]