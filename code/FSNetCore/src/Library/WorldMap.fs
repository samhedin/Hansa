module WorldMap
open Domain
//The world map consists of nxn tiles. Each tile has a terrain and optionally a city.

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

let grid size = //The grid is expressed as tuples
  let nums = [0..size - 1]
  List.allPairs nums nums

//TODO: update the addCity row in worldMap to actually add a city.
let worldMap size =
  let locations = grid size //A location is expressed in (x,y)
  let terrain = randomTerrain (size * size) //Need size^2 terrain tiles
  
  let rec createTiles location terrain (rand: System.Random)  =
    //These are to decide when to add a city or not
    let randomNumber = rand.Next()
    let addCity = randomNumber % 3 = 0
    let newRand = System.Random(randomNumber)

    match location, terrain with
    | (l :: ls), (t :: ts) when addCity -> {location = l; terrain = t; city = None} :: (createTiles ls ts newRand)
    | (l :: ls), (t :: ts) -> {location = l; terrain = t; city = None} :: (createTiles ls ts newRand)
    | [],[] -> []
    | _,_ -> failwith "error when creating tiles"
  createTiles locations terrain
