open Engine
open Engine.Domain
open Engine.Event
type TestEvent = string
[<EntryPoint>]
let main _ =
  let w = World.World()
  let e1 = w.Post CreateEntity
  e1.Id |> printfn "%A"
  0