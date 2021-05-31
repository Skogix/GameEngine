module Engine.Tests
open System.Threading
open Engine.API
open Engine.Command
open Engine.Domain
open Engine.Event
open Engine.System
open Expecto

type TestPositionData = {x:int;y:int}
type TestNameData = {TESTNAME:string}
type TestBoolData = {TESTBOOL:bool}
type TestEventInt = {TESTEVENTINT:int} interface iEvent
type TestEventString = {TESTEVENTSTRING:string} interface iEvent
type TestCommand =
  | SendTestEventInt of TestEventInt
  interface iCommand
type TestRunSystem() =
  interface iRunSystem with
    member this.Run() = ()
let filterTests =
  let world = engineWorld
  let testNameComponent = {TESTNAME="Skogix"}
  let testBoolComponent = {TESTBOOL=true}
  let testPositionComponent = {x=0;y=0}
  testSequenced <| testList "FilterTests" [
    test "filters returnar lista med components" {
      let e1 = world.CreateEntity()
      let e2 = world.CreateEntity()
      let e3 = world.CreateEntity()
      e1.Add testNameComponent
      e2.Add testNameComponent
      e3.Add testNameComponent
      
      e1.Add testBoolComponent
      e2.Add testBoolComponent
      
      e1.Add testPositionComponent
      let actualFilter1 = world.Filter1<TestNameData>()
      let actualFilter2 = world.Filter2<TestNameData, TestBoolData>()
      let actualFilter3 = world.Filter3<TestNameData, TestBoolData, TestPositionData>()
      Expect.equal actualFilter1.Length 3 "filter1"
      Expect.equal actualFilter2.Length 2 "filter2"
      Expect.equal actualFilter3.Length 1 "filter3"
      ()
    }
  ]
let commandTests =
  let world = engineWorld
  let testCommand = SendTestEventInt {TESTEVENTINT=10}
  testSequenced <| testList "CommandTests" [
    test "Command addas till store" {
      let expected = testCommand
      world.Command expected
      let actual = world._CommandManager.GetCommandStore().Head
      Expect.equal (expected :> iCommand) actual ""
    }
  ]
let mutable testIntForSystemTests = 0
type TestSystem() =
  interface iRunSystem with
    member this.Run() = testIntForSystemTests <- testIntForSystemTests + 1
let systemTests =
  let world = API.engineWorld
  testSequenced <| testList "SystemTests" [
    test "runsystem increase testint" {
      let testSystem = TestSystem()
      world.AddRunSystem testSystem
      let oldCount = testIntForSystemTests
      let system = world._SystemManager._runSystems.Head
      system.Run()
      let newCount = testIntForSystemTests
      Expect.isGreaterThan newCount oldCount ""
    }
    test "Adda runsystem till world" {
     let oldCount = world._SystemManager._runSystems.Length
     world.AddRunSystem<TestRunSystem>(TestRunSystem())
     Expect.isGreaterThan world._SystemManager._runSystems.Length oldCount ""
    }
  ]
let eventTests =
  let world = engineWorld
  let testEventInt = {TESTEVENTINT=10} 
  let testEventString = {TESTEVENTSTRING="testString"}
  testSequenced <| testList "EventTests" [
    test "Event addas till store" {
      let expected = testEventInt
      world.Post expected
      let actual = world._EventManager.GetEventStore().Head
      Expect.equal (expected :> iEvent) actual ""
    }
    test "onInt post TestEventString" {
      world.OnEvent<TestEventInt> (fun x -> world.Post testEventString)
      world.Post testEventInt
      Thread.Sleep 500
      let latestEvent = world._EventManager.GetEventStore().Head
      Expect.equal (latestEvent) (testEventString :> iEvent) ""
    }
    
  ]
let entityTests =
  let w = engineWorld
  testList "EntityTests" [
    test "EntityIds och generations" {
      let e1 = w.CreateEntity()
      let actualCount1 = w._EntityManager._Entities.Count-1
      Expect.equal e1.Id actualCount1 "EntityId"
      Expect.equal 0 e1.Generation "GenerationId 0"
      w.DestroyEntity e1
      let e2 = w.CreateEntity()
      let actualCount2 = w._EntityManager._Entities.Count-1
      Expect.equal e1.Id actualCount2  "EntityId"
      Expect.equal 1 e2.Generation "GenerationId 1"
    }
  ]
let componentTests =
  let w = engineWorld
  let testComponentData1 = {x=0;y=0}
  let testComponentData2 = {x=1;y=1}
  let createTestPositionComponent entity data = {
    Data = data
    Owner = entity
  }
  testList "EngineTests" [
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
let engineTests =
  [componentTests;entityTests;eventTests;commandTests;filterTests;systemTests]
