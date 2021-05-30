open System
open Engine
open Engine.Event
open Engine.Tests
open Engine.API
open Domain
open Expecto
open Game


[<EntryPoint>]
let main _ =
  Console.Clear()
  engineTests |> List.iter runTest
  
  let w = API.engineWorld
  w.Listen<OutputChannel> (fun output ->
    match output with
    | PrintGlyph (pos, glyph) -> printfn $"Pos: {pos.x}, {pos.y} {glyph} ") 
  w.Post (PrintGlyph ({x=1;y=1}, 'X'))
  w.Post (PrintGlyph ({x=1;y=2}, 'Y'))
  
  0