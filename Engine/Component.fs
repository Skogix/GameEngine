module Engine.Component

open Engine.Domain
open Engine.Event

type Pool<'T>() =
  static let createComponent e d =
    {
      Type = typedefof<'T>
      Owner = e
      Data = d
    }
  static let mutable pool: Map<Entity, Component<'T>> = Map.empty
  static member Set<'T> entity (data:'T) =
    let c = createComponent entity data
//    EngineEvent.Post<'T> {updatedComponent=c} 95% säker dem här två är samma, oklart
    EngineEvent.Post<ComponentUpdated<'T>> {updatedComponent=c}
    pool <- pool.Add(entity,c)
  static member AllEntities = [for map in pool do map.Key]
  static member AllComponents = [for map in pool do map.Value]
  static member TryGet = pool.TryFind
