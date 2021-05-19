open System
open System.Diagnostics
open ConsoleOutput.Components
open ConsoleOutput.OnSystems
open ConsoleOutput.RunSystems
open Engine.API
open Engine
open Engine.Debug
  
[<EntryPoint>]
let main _ =
  let engine = Engine.Engine()
  Debug.DebugStatus <- Enabled
  Console.CursorVisible <- false
  
  // todo fixa lite extensions, det här ser ut som skit
  createWalls 20 20 engine
  let player2 = createPlayer "Skogix" '@' 100 10 {x=4;y=5} engine
  let monster1 = createMonster 'A' 14 8 {x=8;y=9} engine
  let monster2 = createMonster 'B' 18 12 {x=2;y=4} engine
  
  
  engine.AddRunSystem(RenderSystem())
  engine.AddRunSystem(InputSystem())
  engine.AddListenSystem(OnTryMove())
  engine.AddListenSystem(OnCollision())
  engine.AddListenSystem(OnKeyPressed())
  engine.AddListenSystem(OnAttack())
  engine.AddListenSystem(OnDamageTaken())
  engine.AddListenSystem(OnDeath())
  engine.Init()
  while true do
    engine.Run()
  0