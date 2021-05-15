module ConsoleOutput.ZtriderScratch

open System
open Engine.API
open Engine.Domain

/// Statiska components, bara data (tänk structs i c#)
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

// engine är hela engine bakom allt, doesnt matter osv (är en shortcut till basic-funktioner och kanske ett par object
// men ska bytas ut till "agents" eller  "object" som init:ar skit som inte betyder något atm
let engine = Engine()

// events är bara en typ som innehåller data
// ett move-event är data som är en tuple/datatyp som är <x, y>
// här for det är enkelt så är from=en entity, to= ny position
type MoveEvent = {
  From: Entity // ville hålla det enkelt, så egentligen så är det en pos -> pos, men är något som byter position
  To: Position  
}
// onEvent är ingen egen typ, men en funktion som svarar på ett event och returnar Null)
// (men den lyssnar på events av typ X och kan inte gora något utan att skicka ett nytt event)
type OnMoveEvent() =
  // handler är bara en funktion som tar MoveEvent och returnar "null"/oftast skickar ett event
  let handler (event:MoveEvent) =
    // moveevent är typ ett argument med två values/en tuple (from (entity/container) to (nyPos))
    event.From.Update{x=event.To.x;y=event.To.y} // updatea position (engine skickar events om update)
  do engine.Listen<MoveEvent> handler // (regga "eventhandler" som "subscriber" till moveEvent)
// ett enkelt system/funktionalitet som kors varje update, dvs kolla inputkey
type InputSystem() =
  member this.Run() = // run kors varje tick/updatecycle
    let input = Console.ReadKey(true).Key
    // filters just nu returnar alla components/all data som har en position
    for (pos, player, entity) in engine.Filter2<Position, Player>() do
      let entityPos = entity.Get<Position>().Data
      match input with
      | ConsoleKey.UpArrow -> engine.Post<MoveEvent> {From=entity; To={x=1;y=entityPos.y}}
      | _ -> ()
    ()