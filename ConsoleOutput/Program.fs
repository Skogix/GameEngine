open System
open System.Collections.Generic
open Engine.API
open Engine.Component
open Engine.Component
open Engine.Domain
type TestComponent1 = {x:int}
type TestComponent2 = {x:int}
type TestComponent3 = {y:int}
[<EntryPoint>]
let main _ =
  Console.Clear()
  Console.ForegroundColor <- ConsoleColor.Black
  let e = Engine()
  let debug msg = e.Post<eDebug> (msg |> string)
  e.Listen<TestComponent2>(fun x -> printfn "lyssnar2 med x %A" x.x)
  e.Listen(fun x -> printfn "test3: %A" x.Data.y)
  e.Listen<ComponentUpdated<TestComponent1>>(fun x -> debug $"ComponentUpdated: {x.Data}")
  
  let e1 = e.CreateEntity
  e1.Set<TestComponent1> {x=99}
  e1.Set {x=10}
  e1.Set {y=843}
//  e1.Set {y=99}
//  e.EventStore.PrintHistory
  0