#load "Domain.fs"
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
  {name = "stockholm"; population = 100; production = [Wheat, 10; Fish, 20;]; export = [Wheat, 5; Fish,10]; utility = 10; autarchy = 10; surroundingTerrain = []}

let sample = WorldMap.worldMap 5 |> CreateCities.addCities |> printMap


