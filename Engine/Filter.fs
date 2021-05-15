module Engine.Filter

open Engine.Component
open Engine.Domain

let FilterEntity1<'A> = Pool<'A>.AllEntities 
let FilterEntity2<'A,'B> = Pool<'A>.AllEntities |> List.filter(Pool<'B>.ContainsEntity)
let FilterEntity3<'A,'B, 'C> = Pool<'A>.AllEntities |> List.filter(Pool<'B>.ContainsEntity) |> List.filter(Pool<'C>.ContainsEntity)
let GetComponentsFromEntityList<'A> (es:Entity list) =
  es
  |> List.map Pool<'A>.Get
let Filter1<'A> =
  let es = FilterEntity1<'A>
  let a = es |> GetComponentsFromEntityList<'A>
  es |> List.zip a
let Filter2<'A, 'B> =
  let es = FilterEntity2<'A, 'B>
  let a = (es |> GetComponentsFromEntityList<'A>)
  let b = (es |> GetComponentsFromEntityList<'B>)
  es |> List.zip3 a b
let Filter3<'A, 'B, 'C> =
  let es = FilterEntity3<'A,'B,'C>
  let a = (es |> GetComponentsFromEntityList<'A>)
  let b = (es |> GetComponentsFromEntityList<'B>)
  let c = (es |> GetComponentsFromEntityList<'C>)
  (es, a, b, c)
