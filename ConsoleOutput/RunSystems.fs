module ConsoleOutput.RunSystems

open System
open ConsoleOutput.Components
open Engine
open Engine.Event
open Engine.System
open Events

/// Går igenom alla entities som har en position och en graphic och printar till console
type RenderSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      let filter = Filter.Filter2<PositionComponent, GraphicComponent>
      match Debug.debugEnabled with
      | true ->
        for pos, render in filter do
        printfn $"Render: {render.Data.glyph} at {pos.Data.x},{pos.Data.y}"
      | false ->
        Console.Clear()
        for pos, render in filter do
        Console.SetCursorPosition(pos.Data.x, pos.Data.y)
        printf $"{render.Data.glyph}"
/// Hämtar input från user for varje entity med player, position och graphic
type InputSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      for player, pos, glyph in Filter.Filter3<PlayerComponent, PositionComponent, GraphicComponent> do
        let key = Console.ReadKey(true).Key
        eEvent.Post{keyPressed=key;entity=player.Owner}
