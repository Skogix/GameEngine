module Engine.Event

open System

type EventStore() =
  let mutable events: (Type * obj) list = []
  member this.Add event = events <- events @ [event.GetType(), event]
  member this.PrintFormattedHistory =
    printfn "Events: "
    events
    |> List.map (fun (_,e) -> sprintf $"\t{e}")
    |> List.iter (printfn "%s")
let eventStore = EventStore()
type EngineEvent() =
  static let event = Event<_>()
  static member Listen(handler) = event.Publish.Add handler
  static member Post e =
    eventStore.Add e
    event.Trigger e