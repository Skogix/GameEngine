module Engine.Event

open System
open System.Collections.Generic
open System.Diagnostics
open Engine.Domain
type EventId = int
type iEvent = interface end
type iCommand = interface end
type EngineEvents =
  | EntityCreated of Entity
  interface iEvent
type EngineCommands =
  | CreateEntity of AsyncReplyChannel<Entity>
  interface iCommand
type EventListeners<'e>() =
  static let Listeners = List<'e -> unit>()
  static member Post<'e> (event:'e) = for listener in Listeners do listener event
  static member Listen (handler: 'e -> unit) = Listeners.Add handler
type EventPool<'t>() =
  static do EventListeners<'t>.Listen Debug.DebugListener
  static let pool: Map<EventId, 't> = Map.empty
  static member Add<'t> id event = pool.Add (id, event)
  static member _GetAll = pool

type EventStore() = // todo and är code-smell
  let mutable eventCounter = 0
  member this.Add<'e>(event:'e) =
    EventPool<'e>.Add eventCounter event |> ignore
    eventCounter <- eventCounter + 1
type EventManager() =
  let eventStore = EventStore()
  member this.Post<'e>(event:'e) =
    if typedefof<'e>.IsAssignableFrom(typedefof<iCommand>) then eventStore.Add event
    EventListeners<'e>.Post event
  member this.Listen<'e>(handler: 'e -> unit) = EventListeners.Listen handler
  member this._EventStore = eventStore
