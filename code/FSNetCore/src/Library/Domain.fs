module Domain

type Resource = Wheat | Fish | Iron | Silk 
type Terrain = Ocean | Land | Mountain | River | Forest
type YearlySupply = Map<Resource, int>

type City = {
  name : string
  population : int
  total : YearlySupply
  production : YearlySupply
  export : YearlySupply
  import : YearlySupply
  utility : int
  autarchy : int
  surroundingTerrain: Terrain list
}

type League = City list

type Location = {
  x : int
  y : int
}
type Tile = {
  location : Location
  terrain : Terrain
  city : City option
}

type WorldMap = Tile list

type DifficultyMultiplier = double
let resourceDifficulty terrain resource : DifficultyMultiplier =
  match terrain, resource with
  | Ocean, Fish -> 0.5
  | Land, Wheat -> 0.5
  | Mountain, Iron -> 0.5
  | River, Fish -> 0.5
  | _,_ -> failwith ("Difficulty multiplier not implemented for these yet")

let movementDifficulty terrain : DifficultyMultiplier =
  match terrain with
  | Ocean -> 0.5
  | Land -> 1.0
  | Mountain -> 2.0
  | River -> 0.5
  | _ -> failwith ("Difficulty multiplier not implemented for these yet")