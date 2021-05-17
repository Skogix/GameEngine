module Engine.Domain

open System
let mutable debugEnabled = true
type EntityId = int
type Entity = {
  Id: EntityId
  Generation:int
  Active:bool
  }

type ComponentPoolId = int
type Component<'C> = {
      Type: Type
      Owner: Entity
      Data: 'C }

// Engine events
type ComponentUpdated<'C> = {updatedComponent: Component<'C>}
type EntityCreated =  {createdEntity: Entity}
type EntityDestroyed = {destroyedEntity: Entity}

type Debug =
  | DebugMessage of string
  | Wawa of string // måste ha två alternativ for gettype().Name ska returnera rätt -.-