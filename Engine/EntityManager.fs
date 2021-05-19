module Engine.EntityManager

open Engine.Domain
open Engine.Event

type EntityManager() =
  static let mutable entities = Map.empty
  static member CreateEntity() =
    let newEntity =
      match
        entities
        |> Map.toSeq
        |> Seq.tryFind(fun (_,y) -> y.Active = false)
        with
      | Some (id, entity) -> { Id = id
                               Generation = entity.Generation+1
                               Active = true}
      | None -> { Id = entities.Count
                  Generation = 0
                  Active = true}
    entities <- entities.Add(newEntity.Id, newEntity) 
    eEvent<EntityCreated>.Post {entityCreated=newEntity}
    newEntity
  static member DestroyEntity entity =
    let destroyedEntity = {entity with Active = false}
    eEvent<EntityDestroyed>.Post {entityDestroyed=destroyedEntity}
    entities.Add(destroyedEntity.Id, destroyedEntity) |> ignore
//type EntityManager() =
//  let mailbox = MailboxProcessor.Start(fun inbox ->
//    let rec loop (entities:Map<EntityId, Entity>) = async {
//      let! message = inbox.Receive()
//      match message with
//      | CreateAndGetEntity rc ->
//        let newEntity =
//          match
//            entities
//            |> Map.toSeq
//            |> Seq.tryFind(fun (_,y) -> y.Active = false)
//            with
//          | Some (id, entity) -> { Id = id
//                                   Generation = entity.Generation+1
//                                   Active = true}
//          | None -> { Id = entities.Count
//                      Generation = 0
//                      Active = true}
//        rc.Reply newEntity
//        return! loop (entities.Add(newEntity.Id, newEntity))
//      | DestroyEntity (entity, rc) ->
//        let destroyedEntity = {entity with Active = false}
//        rc.Reply destroyedEntity
//        return! loop (entities.Add(destroyedEntity.Id, destroyedEntity))
//      return! loop entities
//    }
//    loop Map.empty
//    )
//  member this.CreateEntity() = mailbox.PostAndReply CreateAndGetEntity
//  member this.DestroyEntity e = mailbox.PostAndReply (fun rc -> DestroyEntity (e, rc))
