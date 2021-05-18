module Engine.ComponentManager

open Engine.Component
open Engine.Domain

type ComponentManager() =
  member this.Add<'data> (entity:Entity) (data:'data) = Pool<'data>.Add entity data