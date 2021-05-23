open Engine
open Engine.Domain
open Engine.Event
type TestEvent = string
[<EntryPoint>]
let main _ =
  let w = World.World()
  let e1 = w.CreateEntity()
  let e2 = w.CreateEntity()
  e1.Id |> printfn "%A"
  e2.Id |> printfn "%A"
  EventPool<EngineCommands>._GetAll |> printfn "%A"
  0