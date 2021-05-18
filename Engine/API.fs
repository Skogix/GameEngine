module Engine.API

open Engine.Engine
open Engine.EntityManager

type Engine with
  member this.huhu = 0
//type Engine() =
//  let entityManager = EntityManager()
//  let eventStore = Engine.EventStore.initEventStore()
//  member this.CreateEntity =
//    let e = entityManager.CreateEntity
//    eventStore.Append [Domain.EntityCreated e]
//    e
//  member this.EventStore = eventStore