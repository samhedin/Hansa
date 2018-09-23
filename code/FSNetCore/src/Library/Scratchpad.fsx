#load "Domain.fs"
#load "WorldMap.fs"
#load "CreateCities.fs"
open WorldMap
open CreateCities
open Domain

//if the city is on the edge of the map, it is considered a neighbor to 
//the city on the other side of the map. The location "wraps around"

let map = WorldMap.worldMap 5 |> CreateCities.addCities |> CreateCities.citiesWithTerrain