module Engine.System
type iRunSystem =
  abstract Run: unit -> unit
type SystemManager() =
  let mutable runSystems: iRunSystem list = []
  member this.AddSystem system = runSystems <- system::runSystems
  member this._runSystems = runSystems
  member this.Run() = for runSystem in runSystems do runSystem.Run()
