module Engine.Tests
open Engine.World
open Expecto
open Expecto.Logging

//let inline repeat10 f a =
//  let mutable v = f a
//  v <- f a
//  v <- f a
//  v <- f a
//  v <- f a
//  v <- f a
//  v <- f a
//  v <- f a
//  v <- f a
//  v <- f a
//  v
//let inline repeat100 f a = repeat10 (repeat10 f) a
//let inline repeat1000 f a = repeat10 (repeat100 f) a
//let inline repeat10000 f a = repeat10 (repeat1000 f) a
//
let logger = Log.create "Sample"
//
type TestPositionData = {x:int;y:int}
let tests =
  let w = Engine.World.World()
  testList "EngineTests" [
    test "EntityIds och generations" {
      let e1 = w.CreateEntity()
      Expect.equal 0 e1.Id "EntityId ska vara 0"
      Expect.equal 0 e1.Generation "GenerationId ska vara 0"
      w.DestroyEntity e1
      let e2 = w.CreateEntity()
      Expect.equal 0 e2.Id "EntityId ska vara 0"
      Expect.equal 1 e2.Generation "GenerationId ska vara 1"
    }
    test "Add och has component" {
      let e1 = w.CreateEntity()
      Expect.equal false (e1.Has<TestPositionData>()) "Borde vara false"
      
      e1.Add<TestPositionData>{x=0;y=0}
      Expect.equal true (e1.Has<TestPositionData>()) "Borde vara true"
    }
  ]


