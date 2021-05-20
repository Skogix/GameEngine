module Game1.Run
open Engine.API
open GameComponents
open Init
open Domain

let Run() =
  Init()
  gameEngine.Run()