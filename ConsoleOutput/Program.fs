open System
open System.Diagnostics
open System.Diagnostics.Tracing
open System.Text.Json.Serialization
open Engine
open Domain
open Engine
open Engine.Component
open Engine.Engine
open Engine.Event
open System
open API
type Direction = Up | Down | Left | Right

// components
type Position = {x:int;y:int}
type Graphic = {glyph:char}
type Player = {name:string}
// events
type KeyPressed = {keyPressed:ConsoleKey;entity:Entity}
type TryMove = {tryMoveToPos:Position;entity:Entity}
type Moved = {movedToPos:Position;entity:Entity}
// systems
type RenderSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      let filter = Filter.Filter2<Position, Graphic>
      for pos, render in filter do
        if Debug.debugEnabled = false then
          Console.Clear()
          Console.SetCursorPosition(pos.Data.x, pos.Data.y)
          printf $"{render.Data.glyph}"
        else
          printfn $"Render: {render.Data.glyph} at {pos.Data.x},{pos.Data.y}"
type InputSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      for player, pos, glyph in Filter.Filter3<Player, Position, Graphic> do
        let key = Console.ReadKey(true).Key
        EngineEvent.Post{keyPressed=key;entity=player.Owner}
type OnKeyPressed() =
  interface iListenSystem with
    member this.Init() =
      do EngineEvent.Listen(fun (x:KeyPressed) ->
        let oldPos = x.entity.Get<Position>(x.entity)
        let newPosData =
          match x.keyPressed with
          | ConsoleKey.UpArrow -> {x=oldPos.Data.x;y=oldPos.Data.y-1}
          | ConsoleKey.DownArrow -> {x=oldPos.Data.x;y=oldPos.Data.y+1}
          | ConsoleKey.LeftArrow -> {x=oldPos.Data.x-1;y=oldPos.Data.y}
          | ConsoleKey.RightArrow -> {x=oldPos.Data.x+1;y=oldPos.Data.y}
          | _ -> oldPos.Data
        EngineEvent.Post{ tryMoveToPos=newPosData
                          entity = x.entity }
                          
        )
type CollisionSystem() =
  interface iListenSystem with
    member this.Init() =
      do EngineEvent.Listen(fun (event:TryMove) ->
        match
          Filter.Filter1<Position>
          |> List.map(fun x -> x.Data)
          |> List.contains(event.tryMoveToPos) with
        | true -> printfn "COLLISION!"
        | false ->
          printfn "lugnt, skickar moveevent!"
          event.entity.Add event.tryMoveToPos
          EngineEvent.Post{ movedToPos=event.tryMoveToPos
                            entity = event.entity }
        ()
        
        )
[<EntryPoint>]
let main _ =
  let engine = Engine.Engine()
  Engine.Debug.debugEnabled <- true
  Console.CursorVisible <- false
  
  let player = engine.CreateEntity()
  player.Add{name="Skogix"}
  player.Add{x=3;y=4}
  player.Add{glyph='@'}
  let monster = engine.CreateEntity()
  monster.Add{x=6;y=7}
  monster.Add{glyph='M'}
  
  engine.AddRunSystem(RenderSystem())
  engine.AddRunSystem(InputSystem())
  engine.AddListenSystem(CollisionSystem())
  engine.AddListenSystem(OnKeyPressed())
  engine.Init()
  while true do
    engine.Run()
  0