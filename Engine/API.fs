module Engine.API

open Engine.Domain
type Engine() =
  member this.Init = 0
  member this.CreateEntity() = 0
type Entity with
  member this.Get<'C>() = 0
  member this.Set(c) = 0
  member this.Remove<'C>() = 0
