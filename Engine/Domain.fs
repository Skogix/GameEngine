module Engine.Domain
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
