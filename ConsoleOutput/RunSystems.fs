module ConsoleOutput.RunSystems

open System
open ConsoleOutput.Components
open Engine
open Engine.Debug
open Engine.Event
open Engine.System
open Events

/// Går igenom alla entities som har en position och en graphic och printar till console
/// todo render ordering, just nu så finns det ingen priolista
type RenderSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      let filter = Filter.Filter2<PositionComponent, GraphicComponent>
      match Debug.DebugStatus with
      | DebugOption.Enabled ->
        let playerFilter = Filter.Filter3<PlayerComponent, PositionComponent, GraphicComponent>
        let monsterfilter = Filter.Filter3<MonsterComponent, PositionComponent, GraphicComponent>
        for player, pos, glyph in playerFilter do
          printfn $"{player.Data.playerName}: {glyph.Data.glyph} at {pos.Data.x},{pos.Data.y}"
        for monster, pos, glyph in monsterfilter do
          printfn $"{monster.Data.monsterName}: {glyph.Data.glyph} at {pos.Data.x},{pos.Data.y}"
        printfn "-------------------------------------"
      | DebugOption.Disabled ->
        Console.Clear()
        printfn "-------------------------------------"
        for pos, render in filter do
          Console.SetCursorPosition(pos.Data.x, pos.Data.y)
          printf $"{render.Data.glyph}"
        Console.SetCursorPosition(1, 21)
        printfn "-------------------------------------"
/// Hämtar input från user for varje entity med player, position och graphic
type InputSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      for player, pos, glyph in Filter.Filter3<PlayerComponent, PositionComponent, GraphicComponent> do
        let key = Console.ReadKey(true).Key
        eEvent.Post{keyPressed=key;entity=player.Owner}
type AiSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      let rand = System.Random().Next(1,4)
      for monster, pos, glyph in Filter.Filter3<MonsterComponent, PositionComponent, GraphicComponent> do
        match rand with
        | 1 -> eEvent.Post<MoveCommand>{keyPressed=ConsoleKey.UpArrow;entity=monster.Owner}
        | 2 -> eEvent.Post{keyPressed=ConsoleKey.DownArrow;entity=monster.Owner}
        | 3 -> eEvent.Post{keyPressed=ConsoleKey.LeftArrow;entity=monster.Owner}
        | 4 -> eEvent.Post{keyPressed=ConsoleKey.RightArrow;entity=monster.Owner}
        | _ -> eEvent.Post{keyPressed=ConsoleKey.Spacebar;entity=monster.Owner}
        
      
      ()
