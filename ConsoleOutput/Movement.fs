module ConsoleOutput.Movement

open ConsoleOutput.GameComponents
open ConsoleOutput.GameEvents
open Engine.Component
open Engine.Domain
open Engine.Event
open Engine.System
open Engine.Debug
open Engine.API

type MoveDestinationResult =
  | Combat of Entity
  | Move
type OnMoveCommand() =
  interface iListenSystem with
    member this.Init() = do eEvent.Listen(fun (moveCommand:MoveCommand) ->
      let blockingEntityOption = Pool<Transform>.AllComponents |> List.tryFind(fun x -> x.Data = moveCommand.destination)
      match blockingEntityOption with
      | Some blockingEntity ->
        if blockingEntity.Owner.Has<Health>() then
          eEvent.Post{attacker=moveCommand.positionComponent.Owner;defender=blockingEntity.Owner}
          
          ()
      | None ->
        let newComponent = moveCommand.positionComponent.Update moveCommand.destination
        eEvent.Post{moved=newComponent}
      ())
