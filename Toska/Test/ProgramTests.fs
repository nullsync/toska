module Toska.Test.ProgramTests

open NUnit.Framework
open FsUnit
open Toska.Program

[<TestFixture>]
type ProgramTests() =

    [<Test>]
    member _.``Should format query parameters correctly``() =
        let query = [ ("key1", Some "value1"); ("key2", None) ]
        let result = formatQueryParams query
        result |> should equal "key1=value1&key2="
