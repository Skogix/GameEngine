module Engine.Command
open Domain
type iCommand = interface end
type CommandManager() =
  let mutable store: iCommand list = []
  member this.AddToStore event = store <- event::store
  member this.GetStore() = store
let engineCommandManager = CommandManager()
type CommandPool<'t when 't :> iCommand>() =
  static let mutable pool: Map<EventId, 't> = Map.empty
  static let mutable listeners: ('t -> unit) list = []
  static member AddListener listener = listeners <- listener::listeners
  static member Post event =
    engineCommandManager.AddToStore event
    for listener in listeners do listener event
