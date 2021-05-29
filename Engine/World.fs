module Engine.World
type EntityId = int
type GenerationId = int
type Entity = {
  Id: EntityId
  Generation: GenerationId
  Active: bool
}
type Component<'t> = {
  Owner: Entity
  Data: 't
}
type World() =
  let mutable entities: Map<EntityId, Entity> = Map.empty
  member this.CreateEntity() =
    let newEntity: Entity =
      match 
        entities
        |> Map.toSeq
        |> Seq.tryFind(fun (_, e) -> e.Active = false)
        with
      | Some (_, e) -> { Id=e.Id
                         Generation = e.Generation+1
                         Active = true }
      | None -> { Id = entities.Count
                  Generation = 0
                  Active = true }
    entities <- entities.Add (newEntity.Id, newEntity) 
    newEntity
  member this.DestroyEntity entity =
    let newEntity = {entity with Active = false}
    entities <- entities.Add(newEntity.Id, newEntity)
type Entity with
  member this.TryGet<'t>() = ()
  member this.Has<'t>() = true
  member this.Add<'t>(data:'t) = ()
