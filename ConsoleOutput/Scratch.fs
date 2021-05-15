module ConsoleOutput.Scratch

open System
open System.Threading
open Engine.API
open Engine.Domain
open Engine.System

let e = Engine()
type InputChar = char
type Health = {HealthAmount: int}
type Attack = {AttackAmount: int}
type Defense = {DefenseAmount: int}
type Glyph = {Glyph: char}
type Description = {
  Name: string
  Description: string
}
type Position = {
  x: int
  y: int
}
// todo borde vara ett snyggare sätt att losa?
// annars så borde det här vara ett bra ställe att DIa måsten?
type Combat = unit
type Renderable = unit
type Player = unit
let addCombat health attack defense entity =
  entity
  |> e.AddComponent{HealthAmount=health}
  |> e.AddComponent{AttackAmount=attack}
  |> e.AddComponent{DefenseAmount=defense}
  |> e.AddComponent<Combat>()
let addRenderable (x,y) glyph entity =
  entity
  |> e.AddComponent{Glyph=glyph}
  |> e.AddComponent{x=x; y=y}
  |> e.AddComponent<Renderable>()
let createPlayer =
  e.CreateEntity
  |> addCombat 100 10 3
  |> addRenderable (1,1) 'P'
  |> e.AddComponent<Player>()
let createMonster =
  e.CreateEntity
  |> addCombat 30 4 1
  |> addRenderable (3,3) 'M'
type TryMove = {
  From: Entity
  To: Position
}
type MoveSystem() =
  let handler (event:TryMove) =
    // kolla vad är på pos men just nu bara updatepos
    event.From.Data.Update<Position>{x=event.To.x;y=event.To.y}
  interface iListenSystem with
    member this.Init() =
      do e.Listen<TryMove> handler
type RenderSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      Console.Clear()
      for pos, glyph, entity in e.Filter2<Position, Glyph>() do
        Console.SetCursorPosition(pos.Data.x, pos.Data.y)
        printf $"{glyph.Data.Glyph}"
type InputSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      let input = Console.ReadKey(true).Key
      let filter = e.Filter2<Player, Position>()
      for player, pos, entity in filter do
        let playerPos = pos.Data
        match input with
        | ConsoleKey.UpArrow -> e.Post<TryMove>{From=entity;To={x=playerPos.x;y=playerPos.y-1}}
        | ConsoleKey.DownArrow -> e.Post<TryMove>{From=entity;To={x=playerPos.x;y=playerPos.y+1}}
        | ConsoleKey.LeftArrow -> e.Post<TryMove>{From=entity;To={x=playerPos.x-1;y=playerPos.y}}
        | ConsoleKey.RightArrow -> e.Post<TryMove>{From=entity;To={x=playerPos.x+1;y=playerPos.y}}
        | _ -> ()