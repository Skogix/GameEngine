open Engine
open Domain
type TestComponent1 = {x:int}
type SkogixEvent =
  | Huhu of string
  | Wawa of int
  | EntityCreated of Entity
  | Render of string
[<EntryPoint>]
let main _ =
  Engine.Tests.runTests()
  let engine = Engine.Engine()
  engine._eventManager.Listen (fun (x:EntityCreated) ->
    x.entityCreated |> printfn "Entity Created: %A"
    ) 
  engine._eventManager.Listen (fun (x:EntityDestroyed) ->
    x.entityDestroyed |> printfn "Entity Destroyed: %A"
    ) 
  let e1 = engine.CreateEntity()
  let e2 = engine.CreateEntity()
  engine.DestroyEntity e1
  let e3 = engine.CreateEntity()
  
  
  
//  engine._eventManager.AllEvents |> printfn "All: %A"
  0