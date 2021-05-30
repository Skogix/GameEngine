open System
open System.Threading
open Engine
open Engine.Event
open Engine.Tests
open Engine.API
open Domain
open Expecto
open Microsoft.Diagnostics.Tracing.Parsers.Clr
let OutputConsoleTests =
  testList "outputTests" [
    test "huhu" {
      ()
    }
  ]
type EventInt = {eventInt:int} interface iEvent
type EventString = {eventString:string} interface iEvent
[<EntryPoint>]
let main _ =
  Console.Clear()
  runTest engineTests
  runTest eventTests
  runTest commandTests
  runTest OutputConsoleTests
//
//  let w = API.engineWorld
//  w.Init()
//  let testEventHandler = fun (x:EventInt) -> printfn $"SkogixEvent: %A{x}"; w.Post {eventString = "huhuh"}
//  w.Listen testEventHandler
//  w.Post<EventInt> {eventInt=10}
//  w.GetStore().Head |> printfn "%A"
  0