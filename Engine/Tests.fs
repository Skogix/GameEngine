module Engine.Tests
open Expecto

let tests =
  let w = Engine.World.World()
  testList "EngineTests" [
    test "CreateEntity() returnar entity med Id=0." {
      let e = w.CreateEntity()
      Expect.equal 0 e.Id "EntityId ska vara 0"
    }
    test "CreateEntity efter DestroyEntity ska byta generation med använda samma id." {
      let e1 = w.CreateEntity()
      Expect.equal 0 e1.Id "EntityId ska vara 0"
      Expect.equal 0 e1.Generation "GenerationId ska vara 0"
      w.DestroyEntity e1
      let e2 = w.CreateEntity()
      Expect.equal 0 e1.Id "EntityId ska vara 0"
      Expect.equal 1 e1.Generation "GenerationId ska vara 1"
    }
    
  ]


