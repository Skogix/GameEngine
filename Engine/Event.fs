module Engine.Event
open System.Collections.Generic
open System.Diagnostics
open Engine

type EventListeners<'event>() =
  static let Listeners = List<'event -> unit>()
  static do Listeners.Add Debug.debug
  static member Post<'event> (event:'event) =
    for listener in Listeners do
      listener event
  static member Listen (handler:'event -> unit) =
    Listeners.Add handler
type EngineEvent<'event> =
  static member Post(event:'event) =
    EventListeners<'event>.Post event
  static member Listen<'event> handler =
    EventListeners<'event>.Listen handler
