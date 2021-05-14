open Engine
open Engine.Entity

[<EntryPoint>]
let main _ =
    let eMan = EntityManager()
    let entity1 = eMan.CreateEntity
    entity1 |> printfn "Entity1: %A"
    0 