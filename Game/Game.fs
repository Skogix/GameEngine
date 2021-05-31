module Game.Game

open Engine.Command
open Engine.Domain
open Game.Component
open Game.Core
open Game.Domain

type GameCommand =
  | Move of Direction * Component<Position>
  interface iCommand
type Game() =
  member this.Engine = Engine.API.engineWorld
let game = Game()
