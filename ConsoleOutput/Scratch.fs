module ConsoleOutput.Scratch

open System
open Engine.API
open Engine.Domain

let e = Engine()
type InputChar = char
type Health = {
  HealthAmount: int
}
type Attack = {
  AttackAmount: int
}
type Defense = {
  DefenseAmount: int
}
type Description = {
  Name: string
  Description: string
}
// todo borde vara ett snyggare sätt att losa?
// annars så borde det här vara ett bra ställe att DIa måsten?
type Combat = unit
let addCombat health attack defense entity =
  entity
  |> e.AddComponent{HealthAmount=health}
  |> e.AddComponent{AttackAmount=attack}
  |> e.AddComponent{DefenseAmount=defense}
  |> e.AddComponent<Combat>()
let createPlayer =
  e.CreateEntity
  |> addCombat 100 10 3
let createMonster =
  e.CreateEntity
  |> addCombat 30 4 1
