module ConsoleOutput.Scratch

open System
open Engine.API
open Engine.Domain

let e = Engine()

type Tag =
  | Combat
  | Player
  | Monster

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
let addCombat health attack defense entity =
  entity
  |> e.AddComponent{HealthAmount=health}
  |> e.AddComponent{AttackAmount=attack}
  |> e.AddComponent{DefenseAmount=defense}
  |> e.AddComponent Combat
let createPlayer =
  e.CreateEntity
  |> addCombat 100 10 3
  |> e.AddComponent Player
let createMonster =
  e.CreateEntity
  |> addCombat 30 4 1
  |> e.AddComponent Monster
