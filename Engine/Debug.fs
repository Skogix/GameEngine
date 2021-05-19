module Engine.Debug

open System

let mutable debugEnabled = true
let debug x =
  if debugEnabled then
    Console.ForegroundColor <- ConsoleColor.Gray
    printfn "DEBUG::\n %A" x
    Console.ForegroundColor <- ConsoleColor.Black
