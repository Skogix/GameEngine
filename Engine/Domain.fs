module Engine.Domain

open System
type EntityId = int
type EntityGeneration = int
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
type EntityCreated = {entityCreated:Entity}
type EntityDestroyed = {entityDestroyed:Entity}
type ComponentUpdated<'t> = {componentUpdated:Component<'t>}
