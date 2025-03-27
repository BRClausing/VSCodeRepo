namespace AdventOfCode

open DayNineData

module DayNine =
    let debug = false
    let debugOne = false
    let (|ODD|EVEN|) (value : int) =
        match value with
        | n when (n % 2 = 0) -> EVEN
        | _ -> ODD

    let convertStringToIntList (input : string) : int list = 
        input.ToCharArray() |> Array.toList |> List.map (fun x -> int x - int '0')

    let updateBlockFile (valueToAdd : string) (amtToAdd : int) : string list =
        if (debug) then printfn "valueToAdd: %A, amtToAdd: %A" valueToAdd amtToAdd
        match amtToAdd with
        | 0 -> []
        | _ -> 
            let lastValue = amtToAdd - 1
            [for i in 0..lastValue -> valueToAdd]

    let rec diskMapToDiskBlocks (diskMap : int list) (iteration : int) (id : int) (acc : string list) : string list = 
        if (debug) then printfn "diskMap: %A, acc: %A" diskMap acc
        match diskMap with
        | [] -> acc
        | x::xs ->
            if (debug) then printfn "id: %A" id
            match iteration with
            | 0 | EVEN -> 
                if (debug) then printfn "match case: 0 | EVEN"
                acc @ (updateBlockFile (id.ToString()) x)
                |> diskMapToDiskBlocks xs (iteration + 1) (id + 1) 
            | ODD -> 
                if (debug) then printfn "match case: ODD"
                acc @ (updateBlockFile "." x)
                |> diskMapToDiskBlocks xs (iteration + 1) (id)

    let findNextIndex (searchFromFront : bool) (diskBlocks : string array) (startIndex : int) : int = 
        try
            if (searchFromFront) then
                diskBlocks |> Array.findIndex (fun x -> x = ".")
            else
                let mutable i = startIndex
                let mutable result = -1
                while (i > 0) do
                    if (debug) then printfn "i: %A, diskBlock.[i] = %A" i diskBlocks.[i]
                    if (diskBlocks.[i] <> ".") then
                        result <- i
                        i <- 0
                    else    
                        i <- i - 1
                result
        with
        | _ -> -1

    let findNextFrontIndex = findNextIndex true

    let findNextBackIndex = findNextIndex false

    let rec fileSystemCheckSum (diskBlocks : string list) (iteration : uint64) (acc : uint64 list) : uint64 list = 
        match diskBlocks with
        | [] -> acc
        | x::xs -> 
            if (x = ".") then
                acc 
            else
                let headAsInt : uint64 = x |> uint64
                if (debugOne) then printfn "x: %A headAsInt: %A, iteration: %A, (headAsInt * iteration): %A, acc.Sum: %A" x headAsInt iteration (headAsInt * iteration) (acc |> List.sum)
                fileSystemCheckSum xs (iteration + 1UL) (headAsInt * iteration :: acc)

    let compressDiskBlocks (frontDiskBlocks : string list) (backDiskBlocks : string list) : string list * string list = 
        let frontCompressed = frontDiskBlocks |> List.toArray
        let backCompressed = 
            match backDiskBlocks with
            | [] -> frontCompressed
            | _ -> backDiskBlocks |> List.toArray

        let mutable swappingComplete = false
        let mutable backToFrontIndex = Array.length backCompressed - 1
        
        while swappingComplete = false do
            let frontIndex = findNextFrontIndex frontCompressed 0
            let backIndex = findNextBackIndex backCompressed backToFrontIndex
            match (List.length backDiskBlocks) with
            | n when n > 0 && (frontIndex = -1 || backIndex = -1) -> swappingComplete <- true
            | n when n = 0 && (frontIndex = -1 || backIndex = -1 || frontIndex >= backIndex) -> swappingComplete <- true
            | _ -> 
                if (debug) then printfn "Moving backIndex[%A] = %A to frontIndex[%A] = %A" backIndex backCompressed.[backIndex] frontIndex frontCompressed.[frontIndex]
                frontCompressed.[frontIndex] <- backCompressed.[backIndex]
                backCompressed.[backIndex] <- "."
                backToFrontIndex <- backToFrontIndex - 1
        (frontCompressed |> Array.toList, backCompressed |> Array.toList)

    let rec popValueFromList (diskBlocksList : string list) : string * string list = 
        match diskBlocksList with
        | [] -> ("", [])
        | x::xs ->
            match x with
            | "." -> popValueFromList xs
            | _ -> (x, xs)

    let removeLastElement tailOfList =
        match tailOfList with
        | [] -> []
        | _ -> 
            tailOfList |> List.rev |> List.tail |> List.rev
    
    let rec zipper (diskBlocks : string list) (reversedDiskBlocks : string list) (acc : string list) : string list = 
        match diskBlocks with
        | [] -> acc
        | x::xs ->
            match x with
            | "." -> 
                match reversedDiskBlocks with
                | [] -> acc
                | y::ys ->
                    acc @ [y]
                    |> zipper (xs |> removeLastElement) ys
            | _ ->
                match reversedDiskBlocks with
                | [] -> acc
                | _ ->
                    acc @ [x]
                    |> zipper xs (reversedDiskBlocks |> removeLastElement)
    
    let inPlaceSwappingApproach () =
        let input = puzzleInputReal |> convertStringToIntList
        let fullDiskBlocks = diskMapToDiskBlocks input 0 0 []
        let diskBlockList = fullDiskBlocks |> List.splitInto 2
        let incompleteFront, compressedBack = compressDiskBlocks diskBlockList.[0] diskBlockList.[1]
        let compressedFront, _ = compressDiskBlocks incompleteFront []
        let fullyCompressedDisk = compressedFront @ compressedBack
        fullyCompressedDisk |> List.fold (+) "" |> printfn "fullyCompressedDisk: %A"
        fileSystemCheckSum fullyCompressedDisk 0UL [] |> List.sum
    
    let zipTwoListsApproach () : uint64 = 
        let input = puzzleInputReal |> convertStringToIntList
        let fullDiskBlocks = diskMapToDiskBlocks input 0 0 []
        let fullDiskBlocksReversed = fullDiskBlocks |> List.rev |> List.filter (fun x -> x <> ".")
        let fullyCompressedDisk = zipper fullDiskBlocks fullDiskBlocksReversed []
        fileSystemCheckSum fullyCompressedDisk 0UL [] |> List.sum

    let compressDiskBlocksExposed () =
        inPlaceSwappingApproach ()
