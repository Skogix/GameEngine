module Engine.Tests
open Expecto

let tests =
  testList "EngineTests" [
    ptest "CreateWorld returnar object World." {
      Expect.equal (2+2) 4 "2+2"
    }
    
//    ptest "World.CreateEntity() returnar entity med Id=0." {
//      let entity = World.CreateEntity()
//      Expect.equal 1 entity.Id "EntityId ska vara 0"
//    }
  ]


