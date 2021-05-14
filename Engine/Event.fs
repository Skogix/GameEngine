module Engine.Event

open System
open Engine.Domain

type EventStore() =
  let mutable events: (Type * obj) list = []
  member this.Add event = events <- events @ [event.GetType(), event]
  member this.History = events
let eventStore = EventStore()
type EngineEvent() =
  static let event = Event<_>()
  static member Listen(handler) = event.Publish.Add handler
  static member Post e =
    eventStore.Add e
    event.Trigger e
    ()
