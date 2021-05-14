module Engine.Domain

open System
let debugEnabled = true
type EntityId = int
type Entity = {Id:EntityId}
type EntityData = {
  Entity: Entity
  Generation:int
  Active:bool
}
type eEvent =
  | TestEvent of string
  | EntityCreated of Entity
  | EntityDestroyed of Entity
type Debug = DebugMessage of string
//type eEntityCreated = EntityCreated of Entity
//type eEntityDestroyed = EntityDestroyed of Entity
//type eDebug = Debug of string