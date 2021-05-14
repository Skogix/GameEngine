module Engine.Debug

open System
open Engine.Domain

let debug<'T> event =
  #if DEBUG
  let printDebug msg =
    Console.ForegroundColor <- ConsoleColor.Gray
    printfn msg
  let printMessage msg =
    Console.ForegroundColor <- ConsoleColor.Yellow
    printfn msg
  match (event.GetType() = typedefof<DebugMessage>) with
  | true -> printMessage $"{event}"
  | false -> printDebug $"{typedefof<'T>.Name}\n {event}"
  Console.ForegroundColor <- ConsoleColor.Black
  #endif
