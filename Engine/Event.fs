module Engine.Event

open System
open Engine
open Debug
type EventStore() =
  // todo gor om till mailbox for att slippa mutable data
  let mutable events: (Type * obj) list = []
  member this.Add<'T> t event = events <- events @ [t, event]
  member this.PrintHistory =
    printfn "EventStore: "
    events
    |> List.iter (fun (t,e) -> printfn $" {t.Name}\n  {e}" )
let eventStore = EventStore()

type EventManager<'T>() =
  static let event = Event<'T>()
  static member Listen(handler) = event.Publish.Add handler
  static member Post<'T> (e:'T) =
    // fixa debug
    debugHandler<'T> typedefof<'T> e
    eventStore.Add<'T> typedefof<'T> e
    event.Trigger e
  static member Post (a, b) = EventManager<'T>.Post (a b)
