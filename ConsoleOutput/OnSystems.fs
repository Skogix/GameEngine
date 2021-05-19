module ConsoleOutput.OnSystems

open System
open ConsoleOutput.Components
open ConsoleOutput.Events
open Engine
open Engine.Domain
open Engine.Event
open Engine.System
open Engine.API

/// helpers
type Direction = Up | Down | Left | Right

/// skapar en ny position beroende på direction
let createNewPosFromDirection (entity:Entity) direction =
  let oldPos = entity.Get<PositionComponent>().Data
  let newPos = 
    match direction with
    | Up -> {x=oldPos.x;y=oldPos.y-1}
    | Down -> {x=oldPos.x;y=oldPos.y+1}
    | Left -> {x=oldPos.x-1;y=oldPos.y}
    | Right -> {x=oldPos.x+1;y=oldPos.y}
  newPos
  
/// - ska keypressed gå som ett runsystem for det kors varje tick och är en av de
/// - få onevents som alltid skickar try-commands-ish?
/// matcha keyarrows och skicka moveCommand
type OnKeyPressed() =
  interface iListenSystem with
    member this.Init() =
      do eEvent.Listen(fun (x:KeyPressedEvent) ->
        let newPos =
          match x.keyPressed with
          | ConsoleKey.UpArrow -> Some (createNewPosFromDirection x.entity Up)
          | ConsoleKey.DownArrow -> Some (createNewPosFromDirection x.entity Down)
          | ConsoleKey.LeftArrow -> Some (createNewPosFromDirection x.entity Left)
          | ConsoleKey.RightArrow -> Some (createNewPosFromDirection x.entity Right)
          | _ -> None
        match newPos with
        | Some pos ->
          eEvent.Post{ tryMoveToPos=pos
                       entity = x.entity }
        | None -> ()
        )
/// om ny pos är "tom", skicka move-event
/// om ny pos är något, skicka collide-event
type OnTryMove() =
  interface iListenSystem with
    member this.Init() =
      do eEvent.Listen(fun (event:MoveCommand) ->
        match
          Filter.Filter1<PositionComponent>
          |> List.tryFind(fun x -> x.Data = event.tryMoveToPos) with
        | Some collidedWith ->
          eEvent.Post { collider=event.entity;collidedWith = collidedWith.Owner}
        | None ->
          event.entity.Add event.tryMoveToPos
          eEvent.Post{ movedToPos=event.tryMoveToPos;entity = event.entity }
        )
/// om a har attack och b har health, skicka attack
/// om b IsBlocking true ()
/// om b IsBlocking false skicka moveEvent
/// annars inget
type CollisionTypes =
  | Attack
  | Move
  | DoNothing
type OnCollision() =
  interface iListenSystem with
    member this.Init() = do eEvent.Listen<CollisionEvent>(fun (x:CollisionEvent) ->
      let isAttack = (x.collider.Has<AttackComponent>() && x.collidedWith.Has<HealthComponent>())
      let isBlocking = x.collidedWith.Get<PhysicsComponent>().Data.isBlocking
      let getCollisionType = 
        match (isAttack, isBlocking) with
        | true, _ -> Attack
        | false, false -> Move
        | _, _ -> DoNothing
      match getCollisionType with
      | Attack -> 
        eEvent.Post<AttackCommand>{ attacker=x.collider;defender=x.collidedWith }
        printfn $"{x.collider.Id} attacks {x.collidedWith.Id}"
      | Move ->
        x.collider.Add (x.collidedWith.Get<PositionComponent>().Data)
        eEvent.Post<MovedEvent>{ movedToPos=x.collidedWith.Get<PositionComponent>().Data
                                 entity = x.collider }
      | DoNothing -> ()
      )
/// skicka damageTakenEvent (hp - attackdamage)
type OnAttack() =
  interface iListenSystem with
    member this.Init() = do eEvent.Listen<AttackCommand>(fun (x:AttackCommand) ->
      let attackDamage = x.attacker.Get<AttackComponent>()
      let defenderHealth = x.defender.Get<HealthComponent>()
      let newDefenderHealth = {defenderHealth.Data with health = defenderHealth.Data.health - attackDamage.Data.attack}
      let newComponent = x.defender.Add newDefenderHealth
      eEvent.Post{ healthComponent=newComponent;entity = x.defender }
      ()
      )
/// kolla om entitien som tog damage har <= 0 hp, isf skicka deathEvent
type OnDamageTaken() =
  interface iListenSystem with
    member this.Init() = do eEvent.Listen<DamageTakenEvent>(fun (x:DamageTakenEvent) ->
      if x.healthComponent.Data.health <= 0 then
        eEvent.Post{deadEntity=x.entity}
      )
/// hantera on death t.ex ge xp, droppa loot, ta bort entity
/// just nu bara ändra glyph, remove health
type OnDeath() =
  interface iListenSystem with
    member this.Init() = do eEvent.Listen<DeathEvent>(fun (x:DeathEvent) ->
      printfn "Någon dog: %A" x.deadEntity
      // todo fixa destroyEntity plz
      x.deadEntity.Add{glyph='x'}
      x.deadEntity.Add{isBlocking=false}
      x.deadEntity.Remove<HealthComponent>()
      x.deadEntity.Remove<MonsterComponent>()
      ()
      
      )
