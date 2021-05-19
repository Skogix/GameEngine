open System
open System.Diagnostics
open System.Threading
open ConsoleOutput.Components
open ConsoleOutput.OnSystems
open ConsoleOutput.RunSystems
open Engine.API
open Engine
open Engine.Debug
  
[<EntryPoint>]
let main _ =
  let engine = Engine.Engine()
  let random = System.Random()
  DebugStatus <- Combo
  Console.CursorVisible <- false
  Console.Clear()
  
  // todo fixa lite extensions, det här ser ut som skit
  createWalls 50 20 engine
  let player1 = createPlayer "Skogix" '@' 100 10 {x=8;y=8} engine
//  let monster1 = createMonster "Monster" 'M' 18 12 {x=2;y=2} engine
  for n in [2..15] do
    createMonster
      $"Monster#{n}"
      'M'
      (random.Next(10,50))
      (random.Next(1,5))
      {x=(random.Next(2,18));y=(random.Next(2,18))}
      engine |> ignore
  
  
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