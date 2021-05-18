module Engine.Debug

open System

let mutable enableDebug = true
let debug x =
  if enableDebug then
    Console.ForegroundColor <- ConsoleColor.Gray
    printfn "DEBUG::\n %A" x
    Console.ForegroundColor <- ConsoleColor.Black
