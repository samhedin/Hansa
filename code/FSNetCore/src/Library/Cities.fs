module Cities
open Domain
open WorldMap

let cityNames = ["Antwerp"; "Amsterdam"; "Stockholm"; "Prague"; "Rothenburg"; "Mont Saint Michel"; "Edinburgh"; "Colmar"; "York"; "Siena"]

let createCities =
  List.map (fun name' -> {name = name'; population = 10; production = 0; utility = 0; autarchy = 0; }) cityNames

let addCities worldMap : WorldMap = 
  let cities = createCities
  let rec add wm cities randNum =
    let rnd = System.Random()
    let nextRand = rnd.Next(randNum)
    match (wm: WorldMap), cities with
    | (tile :: tiles), (c :: cs) when nextRand % 3 = 0 -> {tile with city = Some c} :: add tiles cs nextRand
    | _, _ -> []
  add (WorldMap.worldMap 5) cities 1

let mapWithCities = addCities WorldMap.worldMap