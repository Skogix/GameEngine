module Engine.Domain

open System
let debugEnabled = true
type EntityId = int
type Entity = {
  Id: EntityId
  Generation:int
  Active:bool
}
type ComponentPoolId = int
type Component<'C> =
    {
      Type: Type
      Owner: Entity
      Data: 'C
    }
// vilken funkar bäst?
type eEvent<'C> =
  | EntityCreated of Entity
  | EntityDestroyed of Entity
//  | ComponentUpdated of Component<'C>
//type EntityCreated = Entity
//type EntityDestroyed = Entity
type ComponentUpdated<'C> = Component<'C>
type eDebug = string