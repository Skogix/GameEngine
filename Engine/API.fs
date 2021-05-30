module Engine.API

open Engine.Domain
open Engine.Event
open Engine.System
open Engine.World
open Entity

let engineWorld = World()
type World with
  member this.CreateEntity() = this._EntityManager.CreateEntity()
  member this.DestroyEntity entity = this._EntityManager.DestroyEntity entity
  member this.Listen (handler:'t -> unit) = EventPool<'t>.AddListener handler
  member this.Post<'t when 't :> iEvent> event = EventPool<'t>.Post event
  member this.GetStore() = this._EventManager.GetStore()
  member this.AddRunSystem<'t when 't :> iRunSystem> system = this._SystemManager.Add system
type Entity with
  member this.TryGet<'t>() = engineWorld._ComponentManager.TryGet<'t> this
  member this.Has<'t>() = engineWorld._ComponentManager.HasComponent<'t> this
  member this.Add<'t>(data:'t) = engineWorld._ComponentManager.AddComponent<'t> this data
  member this.Set<'t>(data:'t) = engineWorld._ComponentManager.SetComponent<'t> this data
