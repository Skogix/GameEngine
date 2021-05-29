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
type ComponentPool<'t>() =
  static let mutable components: Map<Entity, Component<'t>> = Map.empty
  static member Update entity component = components <- components.Add(entity, component)
  static member Has  = components.ContainsKey
  static member TryGet = components.TryFind
type ComponentManager() =
  let createComponent entity data = {Data = data; Owner = entity}
  member this.AddComponent<'t> entity (data:'t) =
    let newComponent = createComponent entity data
    ComponentPool<'t>.Update entity newComponent
    newComponent
  member this.HasComponent<'t> entity = ComponentPool<'t>.Has entity
  member this.TryGet<'t> entity = ComponentPool<'t>.TryGet entity
type World() =
  let componentManager = ComponentManager()
  let mutable entities: Map<EntityId, Entity> = Map.empty
  member this.ComponentManager = componentManager
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
module API =
  let engineWorld = World()
type Entity with
  member this.TryGet<'t>() = API.engineWorld.ComponentManager.TryGet<'t> this
  member this.Has<'t>() = API.engineWorld.ComponentManager.HasComponent<'t> this
  member this.Add<'t>(data:'t) = API.engineWorld.ComponentManager.AddComponent<'t> this data
