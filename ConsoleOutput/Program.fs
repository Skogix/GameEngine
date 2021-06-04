open System
open Engine
open Game
open Game.Input
open Game.Domain
open Game.Output
      
type inputHandler() =
  interface iInputChannel with
    member this.Get() =
      let keyPressed = Console.ReadKey(true).Key
      match keyPressed with
      | ConsoleKey.UpArrow -> InputMove Up
      | ConsoleKey.DownArrow -> InputMove Down
      | ConsoleKey.LeftArrow -> InputMove Left
      | ConsoleKey.RightArrow -> InputMove Right
      | _ -> InputNone
type outputHandler() =
  interface iOutputChannel with
    member this.Handler(output:Output) =
      match output with
      | PrintGlyph (pos, glyph) -> ()
[<EntryPoint>]
let main _ =
  Console.Clear()
  Game.Init.Init(inputHandler(), outputHandler())
  
  0