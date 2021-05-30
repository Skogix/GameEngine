module Engine.System
type iRunSystem =
  abstract Run: unit -> unit
type SystemManager() =
  let mutable runSystems = []
  member this.Add system = runSystems <- system::runSystems
  member this._runSystems = runSystems
