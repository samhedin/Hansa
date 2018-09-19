module Domain

type Resource = Wheat | Fish | Iron | Silk 
type DifficultyMultiplier = double
type Terrain = Ocean | Land | Mountain | River | Forest

let difficulty terrain resource : DifficultyMultiplier =
  match terrain, resource with
  | Ocean, Fish -> 0.5
  | Land, Wheat -> 0.5
  | Mountain, Iron -> 0.5
  | River, Fish -> 0.5
  | _,_ -> failwith ("Difficulty multiplier not implemented for these yet")

type City = {
  population : int
  production : int
  utility : int
  resources : (Resource * DifficultyMultiplier) list
  surroundingTerrain : (Terrain * Terrain * Terrain * Terrain)
}

type League = City list

