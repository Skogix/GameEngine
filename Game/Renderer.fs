module Game.Renderer

open Engine
open Engine.System
open Engine.World
open Game.Component
open Game.Game
open Engine.API
open Game.Output

type Renderer() =
  let w = Game.game
  interface iRunSystem with
    member this.Run() =
      let filter = Filter.filter2<Position, Glyph>()
      for (pos, glyph) in filter do
        w.Engine.Post (PrintGlyph (pos.Data, glyph.Data))
      