// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    printfn ""
    printfn "Three Sums to Zero List:"
    ThreeSum.sumsPrinter ThreeSum.threeSumsToZeroList
    printfn ""
    printfn "Two Sums to Four List:"
    ThreeSum.sumsPrinter ThreeSum.twoSumsToFourList
    0 // return an integer exit code
