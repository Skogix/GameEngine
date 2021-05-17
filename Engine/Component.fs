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
  static member AddTag<'T> entity (data:'T) =
    let c = createComponent entity data
    EventManager.Post<ComponentUpdated<'T>> {updatedComponent=c}
    pool <- pool.Add(entity,c)
    entity
  static member Add<'T> entity (data:'T) =
    let c = createComponent entity data
    EventManager.Post<ComponentUpdated<'T>> {updatedComponent=c}
    pool <- pool.Add(entity,c)
  static member AddAndGet<'T> entity (data:'T) =
    let c = createComponent entity data
    Pool<'T>.Add<'T> entity data // todo ser inte rätt ut, skapar dubbel?
    c
  static member Update<'T> entity (data:'T) =
    let currentComponent = Pool<'T>.Get entity
    let newData = {currentComponent with Data=data}
    pool <- pool.Add(entity, newData)
    ()
  static member AllEntities = [for map in pool do map.Key]
  static member AllComponents = [for map in pool do map.Value]
  static member TryGet = pool.TryFind
  static member Get e = pool.[e]
  static member ContainsEntity = pool.ContainsKey
