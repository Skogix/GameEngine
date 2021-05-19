module Engine.Debug

open System
open System.Collections.Generic

type DebugType =
  | Enabled
  | Disabled
  | Combo
let mutable DebugStatus = Disabled
let engineDebug x =
  match DebugStatus with
  | Disabled -> ()
  | Enabled -> 
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
  | Enabled -> debugMessages <- x :: debugMessages
  | Combo -> debugMessages <- x :: debugMessages
