module Toska.Program

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

let formatQueryParams (query: seq<string * string option>) =
        query
        |> Seq.map (fun (key, valueOption) ->
            match valueOption with
            | Some value -> sprintf "%s=%s" key value
            | None -> sprintf "%s=" key
        )
        |> String.concat "&" // Use '&' to separate query parameters

let echoRoute =
    choose [
        GET >=> path "/echo" >=> request (fun req ->
            let queryParams = formatQueryParams req.query
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

// Function to create the server configuration
let createConfig bindings =
    { defaultConfig with bindings = bindings }

// Function to create the web application (routes)
let createWebApp () = echoRoute

// Function to start the server (used in production)
let startServer bindings =
    let config = createConfig bindings
    let webApp = createWebApp()
    startWebServer config webApp

let bindings = [ HttpBinding.createSimple HTTP "127.0.0.1" 8080 ]
startServer bindings
