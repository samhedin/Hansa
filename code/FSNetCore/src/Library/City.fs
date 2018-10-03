module City
open System
open Domain
(*
  For dealing with individual cities and managing their production.
*)

let remainingLabor city =
  let totalProduction = Map.fold (fun acc _ value -> value + acc) 0 city.production
  city.population - totalProduction

let printCity (city: City) =
  let removeMapFromString (str:string) = str.Replace("map","") //Otherwise the string will be 'map [(Wheat, 10);...]' instead of '[(Wheat, 10);...]'

  printf "\n\n=======%A=======\n" city.name
  printf "total population/labor points (lp): %A\nUnused labor: %A\ntotal utility: %A\nautarchy* line: %A\n\n" city.population (remainingLabor city) city.utility city.autarchy

  (sprintf "==Production of goods==\n%A\n" city.production) |> removeMapFromString |> printf "%A"
  (sprintf "\n==Export of goods==\n%A\n" city.export) |> removeMapFromString |> printf "%A"
  (sprintf "\n==Import of goods==\n%A\n" city.import) |> removeMapFromString |> printf "%A"

  printf "\n\n*Autarchy is the highest level of utility a city can achieve without trade\n"


//Call this to let the user configure the production of their city the coming year. 
let rec configureCityProduction city = //Outer loop lets you input commands in sequence.
  let configureCity' city = 
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
    
    let updateCityProductionForResource (city : City) (resource : Resource) (amount : int) = {city with production = city.production.Add(resource, amount)}

    let chosenResource = getUserInput
    printf "\nHow many %A would you like to produce this year? (has to be <= unused labor) " chosenResource
    let amountToProduce = Console.ReadLine() |> int

    updateCityProductionForResource city chosenResource amountToProduce

  let newCity = configureCity' city
  printf "Would you like to change something else?, y/n"
  match Console.ReadLine() with
  | "y" -> configureCityProduction newCity
  | "n" -> newCity