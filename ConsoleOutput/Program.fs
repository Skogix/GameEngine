open System
open Engine.API
open Engine.Domain

[<EntryPoint>]
let main _ =
    Console.Clear()
    Console.ForegroundColor <- ConsoleColor.Black
    let e = Engine()
    let debug msg = e.Post DebugMessage (msg |> string)
//    let e1 = e.CreateEntity
//    let e2 = e.CreateEntity
//    e.DestroyEntity e1
    let e3 = e.CreateEntity
    debug "Te |> test"
    e.EventStore.PrintHistory
    0 