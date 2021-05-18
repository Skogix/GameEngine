module Engine.EventStore

open System.Net.Sockets

type EventProducer<'e> = 'e list -> 'e list
//type EventStore<'e> = {
//  Get: unit -> 'e list
//  Append: 'e list -> unit
//  Evolve: EventProducer<'e> -> unit
//}
type Msg<'e> =
  | Get of AsyncReplyChannel<'e list>
  | Append of 'e list
  | Evolve of EventProducer<'e>
type EventStore() =
  let history = []
  let mailbox = MailboxProcessor.Start(fun inbox ->
    let rec loop history = async {
      let! msg = inbox.Receive()
      match msg with
      | Get rc ->
        rc.Reply history
        return! loop history
      | Append events -> return! loop (history @ events)
      | Evolve producer -> return! loop (history @ producer history)
    }
    loop history
    )
  member this.Append(event) = mailbox.Post (Append [event])
  member this.GetAll() = mailbox.PostAndReply Get
//let initEventStore() =
//  let history = []
//  let mailbox = MailboxProcessor.Start(fun inbox ->
//    let rec loop history = async {
//      let! msg = inbox.Receive()
//      match msg with
//      | Get rc ->
//        rc.Reply history
//        return! loop history
//      | Append events -> return! loop (history @ events)
//      | Evolve producer -> return! loop (history @ producer history)
//    }
//    loop history
//    )
//  let append events =
//    events
//    |> Append
//    |> mailbox.Post
//  let evolve producer =
//    producer
//    |> Evolve
//    |> mailbox.Post
//  {
//    Get = fun () -> mailbox.PostAndReply Get
//    Append = append
//    Evolve = evolve
//  }
type Projection<'state, 'event> = {
  Init: 'state
  Update: 'state -> 'event -> 'state
}
let project projection events = events |> List.fold projection.Update projection.Init