module ConsoleOutput.Components

open Engine.Engine
open Engine.API

/// Components är datan en entity kan ha
type PositionComponent = {x:int;y:int}
type GraphicComponent = {glyph:char}
type PlayerComponent = {playerName:string}
type MonsterComponent = {monsterName:string}
type AttackComponent = {attack:int}
type HealthComponent = {health:int}
type PhysicsComponent = {isBlocking:bool}
type TagComponent = {tag:string}

/// Blueprints for att underlätta creation av entities och adda components
/// kommer losas via json/skogixjson senare
let createPlayer name glyph hp attack (pos:PositionComponent) (engine:Engine) =
  let player = engine.CreateEntity()
  player.Add{playerName=name}
  player.Add{isBlocking=true}
  player.Add pos
  player.Add{glyph=glyph}
  player.Add{attack=attack}
  player.Add{health=hp}
  player.Add{tag="Player"}
  player
let createMonster glyph hp attack (pos:PositionComponent) (engine:Engine) =
  let monster = engine.CreateEntity()
  monster.Add{monsterName="Monster"}
  monster.Add{isBlocking=true}
  monster.Add pos
  monster.Add{glyph=glyph}
  monster.Add{attack=attack}
  monster.Add{health=hp}
  monster.Add{tag="Monster"}
  monster
let createWall (pos:PositionComponent) (engine:Engine) =
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
