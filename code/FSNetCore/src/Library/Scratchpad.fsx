#load "Domain.fs"
#load "WorldMap.fs"
open Domain

let randomTerrain howMany =
  let max = 5 //Change this depending on the amount of possibilities in Terrain
  let terrain number = 
    match number with
    | 0 -> Terrain.River
    | 1 -> Terrain.Forest
    | 2 -> Terrain.Land
    | 3 -> Terrain.Mountain
    | 4 -> Terrain.Ocean
    | 5 -> Terrain.River
  let random = System.Random()
  let numbers = List.init howMany (fun _ -> random.Next(max))
  List.map terrain numbers

let allLocations size = //The grid is expressed as tuples
  let nums = [0..size - 1]
  let rec createLoc pairs =
    match pairs with
    | (p :: ps) -> {x = snd p; y = fst p} :: createLoc ps
    | [] -> []
  createLoc (List.allPairs nums nums |> List.sort)

let worldMap size : WorldMap =
  let locations = allLocations size //A location is expressed in (x,y)
  let terrain = randomTerrain (size * size) //Need size^2 terrain tiles

  let rec createTiles location terrain =
    match location, terrain with
    | (l :: ls), (t :: ts) -> 
      {location = l; terrain = t; city = None} :: (createTiles ls ts)
    | [],[] -> []
    | _,_ -> failwith "error when creating tiles"
  createTiles locations terrain

let printMap (worldMap : WorldMap) =
  let rec print map =
    match map with
    | (tile :: tiles) when tile.location.x = 0 && tile.city = None -> 
      sprintf "\n[<No city>, %A, (%A,%A)]\t" tile.terrain (tile.location.x) (tile.location.y) + (print tiles)

    | (tile :: tiles) when tile.location.x = 0 -> 
      sprintf "\n[%A, %A, (%A,%A)]\t" tile.city tile.terrain (tile.location.x) (tile.location.y) + (print tiles)

    | (tile :: tiles) when tile.city = None -> 
      sprintf "[<No city>, %A, (%A,%A)]\t" tile.terrain tile.location.x tile.location.y + (print tiles)

    | (tile :: tiles) -> sprintf "[%A, %A, (%A,%A)]\t" tile.city tile.terrain tile.location.x tile.location.y + (print tiles)
    | _ -> "\n\n"
  "\n" + print worldMap

let testPrint = printf "%A" (printMap (worldMap 3))