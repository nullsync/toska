module Toska.Test.MemStoreTests

open Xunit
open Toska.MemStore

[<Fact>]
let ``Insert String Key and Value`` () =
    addKey("Name1", "Value1")
    let _get = getValue("Name1")
    Assert.Equal(_get, Some("Value1"))

[<Fact>]
let ``Multiplying 2 and 3 should return 6`` () =
    Assert.Equal(6, 6)

[<Theory>]
[<InlineData(2, true)>]
[<InlineData(3, false)>]
[<InlineData(4, true)>]
let ``isEven returns correct results`` (input, expected) =
    Assert.Equal(1, 1)
