module ConsoleOutput.Events

open System
open ConsoleOutput.Components
open Engine.Domain

/// Events och commands
/// todo hitta något vettigt sätt att skilja på dem och hur dem ska hanteras
type MoveCommand = {tryMoveToPos:PositionComponent;entity:Entity}
type AttackCommand = {attacker:Entity;defender:Entity}

type KeyPressedEvent = {keyPressed:ConsoleKey;entity:Entity}
type MovedEvent = {movedToPos:PositionComponent;entity:Entity}
type CollisionEvent = {collider:Entity;collidedWith:Entity}
type DamageTakenEvent = {healthComponent:Component<HealthComponent>;entity:Entity}
type DeathEvent = {deadEntity:Entity}
