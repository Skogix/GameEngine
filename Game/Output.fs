module Game.Output

open Engine
open Engine.Event
open Engine.System
open Game.Component
open Engine.API
open Game.Core
open Game.Game

type Output =
  | PrintGlyph of Position * Glyph
  interface iEvent
type iOutputChannel =
  interface
    abstract member Handler: Output -> unit
    end
type OutputSystem(handler:iOutputChannel) =
  let w = API.engineWorld
  let mutable outputEvents: Output list = []
  do w.OnEvent<Output>(fun x -> outputEvents <- x::outputEvents)
  interface iRunSystem with
    member this.Run() =
      for outputEvent in outputEvents do
        match outputEvent with
        | PrintGlyph (pos, glyph) -> printfn $"Pos: {pos.x},{pos.y} - {glyph}" 