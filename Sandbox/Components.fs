module ConsoleOutput.Components


/// Components är datan en entity kan ha
type Position = {x:int;y:int}
type Graphic = {glyph:char}
type Player = {playerName:string}
type Monster = {monsterName:string}
type Strength = {strength:int}
type Health = {health:int}
type Physics = {isBlocking:bool}
type Tag = {tag:string}


type AttackDamage = {attackDamage:int}
type AttackType =
  | Slashing
  | Piercing
  | Blunt
type Weapon =
  | Sword of AttackDamage * AttackType
type Combat = Strength * Health * Weapon option

/// Blueprints for att underlätta creation av entities och adda components
/// kommer losas via json/skogixjson senare
let createPlayer name glyph hp attack (pos:Position) (engine:Engine) =
  let player = engine.CreateEntity()
  player.SetName name
  player.Add<Combat>({strength=3}, {health=100}, (Some (Sword({attackDamage=10}, Slashing))))
  player.Add{playerName=name}
  player.Add{isBlocking=true}
  player.Add pos
  player.Add{glyph=glyph}
  player.Add{tag="Player"}
  player
let createMonster name glyph hp strength (pos:Position) (engine:Engine) =
  let monster = engine.CreateEntity()
  monster.SetName name
  monster.Add<Combat>({strength=strength}, {health=hp}, None)
  monster.Add{monsterName=name}
  monster.Add{isBlocking=true}
  monster.Add pos
  monster.Add{glyph=glyph}
  monster.Add<Strength>{strength=strength}
  monster.Add{health=hp}
  monster.Add{tag="Monster"}
  monster
let createWall (pos:Position) (engine:Engine) =
  let wall = engine.CreateEntity()
  wall.Add pos
  wall.Add {glyph='#'}
  wall.Add {isBlocking=true}
  wall.Add {tag="Wall"}
  ()
let createWalls width height (engine:Engine) =
  for x in [1..width] do
    createWall {x=x;y=1} engine
    createWall {x=x;y=height} engine
  for y in [1..height] do
    createWall {x=1;y=y} engine
    createWall {x=width;y=y} engine
