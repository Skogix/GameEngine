module Game.Init

open Game.Component
open Engine.API
open Game
open Game.Input
open Game.Output
open Game.Renderer
open Game.System

let Init(inputHandler:iInputChannel, outputHandler:iOutputChannel) =
  Engine.Tests.engineTests |> List.iter Engine.Domain.runTest
  
  game.Engine.AddRunSystem(InputSystem(inputHandler))
  game.Engine.AddRunSystem(OutputSystem(outputHandler))
  game.Engine.AddRunSystem(Renderer())
  game.Engine.AddOnSystem(OnInputCommand())
  game.Engine.Init()
  let e1 = game.Engine.CreateEntity()
  e1.Add{name="Skogix"}
  e1.Add{x=1;y=1}
  e1.Add{glyph='x'}
  while true do
    game.Engine.Run()

