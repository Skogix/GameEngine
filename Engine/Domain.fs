module Engine.Domain
type EntityId = int
type Entity = EntityId
[<StructuredFormatDisplay("[{Id}:{Generation}:{Active}]")>]
type EntityData = {
  Generation:int
  Active:bool
}
type iEvent = interface end
type EntityEvent =
  | EntityCreated of Entity
  | EntityDestroyed of Entity
  interface iEvent