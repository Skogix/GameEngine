module ConsoleOutput.Input

open System
open ConsoleOutput.GameComponents
open ConsoleOutput.GameEvents
open Engine.Domain
open Engine.System
open Engine.Event
open Engine.API

type Direction = Up | Down | Left | Right
//let getPosition current direction =
//
let createMoveCommand (entity:Entity) direction =
  let currentPos = entity.Get<Transform>()
  let newPos =
    match direction with
    | Up -> {x=currentPos.Data.x;y=currentPos.Data.y-1}
    | Down -> {x=currentPos.Data.x;y=currentPos.Data.y+1}
    | Left -> {x=currentPos.Data.x-1;y=currentPos.Data.y}
    | Right -> {x=currentPos.Data.x+1;y=currentPos.Data.y}
  eEvent.Post<MoveCommand>{positionComponent=currentPos;destination=newPos}
type InputSystem() =
  interface iListenSystem with
    member this.Init() = do eEvent.Listen(fun (x:KeyPressedEvent) ->
      match x.keyPressed with
      | ConsoleKey.UpArrow -> createMoveCommand x.entity Up
      | ConsoleKey.DownArrow -> createMoveCommand x.entity Down
      | ConsoleKey.LeftArrow -> createMoveCommand x.entity Left
      | ConsoleKey.RightArrow -> createMoveCommand x.entity Right
      | _ -> ()
      )