#load "Domain.fs"
open System.Windows.Forms
#load "WorldMap.fs"
#load "CreateCities.fs"
#load "City.fs"
#r @"C:\Users\samhe\.nuget\packages\fsrandom\1.4.0.1\lib\netstandard1.6\FsRandom.dll"
open City
open WorldMap
open CreateCities
open Domain
open FsRandom
open System

let stockholm =
  {name = "stockholm"; population = 100; production = [Wheat, 10; Fish, 20; Iron, 10]; export = [Wheat, 5; Fish, 10]; utility = 10; import = [Silk, 10]; autarchy = 10; surroundingTerrain = []}

let sample = WorldMap.worldMap 5 |> CreateCities.addCities |> printMap

let sumResources (city : City) =
  let rec subtractExport production exports list : YearlySupply list = 
    match production,exports with
    | (p :: ps), (e :: es) when fst p = fst e -> subtractExport ps es ((fst p, snd p - snd e) :: list)
    | (p :: ps), (e :: es) -> subtractExport ps es list
    | ps, [] -> ps @ list //List concatenation
    | _, _ -> list
  let resourcesAfterExport = subtractExport (List.sort city.production) (List.sort city.export) []

(*   let addImports (production : YearlySupply list) (imports : YearlySupply list) = 
    match imports with
    | (i :: is) -> 
      match List.tryFind i production with
      | Some resource -> List.fold (fun list resource' -> if fst resource = fst resource' then fst resource  ) *)

let utility (city: City) = failwith "implement this"
