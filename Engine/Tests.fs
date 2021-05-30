module Engine.Tests
open Engine.API
open Engine.Domain
open Engine.Event
open Expecto
open Expecto.Logging

type TestPositionData = {x:int;y:int}
type TestEventInt = {testEventInt:int} interface iEvent
let eventTests =
  let world = engineWorld
  testList "EventTests" [
    test "Event addas till store" {
      let expected = {testEventInt = 10}
      world.Post expected
      let actual = world._EventManager.GetStore().Head
      Expect.equal (expected :> iEvent) actual ""
    }
  ]
let engineTests =
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
      let expected = createTestPositionComponent e1 testComponentData1
      let actual = e1.TryGet<TestPositionData>()
      Expect.equal (Some expected) actual "returna some component som matchar"
    }
    test "Set component" {
      let e1 = w.CreateEntity()
      e1.Add testComponentData1
      let expectedComponent1 = createTestPositionComponent e1 testComponentData1
      let actualComponent1 = e1.TryGet<TestPositionData>()
      Expect.equal (Some expectedComponent1) actualComponent1 "sanity check"
      let actualReturn = e1.Set testComponentData2
      let expectedComponent2 = createTestPositionComponent e1 testComponentData2
      let actualComponent2 = e1.TryGet<TestPositionData>()
      Expect.equal (Some expectedComponent2) actualComponent2 "ska vara uppdaterad"
      Expect.equal expectedComponent2 actualReturn "ska returna den nya"
    }
  ]


