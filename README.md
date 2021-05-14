# Skogix GameEngine
versioning
```
- systems
- component, pools
- debug, logging
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