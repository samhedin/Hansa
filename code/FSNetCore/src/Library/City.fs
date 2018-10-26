module City
open System
open Domain
//For dealing with individual cities and managing their production.

let remainingLabor city =
  let totalProduction = Map.fold (fun acc _ value -> value + acc) 0 city.production
  city.population - totalProduction

//Sums production, export and import into the total YearlySupply
let calcTotalSupply (city : City) : City =

  let addVals _ (v : int) (v' : int) = v + v'
  let subVals _ (v : int) (v' : int) = v - v'
  
  //Merges the hashmap and uses decide funciton in case of collision.
  let unionWith (decide : Resource -> int -> int -> int) (m1: YearlySupply) (m2: YearlySupply) =
    let add m k v1 =
        let v = match Map.tryFind k m with
                | None    -> v1
                | Some v2 -> decide k v1 v2
        Map.add k v m
    Map.fold add m1 m2

  let productionAndImport = unionWith addVals city.production city.import
  let total = unionWith subVals city.export productionAndImport
  {city with total = total}


let printCity (city: City) =
  let removeMapFromString (str:string) = str.Replace("map","") //Otherwise the string will be 'map [(Wheat, 10);...]' instead of '[(Wheat, 10);...]'

  printf "\n\n=======%A=======\n" city.name
  printf "total population/labor points (lp): %A\nUnused labor: %A\nautarchy* line: %A\n\n" city.population (remainingLabor city) city.autarchy

  printf "\n\n=======Utility=======\n"
  (sprintf "%A" city.utility) |> removeMapFromString |> printf "%A\n"

  (sprintf "==Total supply of goods==\n%A\n" city.total) |> removeMapFromString |> printf "%A"
  (sprintf "==Production of goods==\n%A\n" city.production) |> removeMapFromString |> printf "%A"
  (sprintf "\n==Export of goods==\n%A\n" city.export) |> removeMapFromString |> printf "%A"
  (sprintf "\n==Import of goods==\n%A\n" city.import) |> removeMapFromString |> printf "%A"

  printf "\n\n*Autarchy is the highest level of utility a city can achieve without trade\n"


//Call this to let the user configure the production of their city the coming year. 
let rec configureCityProduction city = //Outer loop lets you input commands in sequence.
  let configureCity' city =
    printCity city |> ignore

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

    updateCityProductionForResource city chosenResource amountToProduce |> calcTotalSupply

  let newCity = configureCity' (calcTotalSupply city)
  printf "Would you like to change something else?, y/n "
  match Console.ReadLine() with
  | "y" -> configureCityProduction newCity
  | "n" -> newCity
  | _  -> failwith "command not found"

