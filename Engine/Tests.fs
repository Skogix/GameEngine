module Engine.Tests
open Engine.World
open Expecto

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
      e1.Add<TestPositionData>{x=0;y=0}
      let actual = e1.Has<TestPositionData>()
      Expect.equal true actual "huhu"
    }
  ]


