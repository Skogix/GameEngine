module Engine.Domain

open System
let debugEnabled = true
type EntityId = int
type Entity = {
  Id: EntityId
  Generation:int
  Active:bool }
type ComponentPoolId = int
type Component<'C> = {
      Type: Type
      Owner: Entity
      Data: 'C }

type ComponentUpdated<'C> = {updatedComponent: Component<'C>}
type EntityCreated =  {createdEntity: Entity}
type EntityDestroyed = {destroyedEntity: Entity}
//type EntityCreated = Entity
//type EntityDestroyed = Entity
type DebugMessage = string