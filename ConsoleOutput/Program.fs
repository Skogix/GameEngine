open System
open System.Threading
open Expecto
open Microsoft.Diagnostics.Tracing.Parsers.Clr
[<EntryPoint>]
let main _ =
  Console.Clear()
  
  runTestsWithCLIArgs [] [||] Engine.Tests.tests |> ignore
  0