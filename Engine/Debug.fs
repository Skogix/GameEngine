module Engine.Debug

open System
open System.Collections.Generic

type DebugType =
  | Text
  | Disabled
  | Combo
let mutable DebugStatus = Disabled
let engineDebug x =
  match DebugStatus with
  | Disabled -> ()
  | Text -> 
    Console.ForegroundColor <- ConsoleColor.Gray
    printfn "DEBUG::\n %A" x
    Console.ForegroundColor <- ConsoleColor.Black
  | Combo ->
    Console.ForegroundColor <- ConsoleColor.Gray
    printfn "DEBUG::\n %A" x
    Console.ForegroundColor <- ConsoleColor.Black
let mutable debugMessages:string list = []
let debug x =
  match DebugStatus with
  | Disabled -> ()
  | Text -> debugMessages <- x :: debugMessages
  | Combo -> debugMessages <- x :: debugMessages
