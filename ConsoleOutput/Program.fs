open System
open System.Threading
open ConsoleOutput
open Engine
open Engine.API
open Engine.Component
open Engine.Domain
open Scratch
[<EntryPoint>]
let main _ =
  Console.Clear()
  Console.ForegroundColor <- ConsoleColor.Black
  let debug msg = e.Post(DebugMessage (msg |> string))
  
  let skogix = createPlayer
//  let monster = createMonster
//  let combatFilter =  (e.Filter1<Combat>())
//  for combat,entity in combatFilter do
//    debug $"Entity: %A{entity} \n\n\n CombatStats: %A{combat}\n\n\n"
  debug Pool<Tag>.AllComponents
  
//  let e = Engine()
//  
//  let e1 = e.CreateEntity
//  let e2 = e.CreateEntity
//  e1.Set {x=10}
//  e1.Set {y=10}
//  e1.Set {z=10}
//  e2.Set {x=10}
//  e2.Set {y=10}
////  e.Filter3<TestComponent1, TestComponent2, TestComponent3>() |> printfn "es: %A"
//  let es, t1, t2 = Filter.Filter2<TestComponent1, TestComponent2>
//  es |> debug
//  t1 |> debug
//  t2 |> debug
  Thread.Sleep 200
//  e.EventStore.PrintHistory
  0