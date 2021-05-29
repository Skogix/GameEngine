module Engine.Component

open Engine.Domain

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
