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
  let event = (a b)
  match (event.GetType() = typedefof<eDebug>) with
  | true -> printMessage $"{b}"
  | false -> printDebug $"{event}"
  Console.ForegroundColor <- ConsoleColor.Black
  #endif
