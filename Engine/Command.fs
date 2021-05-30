module Engine.Command
open Domain
type iCommand = interface end
type CommandManager() =
  let mutable store: iCommand list = []
  member this.AddToCommandStore event = store <- event::store
  member this.GetCommandStore() = store
let engineCommandManager = CommandManager()
type CommandPool<'t when 't :> iCommand>() =
  static let mutable pool: Map<CommandId, 't> = Map.empty
  static let mutable listeners: ('t -> unit) list = []
  static member AddCommandListener listener = listeners <- listener::listeners
  static member PostCommand event =
    engineCommandManager.AddToCommandStore event
    for listener in listeners do listener event
