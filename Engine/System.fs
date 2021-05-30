module Engine.System
type iRunSystem =
  abstract Run: unit -> unit
type SystemManager() =
  let mutable runSystems = []
  member this.AddSystem system = runSystems <- system::runSystems
  member this._runSystems = runSystems
