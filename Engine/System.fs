module Engine.System
type iRunSystem =
  abstract Run: unit -> unit
type iOnSystem =
  abstract Init: unit -> unit
type SystemManager() =
  let mutable runSystems: iRunSystem list = []
  let mutable onSystems: iOnSystem list = []
  member this.AddRunSystem system = runSystems <- system::runSystems
  member this.AddOnSystem system = onSystems <- system::onSystems
  member this._runSystems = runSystems
  member this._onSystems = onSystems
  member this.Init() =
    for onSystem in onSystems do onSystem.Init()
  
  member this.Run() =
    for runSystem in runSystems do runSystem.Run()
