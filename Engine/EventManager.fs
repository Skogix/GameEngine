module Engine.EventManager
open System.Collections.Generic
open Engine
open EventStore

type EventListeners<'event>() =
  static let Listeners = List<'event -> unit>()
  static member Post<'event> (event:'event) =
    for listener in Listeners do
      listener event
  static member Listen (handler:'event -> unit) =
    Listeners.Add handler
type EventManager() =
  let eventStore = EventStore()
  member this.AllEvents = eventStore.GetAll()
  member this._eventStore = eventStore
  member this.Post(event:'event) =
    eventStore.Append [event]
    EventListeners<'event>.Post event
  member this.Listen<'event> (handler) =
    EventListeners<'event>.Listen handler
