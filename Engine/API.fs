module Engine.API

open Engine.Command
open Engine.Domain
open Engine.Event
open Engine.System
open Engine.World

let engineWorld = World()
type World with
  member this.CreateEntity() = this._EntityManager.CreateEntity()
  member this.DestroyEntity entity = this._EntityManager.DestroyEntity entity
  member this.OnEvent (handler:'t -> unit) = EventPool<'t>.AddEventListener handler
  member this.OnCommand (handler:'t -> unit) = CommandPool<'t>.AddCommandListener handler
  member this.Post<'t when 't :> iEvent> event = EventPool<'t>.PostEvent event
  member this.Command<'t when 't :> iCommand> command = CommandPool<'t>.PostCommand command
  member this.GetStore() = this._EventManager.GetEventStore()
  member this.AddRunSystem<'t when 't :> iRunSystem> system = this._SystemManager.AddRunSystem system
  member this.AddOnSystem<'t when 't :> iOnSystem> system = this._SystemManager.AddOnSystem system
  member this.Filter1<'a>() = Filter.filter1<'a>()
  member this.Filter2<'a,'b>() = Filter.filter2<'a,'b>()
  member this.Filter3<'a,'b,'c>() = Filter.filter3<'a,'b,'c>()
  member this.Run() = this._SystemManager.Run()
type Entity with
  member this.TryGet<'t>() = engineWorld._ComponentManager.TryGetComponent<'t> this
  member this.Has<'t>() = engineWorld._ComponentManager.HasComponent<'t> this
  member this.Add<'t>(data:'t) = engineWorld._ComponentManager.AddComponent<'t> this data
  member this.Set<'t>(data:'t) = engineWorld._ComponentManager.SetComponent<'t> this data
type Component<'t> with
  member this.Update (data:'t) = this.Owner.Add<'t> data