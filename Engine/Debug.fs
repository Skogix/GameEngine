module Engine.Debug

open System
open Engine.Domain
let printDebug (msg:string) =
  Console.ForegroundColor <- ConsoleColor.Gray
  printfn $"{msg}"
let printMessage msg =
  Console.ForegroundColor <- ConsoleColor.Yellow
  printfn $"{msg}"
let debugHandler<'T> event t =
  if debugEnabled then 
    match t.GetType().Name with
    | "DebugMessage" -> printMessage (t |> string)
    | _ -> printDebug (t |> string)
