open System
open Engine.API
open Engine.Domain

[<EntryPoint>]
let main _ =
    Console.Clear()
    Console.ForegroundColor <- ConsoleColor.Black
    let e = Engine()
    let debug msg = e.Post DebugMessage (msg |> string)
    
    
    
    e.EventStore.PrintHistory
    0 