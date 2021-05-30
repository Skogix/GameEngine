module Engine.Filter

open System
open Engine.Component
open Engine.Domain

let filter1<'a>() =
  let a: Component<'a> list = ComponentPool<'a>.GetAllComponents()
  let e: Entity list = ComponentPool<'a>.GetAllEntities()
  (a, e)
let filter2<'a, 'b>() =
  let es =
    ComponentPool<'a>.GetAllEntities()
    |> ComponentPool<'b>.FilterEntities
  let a = ComponentPool<'a>.GetAllComponentsFromEntities(es)
  let b = ComponentPool<'b>.GetAllComponentsFromEntities(es)
  (es, a, b)
let filter3<'a, 'b, 'c>() =
  let es =
    ComponentPool<'a>.GetAllEntities()
    |> ComponentPool<'b>.FilterEntities
    |> ComponentPool<'c>.FilterEntities
  let a = ComponentPool<'a>.GetAllComponentsFromEntities(es)
  let b = ComponentPool<'b>.GetAllComponentsFromEntities(es)
  let c = ComponentPool<'c>.GetAllComponentsFromEntities(es)
  (es, a, b, c)
