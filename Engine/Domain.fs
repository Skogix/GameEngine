module Engine.Domain
type EntityId = int
type Entity = EntityId
[<StructuredFormatDisplay("[{Id}:{Generation}:{Active}]")>]
type EntityData = {
  Generation:int
  Active:bool
}
type EntityEvent =
  | EntityCreated of Entity
  | EntityDestroyed of Entity