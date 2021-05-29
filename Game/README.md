# Skogix Game
##### Flow
```f#
components/data
events/commands
skapa entities och adda components
onRunSystems
onEventSystems
run
```
##### Testgame
```f#
Components
  position x y
  glyph char
Commands
  tryMove
Events
  onTryMove entity newPos
    kolla collision, move, send moved
  onMoved oldPos newPos
    send Ouput
```
##### IO
```f#
type MoveInput = Up | Down | Left | Right
type Output =
type Input =
  | Move of MoveInput
  | GameScreen
  | DebugScreen
```
