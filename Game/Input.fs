module Game.Input

open Engine
open Engine.Command
open Engine.Domain
open Engine.System
open Game.Component
open Game.Game
open Game.Core
open Engine.API
open Game.Domain

type Input =
  | InputMove of Direction 
  | InputNone
  interface iCommand
type iInputChannel =
  interface
    abstract member Get: unit -> Input
    end
type InputSystem(handler:iInputChannel) =
  interface iRunSystem with
    member this.Run() =
      let filter = Filter.filter2<Player, Position>()
      for _, position in filter do
        match handler.Get() with
        | InputMove dir ->
          game.Engine.Command (Move (dir, position))
        | InputNone -> ()
