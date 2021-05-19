module ConsoleOutput.OnAttack

open ConsoleOutput.Components
open ConsoleOutput.Events
open Engine.Event
open Engine.System
open Engine.API

type OnAttack() =
  interface iListenSystem with
    member this.Init() = do EngineEvent.Listen<Attacks>(fun (x:Attacks) ->
      let attackDamage = x.attacker.Get<AttackComponent>()
      let defenderHealth = x.defender.Get<HealthComponent>()
      let newDefenderHealth = {defenderHealth.Data with health = defenderHealth.Data.health - attackDamage.Data.attack}
      let newComponent = x.defender.Add newDefenderHealth
      EngineEvent.Post{ healthComponent=newComponent
                        entity = x.defender }
      ()
      )
type OnDamageTaken() =
  interface iListenSystem with
    member this.Init() = do EngineEvent.Listen<DamageTaken>(fun (x:DamageTaken) ->
      if x.healthComponent.Data.health <= 0 then
        printfn "NÅN DOG!"
      )
    
