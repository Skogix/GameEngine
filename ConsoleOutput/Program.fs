open System
open Engine
open Domain
open Engine.EventManager
open System
open API

type TestComponent1 = {x:int}

type KeyPressed = {keyPressed:ConsoleKey}

type GetInput() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() = EventManager.Post{keyPressed=Console.ReadKey(true).Key}
type OnInput() =
  interface iListenSystem with
    member this.Init() = do EventManager.Listen(fun (x:KeyPressed) -> printfn $"pressed {x.keyPressed}")
[<EntryPoint>]
let main _ =
  let engine = Engine.Engine()
  engine.CreateEntity()
  engine.CreateEntity()
  engine.CreateEntity()
  engine.CreateEntity()
  engine.AddRunSystem(GetInput())
  engine.AddListenSystem(OnInput())
  engine.Init()
  while true do
    engine.Run()
  0