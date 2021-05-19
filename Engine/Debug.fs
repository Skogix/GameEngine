module Engine.Debug

open System

type DebugOption =
  | Enabled
  | Disabled
let mutable DebugStatus = Disabled
let debug x =
  if DebugStatus = Enabled then
    Console.ForegroundColor <- ConsoleColor.Gray
    printfn "DEBUG::\n %A" x
    Console.ForegroundColor <- ConsoleColor.Black
