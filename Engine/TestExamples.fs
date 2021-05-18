module Engine.TestExamples

open System.ComponentModel.Design

module TestDomain =
  type Flavour =
    | Vanilla
    | Strawberry
  type Event =
    | Flavour_sold of Flavour
    | Flavour_restocked of Flavour * int
    | Flavour_went_out_of_stock of Flavour
    | Flavour_was_not_in_stock of Flavour
module TestProjections =
  open EventStore
  open TestDomain
  let soldOfFlavour flavour state =
    state
    |> Map.tryFind flavour
    |> Option.defaultValue 0
  let updateSoldFlavours state event =
    match event with
    | Flavour_sold flavour ->
      state
      |> soldOfFlavour flavour
      |> fun amount -> state |> Map.add flavour (amount+1)
    | _ -> state
  let soldFlavours: Projection<Map<Flavour, int>, Event> = {
    Init = Map.empty
    Update = updateSoldFlavours
  }
  let restock flavour number stock =
    stock
    |> Map.tryFind flavour
    |> Option.defaultValue 0
    |> fun amount -> stock |> Map.add flavour (amount + number)
  let updateFlavoursInStock stock event =
    match event with
    | Flavour_sold f -> stock |> restock f -1
    | Flavour_restocked (flavour, amount) -> stock |> restock flavour amount
    | _ -> stock
  let flavoursInStock: Projection<Map<Flavour, int>, Event> = {
    Init = Map.empty
    Update = updateFlavoursInStock
  }
  let stockOf flavour stock =
    stock
    |> Map.tryFind flavour
    |> Option.defaultValue 0
module TestBehaviour =
  open TestProjections
  open TestDomain
  open EventStore
  let sellFlavour flavour events =
    let stock =
      events
      |> project flavoursInStock
      |> stockOf flavour
    match stock with
    | 0 -> [Flavour_was_not_in_stock flavour]
    | 1 -> [Flavour_sold flavour; Flavour_went_out_of_stock flavour]
    | _ -> [Flavour_sold flavour]
  let restock flavour amount events = [Flavour_restocked (flavour, amount)]
module Tests =
  open TestDomain
  open Expecto
  open Expecto.Expect
  let Given = id
  let When handler events = handler events
  let Then expectedEvents events = equal events expectedEvents "Events should equal expected events"
  let tests =
    testList "sellFlavour"
      [
        test "Flavour_Sold" {
          Given
            [
              Flavour_restocked (Vanilla, 5)
              Flavour_sold Vanilla
              Flavour_sold Vanilla
            ]
          |> When (TestBehaviour.sellFlavour Vanilla)
          |> Then [Flavour_sold Vanilla]
        }
      ]
  let runTests() =
    Tests.runTests defaultConfig tests |> ignore