open System
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
type PositionComponent = {x:int;y:int}
type NameComponent = {name:string}
type HealthComponent = {hp:int}
[<EntryPoint>]
let main _ =
  Console.Clear()
  engineTests |> List.iter runTest
  
  let positionData = {x=0;y=0}
  let nameData = {name="Skogix"}
  let healthData = {hp=10}
  let w = API.engineWorld
//  let e1 = w.CreateEntity()
//  let e2 = w.CreateEntity()
//  let e3 = w.CreateEntity()
//  e1.Add positionData
//  e1.Add nameData
//  e1.Add healthData
//  e2.Add positionData
//  e2.Add nameData
//  e2.Add healthData
//  e3.Add positionData
//  e3.Add nameData
//  e3.Add healthData
//  Filter.filter3<HealthComponent, PositionComponent, NameComponent>() |> printfn "%A"
  
//
//  let w = API.engineWorld
//  w.Init()
//  let testEventHandler = fun (x:EventInt) -> printfn $"SkogixEvent: %A{x}"; w.Post {eventString = "huhuh"}
//  w.Listen testEventHandler
//  w.Post<EventInt> {eventInt=10}
//  w.GetStore().Head |> printfn "%A"
  0