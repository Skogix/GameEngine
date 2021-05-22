module Engine.Domain

open System
open System.Collections.Generic

type EntityId = int
type GenerationId = int
type iEntity =
  interface
    abstract member Init: unit
    end
type Entity = {
  Id: EntityId
  Generation: GenerationId
  Active: bool
}
type EntityUpdatedEvent = Entity
let engineDebug = fun x -> printfn "%A" x
type EventListeners<'event>() =
  static let Listeners = List<'event -> unit>()
  static do Listeners.Add engineDebug
  static member Post<'event> (event:'event) =
    for listener in Listeners do
      listener event
  static member Listen (handler:'event -> unit) =
    Listeners.Add handler
type eEvent<'event> =
  static member Post<'event>(event:'event) =
    EventListeners<'event>.Post event
  static member Listen<'event> handler =
    EventListeners<'event>.Listen handler

type Component<'t> = {
  Data: 't
  Owner: Entity
} with
  member this.Update newData = ComponentPool<'t>.Set this.Owner newData
and ComponentPool<'t>() =
  static let mutable pool: Map<Entity, Component<'t>> = Map.empty
  static let createComponent entity data = { Data = data
                                             Owner = entity }
  static let updatePool entity data =
    let c = createComponent entity data
    let newPool = pool.Add(entity, c)
    pool <- newPool
    eEvent<ComponentUpdated<'t>>.Post c
    c
  static member Set<'t> entity (data:'t) = updatePool entity data
  static member Add<'t> entity (data:'t) = updatePool entity data |> ignore
  static member Pool = pool
and ComponentUpdated<'t> = Component<'t>
type EntityPool() =
  static let mutable pool = Map.empty
  static let updatePool entity =
    let newPool = pool.Add(entity.Id, entity)
    eEvent.Post<EntityUpdatedEvent> entity
    pool <- newPool
  static member Pool = pool
  static member Create() =
    let e = { Id = pool.Count
              Generation = 0
              Active = true
              }
    updatePool e
    e
  
type Entity with
  member this.Set<'t>(data:'t) = ComponentPool<'t>.Set this data
  member this.Add<'t>(data:'t) = ComponentPool<'t>.Add this data
  member this.TryGet<'t>() = ComponentPool<'t>.Pool.TryFind this
  member this.GetData<'t>() = ComponentPool<'t>.Pool.[this].Data
  member this.Get<'t>() = ComponentPool<'t>.Pool.[this]
  member this.HardGet<'t>() = ComponentPool<'t>.Pool.[this]