module ConsoleOutput.ZtriderScratch

// ToDo imorgon, hur fan forklarar du ECS+EventSourcing+F# till en programmerare?
// eli5 borde jag kunna, men är seriost svårt..

open System
open Engine.API
open Engine.Domain
/// basic intro for ECS
/// Entity = en identifier for whatever, guid, unik int
///   en entity är bara ett id, inget annat *
/// Component = Data/information (separera data från logik)
///   data driven development
///     du hanterar logik beroende på data
///       data bestämmer vilken logik som passar
/// System =
///   System tar data och applicerar logik, t.ex
///     Run; Saker som händer varje frame/tick, t.ex Rendering
///     OnEvent; Reagera på saker som händer, t.ex OnInputKeyPressed
///       (oftast så ett event svarar med ett nytt event)

// Components
type Position = {
  x:int
  y:int
}
type Player = {
  Name: string
  Position: Position
}
// Är något som inte finns i c#, choice types
// Direction är någon av up, down, left, right
type Direction = Up | Down | Left | Right

// object/singleton
// engine är blackboxen som hanterar allt just nu
let engine = Engine()
//
//// events är bara en typ som innehåller data(tror f# här använder samma event-class som c#)
//// ett move-event är data som är en tuple/datatyp som är <x, y>
//// här for det är enkelt så är from=en entity, to= ny position
//type MoveEvent = {
//  From: Entity // ville hålla det enkelt, så egentligen så är det en pos -> pos, men är något som byter position
//  To: Position  
//}
//// onEvent är ingen egen typ, men en funktion/datatyp som svarar på ett event och returnar Null)
//// tänk OnXXX event, den tar en input av ett event och skickar ett nytt event
//type OnMoveEvent() =
//  // handler/whatever är bara en funktion som tar MoveEvent och returnar "null"/oftast skickar ett event
//  let handler (event:MoveEvent) =
//    // moveevent är typ ett argument med två values/en tuple (from (entity/container) to (nyPos))
//    event.From.Update{x=event.To.x;y=event.To.y} // updatea position (engine skickar events om update)
//  do engine.Listen<MoveEvent> handler // (regga "eventhandler" som "subscriber" till moveEvent)
//// ett enkelt system/funktionalitet som kors varje update, dvs kolla inputkey
//type InputSystem() =
//  member this.Run() = // run kors varje tick/updatecycle
//    let input = Console.ReadKey(true).Key
//    // filters just nu returnar alla components/all data som har en position
//    for (pos, player, entity) in engine.Filter2<Position, Player>() do
//      let entityPos = entity.Get<Position>().Data
//      match input with
//      | ConsoleKey.UpArrow -> engine.Post<MoveEvent> {From=entity; To={x=1;y=entityPos.y}}
//      | _ -> ()
//    ()