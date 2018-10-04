#load "Domain.fs"
open System.Windows.Forms
#load "WorldMap.fs"
#load "CreateCities.fs"
#load "City.fs"
open City
open WorldMap
open CreateCities
open Domain
open System

let otherStockholm =
  {name = "Stockholm"; population = 100; total = YearlySupply [Wheat, 1000; Silk, 1]; production = YearlySupply([Wheat, 10; Fish, 20; Iron, 10]); export = YearlySupply([Wheat, 5; Fish, 10]); utility = 10; import = YearlySupply [Silk, 10]; autarchy = 10; surroundingTerrain = []} |> calcTotalSupply


let sampleMap = WorldMap.worldMap 5 |> CreateCities.addCities

let trialRun = 
  printf "%A" (printMap sampleMap) |> ignore
  getCity sampleMap "Antwerp" |> ignore
  let newAntwerp = 
    match getCity sampleMap "Antwerp" with
    | Some a -> configureCityProduction a
    | None -> failwith "antwerp not found"
  let newMap = setCity sampleMap newAntwerp
  getCity newMap "Antwerp"
