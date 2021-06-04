# Skogix ECS
##### ToDo
```f#
Sätt en ordentlig standard for IO
ConsoleUi/debug
IO
Flytta ut tests till moduler?
```
##### API
```f#
World.CreateEntity = unit -> Entity
World.DestroyEntity = Entity -> unit
World.AddRunSystem = iRunSystem -> unit
World.AddEventSystem = iEventSystem -> unit
World.Command = iCommand -> iCommand option
World.Event = iEvent -> unit
World.Filter = 't -> (Entity * iComponent) list
World.Init = unit -> unit
World.Run = unit -> unit
Entity<'t>.Add = 't -> data:'t -> unit
Entity<'t>.Set = 't -> data:'t -> iComponent<'t>
Entity<'t>.Remove = 't -> unit
iComponent.Update = data:'t -> iComponent<'t>
```
##### Events/Commands
```f#
type EngineCommand<'t> =
  | CreateEntity of AsyncReplyChannel<Entity>
  | DestroyEntity
  | SetComponent of AsyncReplyChannel<iComponent<'t>>
  | RemoveComponent of iComponent<'t>
type EngineEvent<'t> =
  | EntityCreated of Entity
  | EntityDestroyed of EntityId
  | ComponentUpdated of iComponent<'t>
```
##### Types
```f#
type EntityId = int
type Entity = {
  Id: EntityId
  Generation: int
  Active: true
}
type iComponent<'t> = {
  Data: 't
  Owner: Entity
}
type iEvent = interface end
type iCommand = interface end
type iRunSystem = 
  interface 
    abstract member Run: unit -> unit
    end
type iEventSystem = 
  interface 
  abstract member Init: unit -> unit
    end
```
##### State
```f#
EventPool<'t> =
  static 't -> unit list
ComponentPool<'t> =
  static Map<Entity, iComponent<'t>>
EventManager() =
  mailbox Map<EventPoolId, Type>
EntityManager() =
  mailbox Map<EntityId, Entity>
CommandManager() =
  mailbox iCommand
EventManager() =
  mailbox iEvent
SystemManager() =
  iRunSystem list
World() =
  EntityManager()
  EventManager()
  CommandManager()
  EventManager()
  SystemManager()
```