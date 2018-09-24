#load "Domain.fs"
#load "WorldMap.fs"
#load "CreateCities.fs"
#r @"C:\Users\samhe\.nuget\packages\fsrandom\1.4.0.1\lib\netstandard1.6\FsRandom.dll"
open WorldMap
open CreateCities
open Domain
open FsRandom


let stockholm =
  {name = "stockholm"; population = 3; production = [Wheat, {production = 10; export = 5}; Fish, {production = 20; export = 10}]; utility = 10; autarchy = 10; surroundingTerrain = []}