module Engine.cAPI
open System
open System.Collections
open System.Collections.Generic
open Engine.Component
open Engine.Domain
open Engine.Entity
open Engine.Event
open Engine.System
type Engine() =
  let eMan = entityManager
  let mutable runSystems = List<iRunSystem>()
  let mutable listenSystems = List<iListenSystem>()
  member this.CreateEntity() = eMan.CreateEntity()
  member this.DestroyEntity() = eMan.DestroyEntity
  static member SetComponent<'C> data entity = Pool<'C>.Add entity data; entity
  static member Filter1<'T>() = Filter.Filter1<'T> |> List.toSeq
  static member Filter2<'T1, 'T2>() = Filter.Filter2<'T1, 'T2> |> List.toSeq
  member this.AddSystem(e:iRunSystem) = ()
  static member Listen<'T>(handler) = ()
  static member Post<'T>(handler:'T) = ()
  static member Get<'T>(entity:Entity): 'T = Unchecked.defaultof<'T>
