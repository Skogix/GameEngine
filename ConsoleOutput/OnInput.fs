module ConsoleOutput.OnInput

open System
open ConsoleOutput.Components
open ConsoleOutput.Events
open Engine.Event
open Engine.System
open Engine.API

type OnKeyPressed() =
  interface iListenSystem with
    member this.Init() =
      do EngineEvent.Listen(fun (x:KeyPressed) ->
        let oldPos = x.entity.Get<PositionComponent>()
        let newPosData =
          match x.keyPressed with
          | ConsoleKey.UpArrow -> {x=oldPos.Data.x;y=oldPos.Data.y-1}
          | ConsoleKey.DownArrow -> {x=oldPos.Data.x;y=oldPos.Data.y+1}
          | ConsoleKey.LeftArrow -> {x=oldPos.Data.x-1;y=oldPos.Data.y}
          | ConsoleKey.RightArrow -> {x=oldPos.Data.x+1;y=oldPos.Data.y}
          | _ -> oldPos.Data
        EngineEvent.Post{ tryMoveToPos=newPosData
                          entity = x.entity }
                          
        )
