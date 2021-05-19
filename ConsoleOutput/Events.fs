module ConsoleOutput.Events

open System
open ConsoleOutput.Components
open Engine.Domain

type KeyPressed = {keyPressed:ConsoleKey;entity:Entity}
type TryMove = {tryMoveToPos:PositionComponent;entity:Entity}
type Moved = {movedToPos:PositionComponent;entity:Entity}
type Collision = {collider:Entity;collidedWith:Entity}
type Attacks = {attacker:Entity;defender:Entity}
type DamageTaken = {healthComponent:Component<HealthComponent>;entity:Entity}
