open System
open ConsoleOutput.Components
open ConsoleOutput.OnAttack
open ConsoleOutput.OnCollision
open ConsoleOutput.OnInput
open ConsoleOutput.RunSystems
open Engine
open API
type Direction = Up | Down | Left | Right

  
[<EntryPoint>]
let main _ =
  let engine = Engine.Engine()
  Debug.debugEnabled <- true
  Console.CursorVisible <- false
  
  let player = engine.CreateEntity()
  player.Add{name="Skogix"}
  player.Add{x=3;y=4}
  player.Add{glyph='@'}
  player.Add{health=10}
  player.Add{attack=2}
  let monster = engine.CreateEntity()
  monster.Add{x=6;y=7}
  monster.Add{glyph='M'}
  monster.Add{health=4}
  monster.Add{attack=1}
  
  engine.AddRunSystem(RenderSystem())
  engine.AddRunSystem(InputSystem())
  engine.AddListenSystem(CheckCollisionSystem())
  engine.AddListenSystem(OnCollision())
  engine.AddListenSystem(OnKeyPressed())
  engine.AddListenSystem(OnAttack())
  engine.AddListenSystem(OnDamageTaken())
  engine.Init()
  while true do
    engine.Run()
    printfn "--------------------------------------------"
  0