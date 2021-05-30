module Engine.Component

open Engine.Domain

type ComponentPool<'t>() =
  static let mutable components: Map<Entity, Component<'t>> = Map.empty
  static member UpdateComponent entity component = components <- components.Add(entity, component)
  static member HasComponent  = components.ContainsKey
  static member TryGetComponent = components.TryFind
  static member GetAllComponents() = [for map in components do map.Value]
  static member GetAllComponentsFromEntities(entities:Entity list) = entities |> List.map components.TryFind |> List.map(fun x -> x.Value)
  static member GetAllEntities() = [for map in components do map.Key]
  static member FilterEntities = List.filter(ComponentPool<'t>.HasComponent)
type ComponentManager() =
  let createComponent entity data = {Data = data; Owner = entity}
  member this.AddComponent<'t> entity (data:'t) =
    let newComponent = createComponent entity data
    ComponentPool<'t>.UpdateComponent entity newComponent
  member this.SetComponent<'t> entity (data:'t) =
    let newComponent = createComponent entity data
    ComponentPool<'t>.UpdateComponent entity newComponent
    newComponent
  member this.HasComponent<'t> entity = ComponentPool<'t>.HasComponent entity
  member this.TryGetComponent<'t> entity = ComponentPool<'t>.TryGetComponent entity
