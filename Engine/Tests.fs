module Engine.Tests
open Engine.API
open Engine.Domain
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
  let w = engineWorld
  let testComponentData = {x=0;y=0}
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
      let actualComponent = e1.Add<TestPositionData> testComponentData
      let expectedComponent = { Owner = e1
                                Data = testComponentData }
      Expect.equal expectedComponent actualComponent "Borde vara samma"
      Expect.equal true (e1.Has<TestPositionData>()) "Borde vara true"
    }
    test "TryGet component" {
      let e1 = w.CreateEntity()
      e1.Add<TestPositionData>{x=0;y=0} |> ignore
      let expected = Some { Data = {x=0;y=0}
                            Owner = e1 }
      let actual = e1.TryGet<TestPositionData>()
      Expect.equal expected actual "Borde returna some component som matchar"
    }
  ]


