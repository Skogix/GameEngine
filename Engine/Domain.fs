module Engine.Domain
type EntityId = int
type Entity = {Id:EntityId}
type EntityData = {
  Generation:int
  Active:bool
}
type EntityEvent =
  | EntityCreated of Entity
  | EntityDestroyed of Entity