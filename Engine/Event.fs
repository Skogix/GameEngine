module Engine.Event

open System
open System.Diagnostics
open Engine.Domain

type EventStore() =
  let mutable events: (Type * obj) list = []
  member this.Add<'T> t event = events <- events @ [t, event]
  member this.PrintHistory =
    printfn "EventStore: "
    events
    |> List.iter (fun (t,e) -> printfn $" %A{e}" )
let eventStore = EventStore()
type EngineEvent<'T>() =
  static let event = Event<'T>()
  static member Listen(handler) = event.Publish.Add handler
  static member Post a b =
    let e = a b
    Debug.debug a b
    eventStore.Add<'T> typedefof<'T> e
    event.Trigger e