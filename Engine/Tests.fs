module Engine.Tests
open Expecto

let tests =
  testList "EngineTests" [
    test "World.CreateEntity() returnar entity med Id=0." {
      let w = Engine.World.World()
      let entity = w.CreateEntity()
      Expect.equal 0 entity.Id "EntityId ska vara 0"
    }
  ]


