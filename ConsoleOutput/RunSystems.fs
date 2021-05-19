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
      | DebugOption.Disabled ->
        for pos, render in filter do
        printfn $"Render: {render.Data.glyph} at {pos.Data.x},{pos.Data.y}"
      | DebugOption.Enabled ->
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
