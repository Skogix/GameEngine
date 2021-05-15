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
  let monster = createMonster
  
  debug Pool<Combat>.AllComponents
  
  Thread.Sleep 200
//  e.EventStore.PrintHistory
  0