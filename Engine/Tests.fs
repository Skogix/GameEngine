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
  let testComponentData1 = {x=0;y=0}
  let testComponentData2 = {x=1;y=1}
  let createTestPositionComponent entity data = {
    Data = data
    Owner = entity
  }
  testList "EngineTests" [
    test "EntityIds och generations" {
      let e1 = w.CreateEntity()
      Expect.equal 0 e1.Id "EntityId 0"
      Expect.equal 0 e1.Generation "GenerationId 0"
      w.DestroyEntity e1
      let e2 = w.CreateEntity()
      Expect.equal 0 e2.Id "EntityId 0"
      Expect.equal 1 e2.Generation "GenerationId 1"
    }
    test "Add och has component" {
      let e1 = w.CreateEntity()
      Expect.equal false (e1.Has<TestPositionData>()) "false"
      e1.Add<TestPositionData> testComponentData1
      Expect.equal true (e1.Has<TestPositionData>()) "true"
    }
    test "TryGet component" {
      let e1 = w.CreateEntity()
      e1.Add<TestPositionData> testComponentData1
      let expected = Some { Data = testComponentData1
                            Owner = e1 }
      let actual = e1.TryGet<TestPositionData>()
      Expect.equal expected actual "returna some component som matchar"
    }
    test "Set component" {
      let e1 = w.CreateEntity()
      e1.Add testComponentData1
      let expectedComponent1 = createTestPositionComponent e1 testComponentData1
      let actualComponent1 = e1.TryGet<TestPositionData>()
      Expect.equal (Some expectedComponent1) actualComponent1 "sanity check"
      let setComponent = e1.Set testComponentData2
      let expectedComponent2 = createTestPositionComponent e1 testComponentData2
      let actualComponent2 = e1.TryGet<TestPositionData>()
      Expect.equal (Some expectedComponent2) actualComponent2 "ska vara uppdaterad"
      Expect.equal expectedComponent2 setComponent "ska returna den nya"
    }
  ]


