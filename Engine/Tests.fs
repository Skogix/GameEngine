module Engine.Tests
open Expecto

let tests =
    test "World.CreateEntity() returnar entity med Id=0." {
      let entity = World.CreateEntity()
      Expect.equal 1 entity.Id "EntityId ska vara 0"
    }
  ]


