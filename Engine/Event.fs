module Engine.Event
type EngineEvent<'T>() =
  static let event = Event<'T>()
  static member Listen<'T>(handler) = event.Publish.Add handler
  static member Post = event.Trigger
type testEvent = string
let testEventListener x = printfn "%s" x
EngineEvent<testEvent>.Listen testEventListener
EngineEvent<testEvent>.Post "test"