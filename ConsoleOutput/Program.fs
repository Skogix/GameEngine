open System
open System.Threading
open Engine
open Engine.API
open Engine.Domain
type TestComponent1 = {x:int}
type TestComponent3 = {y:int}
[<EntryPoint>]
let main _ =
  Console.Clear()
  Console.ForegroundColor <- ConsoleColor.Black
  let e = Engine()
  let debug msg = e.Post<DebugMessage> ("DEBUGMSG::\n " + (msg |> string))
  e.Listen<TestComponent1>(fun x -> debug $"När bara component postas: {x.x}")
  e.Listen<ComponentUpdated<TestComponent1>>(fun x -> debug $"{x}")
  e.Listen<EntityCreated>(fun (x:EntityCreated) -> debug $"{x}")
  e.Listen<EntityDestroyed>(fun x -> debug $"{x}")
  
  
  let e1 = e.CreateEntity
  e1.Set {x=10}
  e1.Destroy
  
  Thread.Sleep 200
//  e.EventStore.PrintHistory
  0