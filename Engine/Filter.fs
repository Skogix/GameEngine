module Engine.Filter
open Component
open Engine.Domain
let FilterEntity1<'a> = Pool<'a>.AllEntities
let FilterEntity2<'a,'b> =
  Pool<'a>.AllEntities
  |> List.filter(Pool<'b>.ContainsEntity)
let FilterEntity3<'a,'b,'c> =
  Pool<'a>.AllEntities
  |> List.filter(Pool<'b>.ContainsEntity)
  |> List.filter(Pool<'c>.ContainsEntity)
let GetComponentsFromEntityList<'a> (es:Entity list) =
  es |> List.map Pool<'a>.Get
let Filter1<'a> =
  let es = FilterEntity1<'a>
  let a = (es |> GetComponentsFromEntityList<'a>)
  a 
let Filter2<'a,'b> =
  let es = FilterEntity2<'a,'b>
  let a = (es |> GetComponentsFromEntityList<'a>)
  let b = (es |> GetComponentsFromEntityList<'b>)
  a |> List.zip b  
let Filter3<'a,'b,'c> =
  let es = FilterEntity3<'a,'b,'c>
  let a = (es |> GetComponentsFromEntityList<'a>)
  let b = (es |> GetComponentsFromEntityList<'b>)
  let c = (es |> GetComponentsFromEntityList<'c>)
  a |> List.zip3 b c 
