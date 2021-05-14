module Engine.Debug

open System
open Engine.Domain

let debug (a:'B -> 'A) (b:'B)=
  #if DEBUG
  let printDebug msg =
    Console.ForegroundColor <- ConsoleColor.Gray
    printfn msg
  let printMessage msg =
    Console.ForegroundColor <- ConsoleColor.Yellow
    printfn msg
  let e = (a b)
  match (e.GetType() = typedefof<Debug>) with
  | true -> printMessage $"{b}"
  | false -> printDebug $"{e}"
  Console.ForegroundColor <- ConsoleColor.Black
  #endif
