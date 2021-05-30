namespace Game

open Engine.Event

type Position = {x:int;y:int}
type Glyph = char
type OutputChannel =
  | PrintGlyph of Position * Glyph
  interface iEvent