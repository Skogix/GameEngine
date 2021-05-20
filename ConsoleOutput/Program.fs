open System
open Engine
open Engine.API
open Engine.Domain
open Game1
let engine = Engine.Engine.Engine()
type Name = {name:string}
type Position = {x:int;y:int}


type WeaponType =
  | Sword
type Item =
  | Weapon of WeaponType
type Inventory = {items:Item list}
type Character =
  interface Engine.Filter.iBluePrint with
    member this.Types = [typedefof<Name>;typedefof<Position>]
  
let createPlayer =
  (engine.CreateEntity, [])
  |> engine.AddBlueprint{name="Skogix"}
[<EntryPoint>]
let main _ =
  Debug.DebugStatus <- Debug.Enabled
  let player = createPlayer
  printfn "Player: %A" player
  for msg in Debug.debugMessages do
    printfn "%A" msg
  
  
  0