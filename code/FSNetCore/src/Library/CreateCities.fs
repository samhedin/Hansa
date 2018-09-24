module CreateCities
open Domain
open WorldMap
(*
  
*)

let cityNames = ["Antwerp"; "Amsterdam"; "Stockholm"; "Prague"; "Rothenburg"; "Edinburgh"; "Colmar"; "York"; "Siena"]

let createCities =
  let allDefaultResources =
    let rp resource : Resource * ProductExport = resource, {production = 0; export = 0}
    [rp Wheat; rp Fish; rp Iron; rp Silk]
  List.map (fun name' -> {name = name'; population = 10; production = allDefaultResources; utility = 0; autarchy = 0; surroundingTerrain = []}) cityNames

let addCities worldMap : WorldMap = 
  let cities = createCities
  let rec add wm cities randNum =
    let rnd = System.Random()
    let nextRand = rnd.Next(randNum)

    match wm, cities with
    | (tile :: tiles), (c :: cs) when nextRand % 5 = 0 && tile.terrain <> Terrain.Ocean && tile.terrain <> Terrain.River -> 
      {tile with city = Some c} :: add tiles cs nextRand

    | (tile :: tiles), cs -> 
      tile :: add tiles cs nextRand

    | _, _ -> []
  add worldMap cities 1


//if the city is on the edge of the map, it is considered a neighbor to 
//the city on the other side of the map. The location "wraps around"
let addSurroundingTerrainToCity (worldMap : WorldMap) tile =
  let sizeOfMap = WorldMap.sizeOfMap worldMap - 1 //indexes start at 0 LMAO xD
  let terrainAtLoc = WorldMap.terrainAtLocation worldMap
  let (Some city) = tile.city
  match tile.location.x,tile.location.y with
  | x, y when x <= 0 && y <= 0 -> //top left
    let xWrap = sizeOfMap
    let yWrap = sizeOfMap
    {city with surroundingTerrain = [terrainAtLoc {x = xWrap; y = y}; terrainAtLoc {x = x; y = yWrap}; terrainAtLoc {x = x + 1; y = y}; terrainAtLoc {x = x; y = y + 1}]}

  | x, y when x > 0 && y > 0 && x < sizeOfMap && y < sizeOfMap -> //middle middle
    {city with surroundingTerrain = [terrainAtLoc {x = x + 1; y = y}; terrainAtLoc {x = x - 1; y = y}; terrainAtLoc {x = x; y = y + 1}; terrainAtLoc {x = x; y = y - 1}]}

  | x, y when x <= 0 && y > 0 && y < sizeOfMap -> //middle left
    let xWrap = sizeOfMap
    {city with surroundingTerrain = [terrainAtLoc {x = x + 1; y = y}; terrainAtLoc {x = xWrap; y = y}; terrainAtLoc {x = x; y = y + 1}; terrainAtLoc {x = x; y = y - 1}]}
  
  | x, y when x > 0 && y <= 0 && x < sizeOfMap -> //middle top
    let yWrap = sizeOfMap
    {city with surroundingTerrain = [terrainAtLoc {x = x + 1; y = y}; terrainAtLoc {x = x - 1; y = y}; terrainAtLoc {x = x; y = y + 1}; terrainAtLoc {x = x; y = yWrap}]}

  | x, y when x >= sizeOfMap && y >= sizeOfMap -> //bot right
    {city with surroundingTerrain = [terrainAtLoc {x = 0; y = y}; terrainAtLoc {x = x - 1; y = y}; terrainAtLoc {x = x; y = 0}; terrainAtLoc {x = x; y = y - 1}]}

  | x, y when y >= sizeOfMap && x <= 0 -> //bot left
    {city with surroundingTerrain = [terrainAtLoc {x = 0; y = y}; terrainAtLoc {x = x - 1; y = y}; terrainAtLoc {x = x; y = 0}; terrainAtLoc {x = x; y = y - 1}]}

  |x, y when x >= sizeOfMap && y <= 0 -> // top right
    {city with surroundingTerrain = [terrainAtLoc {x = 0; y = y}; terrainAtLoc {x = x - 1; y = y}; terrainAtLoc {x = x; y = sizeOfMap}; terrainAtLoc {x = x; y = y + 1}]}

  |x, y when x >= sizeOfMap && y > 0 && y < sizeOfMap -> // middle right
    {city with surroundingTerrain = [terrainAtLoc {x = 0; y = y}; terrainAtLoc {x = x - 1; y = y}; terrainAtLoc {x = x; y = y + 1}; terrainAtLoc {x = x; y = y - 1}]}

  |x, y when x < sizeOfMap && x > 0 && y >= sizeOfMap  -> // middle bot
    {city with surroundingTerrain = [terrainAtLoc {x = x - 1; y = y}; terrainAtLoc {x = x + 1; y = y}; terrainAtLoc {x = x; y = y - 1}; terrainAtLoc {x = x; y = 0}]}

//We want to add all surrounding terrain of a city to the terrain list in that city.


let citiesWithTerrain (worldMap : WorldMap) : WorldMap =
  let hasCity tile =
    match tile.city with
    | Some city -> true
    | None -> false
  List.map (fun (tile: Tile) -> if hasCity tile then {tile with city = Some (addSurroundingTerrainToCity worldMap tile)} else tile) worldMap
  
let printCityResources (worldMap : WorldMap): string =
  let rec printm wm acc =
    match wm with
    | t :: ts ->
      match t.city with
      | None -> printm ts acc
      | Some c -> printm ts ((sprintf "%A: terrain: %A\n\n" c.name c.surroundingTerrain) + acc)
    | [] -> acc
  printm worldMap "\n"