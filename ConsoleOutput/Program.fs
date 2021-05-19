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
  createWalls 50 20 engine
  let player1 = createPlayer "Skogix" '@' 100 10 {x=8;y=8} engine
//  let player2 = createPlayer "Skogix" 'B' 100 10 {x=8;y=8} engine
  for pos in [3..5] do
    createMonster 'M' 18 12 {x=pos;y=pos} engine
  
  
  engine.AddRunSystem(RenderSystem())
  engine.AddRunSystem(InputSystem())
  engine.AddRunSystem(AiSystem())
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