open System
open System.Threading
open ConsoleOutput
open Engine
open Engine.API
open Engine.Component
open Engine.System
open Engine.Domain
open Scratch
[<EntryPoint>]
let main _ =
//  Console.Clear()
  Console.ForegroundColor <- ConsoleColor.Black
  debugEnabled <- false
  let debug msg = e.Post(DebugMessage (msg |> string))
  let skogix = createPlayer
  let monster = createMonster
  
  // add systems
//  e.AddRunSystem (RenderSystem())
  e.AddRunSystem (InputSystem())
  e.AddRunSystem (RenderSystem())
  e.AddListenSystem (MoveSystem())
  
  e.Init()
  e.Run()
  
  while true do
    debug (Filter.Filter2<Position, Glyph>)
    printfn "-------------------------------\n"
    e.Run()
  Thread.Sleep 200
//  e.EventStore.PrintHistory
  0