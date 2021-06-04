module Game.System
open Engine.API
open Engine.Domain
open Engine.Event
open Engine.System
open Game.Component
open Game.Domain
open Game.Game
open Game.Input

type MoveEvent = {oldPos:Position;newPos:Position;entity:Entity} interface iEvent
type OnInputCommand() =
  let w = Game.game.Engine
  let getNewPosition position (direction:Direction) =
    match direction with
    | Up ->     {position with y = position.y - 1}
    | Down ->   {position with y = position.y + 1}
    | Left ->   {position with x = position.x - 1}
    | Right ->  {position with x = position.x + 1}
  let onCommand = fun command ->
    match command with
    | MoveCommand (direction, positionComponent) ->
      let newPos = getNewPosition positionComponent.Data direction
      positionComponent.Update newPos
      w.Post { oldPos = positionComponent.Data
               newPos = newPos
               entity = positionComponent.Owner}
    | _ -> ()
  interface iOnSystem with
    member this.Init() =
      do w.OnCommand onCommand