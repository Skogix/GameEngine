module Engine.Filter

open System
open System.Collections
open Engine.Component
open Engine.Domain
let zip2 xs ys = 
  let rec loop result xs ys =
    match xs,ys with 
    | [],[]         -> result
    | xh::xt,yh::yt -> loop ((xh,yh)::result) xt yt
    | _ -> failwith "xs och ys har olika längd"
  loop [] xs ys |> List.rev
let zip3 xs ys zs = 
  let rec loop result xs ys zs =
    match xs,ys,zs with 
    | [],[],[] -> result
    | xh::xt,yh::yt, zh::zt -> loop ((xh,yh,zh)::result) xt yt zt
    | _ -> failwith "xs, ys och zs har olika längd"
  loop [] xs ys zs |> List.rev
let filter1<'a>() = ComponentPool<'a>.GetAllComponents()
let filter2<'a, 'b>() =
  let es =
    ComponentPool<'a>.GetAllEntities()
    |> ComponentPool<'b>.FilterEntities
  let a = ComponentPool<'a>.GetAllComponentsFromEntities(es)
  let b = ComponentPool<'b>.GetAllComponentsFromEntities(es)
  zip2 a b 
let filter3<'a, 'b, 'c>() =
  let es =
    ComponentPool<'a>.GetAllEntities()
    |> ComponentPool<'b>.FilterEntities
    |> ComponentPool<'c>.FilterEntities
  let a = ComponentPool<'a>.GetAllComponentsFromEntities(es)
  let b = ComponentPool<'b>.GetAllComponentsFromEntities(es)
  let c = ComponentPool<'c>.GetAllComponentsFromEntities(es)
  zip3 a b c
