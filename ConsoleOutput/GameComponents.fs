module ConsoleOutput.GameComponents

open Engine.Domain


type AttackDamage = int
type AttackType =
  | Slashing
  | Piercing
  | Blunt
type Magic = Entity -> unit
type Weapon =
  | Sword of AttackDamage * AttackType * Magic option
  | Unarmed of AttackDamage * AttackType
type Block = int
type Armor =
  | Hat of Block
type Transform = {x:int;y:int}
type Drawable = {glyph:char}
type Player = {playerTag:string}
type Monster = {monsterTag:string}
type Health = {health:int}
