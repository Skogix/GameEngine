open Engine
open Engine.Entity
open Engine.Event

[<EntryPoint>]
let main _ =
    let eMan = EntityManager()
    let entity1 = eMan.CreateEntity
    entity1 |> printfn "Entity1: %A"
    Engine.Event.eventStore.History |> printfn "%A"
    0 