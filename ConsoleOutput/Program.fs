open System
open System.Threading
open Engine
open Engine.Event
open Engine.Tests
open Engine.API
open Domain
open Expecto
open Microsoft.Diagnostics.Tracing.Parsers.Clr
let outputConsoleTests =
  testList "outputTests" [
    test "huhu" {
      ()
    }
  ]
type skogixEvent = {eventInt:int} interface iEvent
[<EntryPoint>]
let main _ =
  Console.Clear()
  runTest engineTests
  runTest eventTests
  printfn "huhu"
  
  let w = API.engineWorld
  w.Init()
  let testEventHandler = fun (x:skogixEvent) -> printfn "%A" x
  w.Listen<skogixEvent> testEventHandler
  w.Post<skogixEvent> {eventInt=10}
  w.GetStore() |> printfn "%A"
  0