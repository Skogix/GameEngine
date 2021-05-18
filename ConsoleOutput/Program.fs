open System
open System.Diagnostics.Tracing
open Engine
open Engine.EventManager
open Engine.EventStore
open Domain
type TestComponent1 = {x:int}
[<EntryPoint>]
let main _ =
  Engine.Tests.runTests()
  let engine = Engine.Engine()
  engine._eventManager.Listen (fun (x:EntityCreated) ->
    x.entityCreated |> printfn "Lyssnade på entitycreated: %A"
    ) 
  engine._eventManager.Listen (fun (x:ComponentUpdated<TestComponent1>) ->
    x.componentUpdated |> printfn "Lyssnade på componentUpdated: %A"
    ) 
  engine._eventManager.Listen (fun (x:TestComponent1) ->
    x.x |> printfn "Lyssnade på testComponent: %A"
    ) 
    
//  engine._eventManager.Listen<EntityCreated>()
  let e1 = engine.CreateEntity()
  let e2 = engine.CreateEntity()
  let c1 = engine.AddComponent e1 {x=10}
  engine._eventManager.Post{x=10}
//  engine._eventManager.AllEvents |> printfn "All: %A"
  0