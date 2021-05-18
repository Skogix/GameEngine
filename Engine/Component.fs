module Engine.Component
open Domain
open Engine.EventStore
type Pool<'c>() =
  static let mutable pool: Map<Entity, Component<'c>> = Map.empty
  static let createComponent entity data =
    {
      Type = typedefof<'c>
      Owner = entity
      Data = data
    }
  static member Add<'c> entity (data:'c) =
    let c = createComponent entity data
    pool <- pool.Add(entity, c)
    c