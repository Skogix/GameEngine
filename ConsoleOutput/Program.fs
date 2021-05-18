open System
open System.Diagnostics
open System.Diagnostics.Tracing
open Engine
open Domain
open Engine.Component
open Engine.Engine
open Engine.Event
open System
open API
type Direction = Up | Down | Left | Right

type Position = {x:int;y:int}
type Graphic = {glyph:char}
type Player = {name:string}

type KeyPressed = {keyPressed:ConsoleKey;entity:Entity}
type Moved = {newPos:Position}

type RenderSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      Console.Clear()
      let filter = Filter.Filter2<Position, Graphic>
      for pos, render in filter do
        Console.SetCursorPosition(pos.Data.x, pos.Data.y)
        printf $"{render.Data.glyph}"
type InputSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      for player, pos in Filter.Filter2<Player, Position> do
        let key = Console.ReadKey(true).Key
        EngineEvent.Post{keyPressed=key;entity=player.Owner}
type OnKeyPressed() =
  interface iListenSystem with
    member this.Init() =
      do Debug.enableDebug <- false
      do Console.CursorVisible <- false
      do EngineEvent.Listen(fun (x:KeyPressed) ->
        let pos = x.entity.Get<Position>(x.entity)
        let newPos =
          match x.keyPressed with
          | ConsoleKey.UpArrow -> {pos.Data with y = pos.Data.y-1}
          | ConsoleKey.DownArrow -> {pos.Data with y = pos.Data.y+1}
          | ConsoleKey.LeftArrow -> {pos.Data with x = pos.Data.x-1}
          | ConsoleKey.RightArrow -> {pos.Data with x = pos.Data.x+1}
          | _ -> pos.Data
        x.entity.Add newPos |> ignore
        )
[<EntryPoint>]
let main _ =
  let engine = Engine.Engine()
  Engine.Debug.enableDebug <- true
  
  let player = engine.CreateEntity()
  player.Add{name="Skogix"}
  player.Add{x=3;y=4}
  player.Add{glyph='@'}
  
  
  engine.AddRunSystem(InputSystem())
  engine.AddRunSystem(RenderSystem())
  engine.AddListenSystem(OnKeyPressed())
  engine.Init()
  while true do
    engine.Run()
  0