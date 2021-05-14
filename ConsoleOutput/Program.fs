open Engine.Entity
open Engine.Event

[<EntryPoint>]
let main _ =
    let eMan = EntityManager()
    let e1 = eMan.CreateEntity()
    let e2 = eMan.CreateEntity()
    eMan.DestroyEntity e1
    eMan.GetAllActiveEntities |> printfn "Active: %A"
    eMan.GetAllInActiveEntities |> printfn "Inactive: %A"
    let e3 = eMan.CreateEntity()
    eMan.GetAllActiveEntities |> printfn "AllEntities: %A"
    eventStore.PrintFormattedHistory
    0 