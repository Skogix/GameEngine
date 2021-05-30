module Engine.Domain
open Expecto
let runTest test = runTestsWithCLIArgs [] [||] test |> ignore
type EntityId = int
type GenerationId = int
type EventId = int
type CommandId = int
type Entity = {
  Id: EntityId
  Generation: GenerationId
  Active: bool
}
type Component<'t> = {
  Owner: Entity
  Data: 't
}
