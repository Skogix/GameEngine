module Engine.World
type EntityId = int
type GenerationId = int
type Entity = {
  Id: EntityId
  Generation: GenerationId
}
type World() =
  member this.CreateEntity() = { Id = 0
                                 Generation = 0 }
  member this.DestroyEntity entity = ()