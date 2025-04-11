open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

// Define the /echo route to handle POST requests
let echoRoute =
    choose [
        GET >=> path "/echo" >=> request (fun req ->
            let queryParams = 
                req.query 
                |> Seq.map (fun (key, valueOption) -> 
                    match valueOption with
                    | Some value -> sprintf "%s=%s" key value
                    | None -> sprintf "%s=" key
                )
                |> String.concat "&" // Use '&' to separate query parameters
            OK queryParams
        )
        POST >=> path "/echo" >=> request (fun req ->
            let data = req.rawForm |> System.Text.Encoding.UTF8.GetString
            if not (System.String.IsNullOrWhiteSpace(data)) then
                OK data
            else
                BAD_REQUEST "No data received"
        )
    ]

// Configure the server
let config =
    { defaultConfig with
        bindings = [ HttpBinding.createSimple HTTP "127.0.0.1" 8080 ] }

// Start the server
startWebServer config echoRoute

