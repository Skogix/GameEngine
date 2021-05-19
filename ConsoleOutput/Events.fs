module ConsoleOutput.Events

open System
open ConsoleOutput.Components
open Engine.Domain

type KeyPressedEvent = {keyPressed:ConsoleKey;entity:Entity}
type TryMoveCommand = {tryMoveToPos:PositionComponent;entity:Entity}
type MovedEvent = {movedToPos:PositionComponent;entity:Entity}
type CollisionEvent = {collider:Entity;collidedWith:Entity}
type AttackCommand = {attacker:Entity;defender:Entity}
type DamageTakenEvent = {healthComponent:Component<HealthComponent>;entity:Entity}
type DeathEvent = {deadEntity:Entity}
