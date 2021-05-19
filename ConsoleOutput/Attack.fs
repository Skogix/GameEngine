module ConsoleOutput.Attack

open ConsoleOutput.GameComponents
open ConsoleOutput.GameEvents
open Engine
open Engine.Event
open Engine.System
open Engine.API

type OnAttackCommand() =
  interface iListenSystem with
    member this.Init() = do eEvent.Listen(fun (x:AttackCommand) ->
      let attacker = x.attacker
      let defender = x.attacker
      let hasWeapon = attacker.Has<Weapon>()
      let hasArmor = attacker.Has<Armor>()
      Debug.debug "Attacking"
      ()
      )