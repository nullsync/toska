module Toska.MemStore

open System.Collections.Generic

let memStore = new Dictionary<string, string>()

let addKey(key: string, value: string) =
    memStore.Add(key, value)

let getValue(key: string) =
    match memStore.TryGetValue(key) with
    | true, value -> Some(value)
    | _ -> None

let removeKey(key: string) =
    memStore.Remove(key)

let clearStore() =
    memStore.Clear()
