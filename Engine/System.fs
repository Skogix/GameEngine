module Engine.System

type iRunSystem =
  interface
    abstract Init: unit -> unit
    abstract Run: unit -> unit
    end
type iListenSystem =
  interface
    abstract Init: unit -> unit
    end

