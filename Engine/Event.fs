module Engine.Event

open Engine.Domain

type EventStore() =
  let mutable events: (string * iEvent) list = []
  member this.Add event = events <- events @ [event.GetType().Name, event]
  member this.History = events
let eventStore = EventStore()
type EngineEvent<'T when 'T :> iEvent>() =
  static let event = Event<'T>()
  static member Listen<'T>(handler) = event.Publish.Add handler
  static member Post e =
    eventStore.Add e
    event.Trigger e
    ()
