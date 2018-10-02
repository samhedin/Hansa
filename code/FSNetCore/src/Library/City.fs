module City
open System
open Domain
(*
  For dealing with individual cities and managing their production and so on.
*)

let remainingLabor (city : City) =
  let totalProduction = List.fold (fun acc resourceProduction -> acc + (snd resourceProduction)) 0 city.production
  city.population - totalProduction

let printCity (city : City) =
  let productionString (production : YearlySupply) =
    sprintf "\nresource: %-10A\nproduction: %A lp\n" (fst production) (snd production) 
  let exportString (export: YearlySupply) =
    sprintf "\nresource: %-10A\nexport: %A lp\n" (fst export) (snd export) 
  let production = Seq.fold (fun acc product -> acc + productionString product) "" city.production |> string
  let export = Seq.fold (fun acc product -> acc + exportString product) "" city.export |> string

  printf "\n\n=======%A=======\n" city.name
  printf "total population/labor points (lp): %A\nUnused labor: %A\ntotal utility: %A\nautarchy* line: %A\n\n" city.population (remainingLabor city) city.utility city.autarchy
  printf "==Production of goods==\n"
  printf "%A" production
  printf "\n==Export of goods==\n"
  printf "%A" export

  printf "\n\n*Autarchy is the highest level of utility a city can achieve without trade\n"


let configureCity (city : City) = 
  printCity city

  let getUserInput = 
    printf "\nSelect which resource you wish to change. Wheat = w, Fish = f, Iron = i, Silk = s\n"
    let command = Console.ReadLine()
    match command with
    | "w" -> Wheat
    | "f" -> Fish
    | "i" -> Iron
    | "s" -> Silk
    | _ -> failwith "invalid command in getUserInput"
  
  let updateCityProductionForResource (city : City) (resource : Resource) (amount : int) =
    let oldProduction = List.find (fun resourceProd -> fst resourceProd = resource) city.production
    let newProduction = YearlySupply (resource, amount)
    {city with production = newProduction :: (List.except [oldProduction] city.production)}

  let chosenResource = getUserInput
  printf "\nHow many would you like to produce this year? (has to be <= unused labor) "
  let amountToProduce = Console.ReadLine() |> int
  updateCityProductionForResource city chosenResource amountToProduce