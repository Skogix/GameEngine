module Engine.Component

open System
open Engine.Domain
open Engine.Event

type Pool<'T>() =
  static let createComponent e d =
    {
      Type = typedefof<'T>
      Owner = e
      Data = d
    }
  static let mutable pool: Map<Entity, Component<'T>> = Map.empty
  static member Set<'T> e (d:'T) =
    let c = createComponent e d
    // todo ska jag skicka componenten eller datan direkt?
    // tror använda datan direkt funkar bättre?
    EngineEvent.Post c
    EngineEvent.Post d
    pool <- pool.Add(e,c)
  static member AllEntities = [for map in pool do map.Key]
  static member AllComponents = [for map in pool do map.Value]
