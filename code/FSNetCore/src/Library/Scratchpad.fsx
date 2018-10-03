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
  {name = "stockholm"; population = 100; production = YearlySupply([Wheat, 10; Fish, 20; Iron, 10]); export = YearlySupply([Wheat, 5; Fish, 10]); utility = 10; import = YearlySupply [Silk, 10]; autarchy = 10; surroundingTerrain = []}

let sample = WorldMap.worldMap 5 |> CreateCities.addCities |> printMap
let utility (city: City) = failwith "implement this"

let test = Map<string, int> (["one", 1; "two",2])

test.Add ("one",test.Item "one" + 1)
