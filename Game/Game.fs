module Game.Game

open Engine.Command
open Engine.Domain
open Game.Component
open Game.Domain

type GameCommand =
  | MoveCommand of Direction * Component<Position>
  interface iCommand
type Game() =
  member this.Engine = Engine.API.engineWorld
let game = Game()
