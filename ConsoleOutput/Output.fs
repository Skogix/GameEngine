module ConsoleOutput.Output

open System
open ConsoleOutput.GameComponents
open ConsoleOutput.GameComponents
open Engine
open Engine.Debug
open Engine.API
open ConsoleOutput.GameEvents
open Engine.Event
open Engine.System

type RunRenderer() =
  let renderGame() =
    let filter = Filter.Filter2<Transform, Drawable>
    for pos, glyph in filter do
      Console.SetCursorPosition(pos.Data.x+90, pos.Data.y)
      printf $"{glyph.Data.glyph}"
  let renderDebug() =
    let playerFilter = Filter.Filter3<Player, Transform, Drawable>
    let monsterFilter = Filter.Filter3<Monster, Transform, Drawable>
    for player, pos, glyph in playerFilter do
      printfn $"{player.Data.playerTag}: {glyph.Data.glyph} at {pos.Data.x},{pos.Data.y}"
    for monster, pos, glyph in monsterFilter do
      printfn $"{monster.Data.monsterTag}(HP:{monster.Owner.Get<Health>().Data.health}): {glyph.Data.glyph} at {pos.Data.x},{pos.Data.y}"
    debug "------------------------"
    debugMessages
    |> List.iter (printfn "%s")
    debugMessages <- []
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      match DebugStatus with
      | DebugType.Enabled ->
        renderDebug()
      | Disabled ->
        Console.Clear()
        renderGame()
      | DebugType.Combo ->
        Console.Clear()
        renderDebug()
        renderGame()

/// Hämtar input från user for varje entity med player, position och graphic
type RunGetInput() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      for player in Filter.Filter1<Player> do
        let key = Console.ReadKey(true).Key
        eEvent.Post{keyPressed=key;entity=player.Owner}
        debug $"{player.Owner} pressed {key}"
type AiSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      let random = Random()
      for monster in Filter.Filter1<Monster> do
        let rand = random.Next(1,5)
        match rand with
        | 1 -> eEvent.Post{keyPressed=ConsoleKey.UpArrow;entity=monster.Owner}
        | 2 -> eEvent.Post{keyPressed=ConsoleKey.DownArrow;entity=monster.Owner}
        | 3 -> eEvent.Post{keyPressed=ConsoleKey.LeftArrow;entity=monster.Owner}
        | 4 -> eEvent.Post{keyPressed=ConsoleKey.RightArrow;entity=monster.Owner}
        | _ -> eEvent.Post{keyPressed=ConsoleKey.Spacebar;entity=monster.Owner}
      
      ()
