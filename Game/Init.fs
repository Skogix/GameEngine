module Game.Init

open Game.Component
open Game.Core
open Engine.API
open Game
open Game.Input
open Game.Output

let Init(inputHandler:iInputChannel, outputHandler:iOutputChannel) =
  Engine.Tests.engineTests |> List.iter Engine.Domain.runTest
  
  game.Engine.AddRunSystem(InputSystem(inputHandler))
  game.Engine.AddRunSystem(OutputSystem(outputHandler))
  do game.Engine.OnCommand<GameCommand>(fun x ->
    game.Engine.Post<Output> (PrintGlyph ({x=1;y=1}, 'x'))
    printfn $"Command: %A{x}")
  let e1 = game.Engine.CreateEntity()
  e1.Add{name="Skogix"}
  e1.Add{x=1;y=1}
  while true do
    game.Engine.Run()

