module Engine.Component

open Engine.Domain

type ComponentPool<'t>() =
  static let mutable components: Map<Entity, 't> = Map.empty
  static member Add entity data = components <- components.Add(entity, data)
  
  