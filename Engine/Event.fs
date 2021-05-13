module Engine.Event
type iEvent = interface end
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
    
type TestEvent = Sträng of string interface iEvent
type TestEvent2 = Test of int interface iEvent
EngineEvent<TestEvent>.Listen (fun x -> printfn "TestEvent1: %A" x)
EngineEvent<TestEvent2>.Listen (fun x -> printfn "TestEvent2: %A" x)
EngineEvent<TestEvent>.Post(Sträng "test")
EngineEvent<TestEvent2>.Post(Test 5)
eventStore.History |> printfn "History: %A"
