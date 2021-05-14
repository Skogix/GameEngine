module Engine.Debug

open System
open Engine.Domain

let debug<'T, 'E> (t:'T) (e:'E) =
  #if DEBUG
  let printDebug msg =
    Console.ForegroundColor <- ConsoleColor.Gray
    printfn msg
  let printMessage msg =
    Console.ForegroundColor <- ConsoleColor.Yellow
    printfn msg
  match (e.GetType() = typedefof<eDebug>) with
  | true -> printMessage $"{e}"
  | false -> printDebug $"{t}:{e}"
  Console.ForegroundColor <- ConsoleColor.Black
  #endif
