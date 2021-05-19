module ConsoleOutput.GameEvents

open System
open ConsoleOutput.GameComponents
open Engine.Domain

type KeyPressedEvent = {keyPressed:ConsoleKey;entity:Entity}
type Moved = {moved:Component<Transform>}

type MoveCommand = {positionComponent:Component<Transform>;destination:Transform}
type AttackCommand = {attacker:Entity;defender:Entity}
