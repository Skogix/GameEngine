# Skogix GameEngine
versioning
```
- systems
- component, pools
0.60 - debug, logging, api
0.50 - entity state
0.40 - eventStore
0.30 - engineEvents
0.20 - setup/planning
0.10 - readme, project setup, init
```
events
```
EntityCreated
EntityDestroyed
ComponentChanged
ComponentDestroyed
ComponentCreated
Debug
```
todo
```
använda records eller rena types for events?
använda concurrentqueue och skota async-calls själv?
skippa commands helt och bara calla tryToEvents? låta lyssna på events skota allt
mer safeguards for entity generations, inkludera entitdata i entity?
Engine mailbox state
Component pools
Component mailbox state?
Systems
 Filters
 OnEvent
 Run
```