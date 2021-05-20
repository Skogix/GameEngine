module Engine.Domain

open System
type EntityId = int
type EntityGeneration = int
[<StructuredFormatDisplay("{Id}")>]
type Entity = {
  Id: EntityId
  Generation: EntityGeneration
  Active: bool
}
type Component<'c> = {
  Type: Type
  Owner: Entity
  Data: 'c
}
type Tag = string
type EntityCreated = {entityCreated:Entity}
type EntityDestroyed = {entityDestroyed:Entity}
type ComponentUpdated<'t> = {componentUpdated:Component<'t>}
type ComponentRemoved<'t> = {componentRemoved:Component<'t>}
