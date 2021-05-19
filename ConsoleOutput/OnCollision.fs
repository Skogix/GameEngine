module ConsoleOutput.OnCollision

open ConsoleOutput.Components
open ConsoleOutput.Events
open Engine
open Engine.Event
open Engine.System
open Engine.API

type CheckCollisionSystem() =
  interface iListenSystem with
    member this.Init() =
      do EngineEvent.Listen(fun (event:TryMove) ->
        match
          Filter.Filter1<PositionComponent>
          |> List.tryFind(fun x -> x.Data = event.tryMoveToPos) with
        | Some collidedWith ->
          EngineEvent.Post { collider=event.entity
                             collidedWith = collidedWith.Owner}
        | None ->
          event.entity.Add event.tryMoveToPos
          EngineEvent.Post{ movedToPos=event.tryMoveToPos
                            entity = event.entity }
        )
type OnCollision() =
  interface iListenSystem with
    member this.Init() = do EngineEvent.Listen<Collision>(fun (x:Collision) ->
      let a = x.collider
      let b = x.collidedWith
      match (a.Has<AttackComponent>() && b.Has<HealthComponent>()) with
      | true ->
        EngineEvent.Post{ attacker=a
                          defender = b }
        printfn $"{a.Id} attacks {b.Id}"
      | false -> ()
      )
