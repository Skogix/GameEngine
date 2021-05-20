module Game1.GameComponents

open Engine
open Engine.Domain
open Engine.Engine
open Engine.API


type GameOutput = string list 
let output:GameOutput = Debug.debugMessages

// basics
type Name = {name:string}

type AttackValue = int
type WeaponType =
  | Sword
type Weapon = AttackValue * WeaponType
type Item =
  | Weapon of Weapon
type Position = {x:int;y:int}
type InventorySlots =
  | Weapon of Weapon option
type Inventory = {inventory:InventorySlots list}
  
// blueprints 
type PlayerBlueprint = {
  name: string
  positionX: int
  positionY: int
}
type Player = {
  name: Component<Name>
  position: Component<Position>
  inventory: Component<Inventory>
}