module Engine.Event

open Engine.Domain
type EntityCreated = {entityCreated:Entity}
type iEvent = interface end
type EventManager() =
  let mutable store: iEvent list = []
  member this.AddEventToStore event = store <- event::store
  member this.GetEventStore() = store
let engineEventManager = EventManager()
type EventPool<'t when 't :> iEvent>() =
  static let mutable pool: Map<EventId, 't> = Map.empty
  static let mutable listeners: ('t -> unit) list = []
  static member AddEventListener listener = listeners <- listener::listeners
  static member PostEvent event =
    engineEventManager.AddEventToStore event
    for listener in listeners do listener event
