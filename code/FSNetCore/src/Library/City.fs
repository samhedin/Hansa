module City
open Domain
(*
  For dealing with individual cities and managing their production and so on.
*)

let printCity (city : City) =
  let productionString (product : Resource * ProductExport) =
    let productExport = snd product
    let resource = fst product
    sprintf "\nresource: %-10A\tproduction: %A\texport: %A\n" resource productExport.production productExport.export
  let production = Seq.fold (fun acc product -> acc + productionString product) "" city.production |> string

  printf "\n\n=======%A=======\n" city.name
  printf "total utility: %A\nautarchy* line: %A\n\n" city.utility city.autarchy
  printf "----Production of goods----\n"
  printf "%A" production

  printf "\n\n*Autarchy is the highest level of utility a city can achieve without trade\n"