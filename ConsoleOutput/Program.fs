open System
open Engine
open Engine.API
open Engine.System
open Domain

//type Entity = int
type PositionComponent = {x:int;y:int}
type OnKeyPressedEvent = {key:ConsoleKey;entity:Entity}
type MovedEvent = {newPos:PositionComponent;entity:Entity}
type GetInputSystem() =
  interface iRunSystem with
    member this.Init() = ()
    member this.Run() =
      for pos, ent in Filter.Filter1<PositionComponent> do
        Engine.Post{
          key=Console.ReadKey(true).Key
          entity=ent
        }
type onKeyPressedEvent() =
  interface iListenSystem with
    member this.Init() = do Engine.Listen (fun (x:OnKeyPressedEvent) ->
      let oldPos = x.entity.Get<PositionComponent>().Data
      let newPos =
        match x.key with
        | ConsoleKey.UpArrow -> {    x = oldPos.x;    y = oldPos.y - 1 }
        | ConsoleKey.DownArrow -> {  x = oldPos.x;    y = oldPos.y + 1}
        | ConsoleKey.LeftArrow -> {  x = oldPos.x - 1;y = oldPos.y }
        | ConsoleKey.RightArrow -> { x = oldPos.x + 1;y = oldPos.y }
      x.entity.Set{x=newPos.x;y=newPos.y}
      Engine.Post{ newPos = newPos
                   entity = x.entity }
      )
[<EntryPoint>]
let main _ =
  let engine = Engine.API.Engine()
  engine.AddRunSystem (GetInputSystem())
  engine.AddListenSystem (onKeyPressedEvent())
  engine.Run()
  0