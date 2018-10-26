#load "Domain.fs"
#load "WorldMap.fs"
#load "CreateCities.fs"
#load "City.fs"
open City
open WorldMap
open CreateCities
open Domain
open System

let sampleMap = WorldMap.worldMap 5 |> CreateCities.addCities

// The value of another unit depending on the ratio of resource/population
// 1 < resource/population very low value
// 0.8 < resource/population < 1 low value
// 0.5 < resource/population < 0.8 medium/low value
// 0.2 < resource/population < 0.5 high value
// resource/production < 0.2 very high value
let utilityFromResources (city : City) =
  let createUtilityFromResource _ (supply : int) =
    let valueForNextUnit = 
      match float supply / float city.population with
      | r when r > 1.0 -> 0.2
      | r when r > 0.8 -> 0.4
      | r when r > 0.5 -> 0.6
      | r when r > 0.2 -> 0.8
      | r when r < 0.2 -> 1.5

    let currentUtility =
      match float supply / float city.population with
      | r when r > 1.0 -> 0.2 * (double city.population)
      | r when r > 0.8 -> 0.4 * (double city.population)
      | r when r > 0.5 -> 0.6 * (double city.population)
      | r when r > 0.2 -> 0.8 * (double city.population)
      | r when r < 0.2 -> 1.5 * (double city.population)
    {utility = currentUtility; valueForNextUnit = valueForNextUnit}
  let newUtility = Map.map createUtilityFromResource city.total
  {city with utility = newUtility}

let stockholm =
  {name = "Stockholm"; population = 100; total = YearlySupply [Wheat, 1000; Silk, 1]; production = YearlySupply ([Wheat, 1000; Fish, 20; Iron, 10]); export = YearlySupply([Wheat, 5; Fish, 10]); utility = UtilityMap []; import = YearlySupply [Silk, 10]; autarchy = 10; surroundingTerrain = []} |> calcTotalSupply |> utilityFromResources

(* let trialRun = 
  printf "%A" (printMap sampleMap) |> ignore
  getCity sampleMap "Antwerp" |> ignore
  let newAntwerp = 
    match getCity sampleMap "Antwerp" with
    | Some a -> configureCityProduction a
    | None -> failwith "antwerp not found"
  let newMap = setCity sampleMap newAntwerp
  getCity newMap "Antwerp" *)