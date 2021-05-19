module ConsoleOutput.Attack

open ConsoleOutput
open ConsoleOutput
open ConsoleOutput.GameComponents
open ConsoleOutput.GameEvents
open Engine
open Engine.Event
open Engine.System
open Engine.API

type OnAttackCommand() =
  interface iListenSystem with
    member this.Init() = do eEvent.Listen(fun (x:AttackCommand) ->
//      Debug.debug $"RunningFunc: {runAttackFunction}" 
      ()
      )