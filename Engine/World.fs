module Engine.World
type Entity = {
  Id: int
}
type World() =
  member this.CreateEntity() = {Id = 0}