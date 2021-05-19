open System
open System.Diagnostics
open System.Threading
open ConsoleOutput
open ConsoleOutput.Attack
open ConsoleOutput.Input
open ConsoleOutput.Movement
open ConsoleOutput.Output
open Engine.API
open Engine
open Engine.Debug
open GameComponents
  
[<EntryPoint>]
let main _ =
  let engine = Engine.Engine()
  DebugStatus <- Enabled
  Console.CursorVisible <- false
  Console.Clear()
  let player = engine.CreateEntity("Player")
  player.Add{x=3;y=3}
  player.Add{glyph='@'}
  player.Add{health=10}
  player.Add{playerTag="Skogix"}
  player.Add(Sword (10, Slashing))
  let monster = engine.CreateEntity("Monster")
  monster.Add{x=4;y=4}
  monster.Add{glyph='M'}
  monster.Add{health=10}
  monster.Add{monsterTag="Monster"}
  
  
  engine.AddRunSystem(RunRenderer())
  engine.AddRunSystem(RunGetInput())
  engine.AddListenSystem(InputSystem())
  engine.AddListenSystem(OnMoveCommand())
  engine.AddListenSystem(OnAttackCommand())
  
  engine.Init()
  while true do
    engine.Run()
  0