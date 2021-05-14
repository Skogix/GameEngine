# Skogix GameEngine
versioning
```
0.07 - systems
0.06 - componentPools
0.05 - entity state
0.04 - eventStore
0.03 - engineEvents
0.02 - setup/planning
0.01 - readme, project setup, init
```
todo
```
mer safeguards for entity generations, inkludera entitdata i entity?
Engine mailbox state
Component pools
Component mailbox state?
Systems
 Filters
 OnEvent
 Run
```
architecture
```
Engine()
 Init
 Entities
EventStore()
 Events list
eEvent<'T>
 Subscribe/listen
 Publish/add
System
 Filter<1/2/3>
 CreateISubscribe
 CreateIRun
 CreateOnEvent
ComponentPool<'T>
 Map<Entity, 'T>
```
api
```
Engine.Init(Systems)
Engine
 Entities
 CreateEntity
System
 Helpers
Extensions
 Entity
  Set<C>
  TryRemove<C>
  TryGet<C>
  Has<C>
 Component
  Update
  Remove/Destroy
```