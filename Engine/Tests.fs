module Engine.Tests
open Engine
open Engine.Domain
open Expecto
open Expecto.Expect

module TestBehaviour =
  let getEntitiesCreated state event =
    state
    |> Map.tryFind event
    |> Option.defaultValue 0
  let createEntity events =
    {
    Id = 0
    Generation = 0
    Active = true }
module TestData =
  let entity = {
    Id = 0
    Generation = 0
    Active = true }
  let testComponent<'t>(data) = {
    Type = typedefof<'t>
    Owner = entity
    Data = data}
let Given = id
let When handler events = handler events
let Then expectedEvents events = equal events expectedEvents "Events should be equal"
let tests =
  testList "huhu" [
    test "wawa" {
      Given[
        // tomt
//        EntityCreated TestData.entity
      ]
      |> Then []
    }
  ]
let runTests() = runTests defaultConfig tests |> ignore