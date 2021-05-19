module Engine.Engine
open System
open System.Collections.Generic
open Engine.System
type Engine() =
  let iRunSystems = List<iRunSystem>()
  let iListenSystems = List<iListenSystem>()
  let random = Random()
  member this.Random a b = random.Next(a,b)
  member this.AddRunSystem<'s when 's :> iRunSystem> system = iRunSystems.Add system
  member this.AddListenSystem<'s when 's :> iListenSystem> system = iListenSystems.Add system
  member this.Init() =
    for system in iListenSystems do
      system.Init()
  member this.Run() =
    for system in iRunSystems do
      system.Run()
