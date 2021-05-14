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
  | EntityCreated of EntityData
  | EntityDestroyed of EntityData
type eDebug = DebugMessage of string