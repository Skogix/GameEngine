module Engine.Debug

open System

let DebugListener event =
  Console.ForegroundColor <- ConsoleColor.Gray
  printfn $"%A{event}"
  Console.ForegroundColor <- ConsoleColor.Black

