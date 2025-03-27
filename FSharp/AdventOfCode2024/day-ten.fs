namespace AdventOfCode

open DayTenData

module DayTen =
    
    let debug = true

    let intToChar (value : int) : char = char (value + int '0')
    let charToInt (value : char) : int = int value - int '0'
    let findAllAdjacentPathPoints (pathMatrix : string list) (position : matrixPosition) (pathElevationLevel : int) : matrixPosition list = 
        let maxUpperRight = { x = pathMatrix.[0].Length - 1; y = 0; v = '0' }
        let maxLowerLeft = { x = 0; y = pathMatrix.Length - 1; v = '0' }
        let directions = [UP; DOWN; LEFT; RIGHT]
        let nextElevationLevel = 
            match (pathElevationLevel + 1) with 
            | n when n = 10 -> '0'
            | _ -> (pathElevationLevel + 1) |> intToChar
        
        let rec helper (directions : directionType list) (acc : matrixPosition list) =
            match directions with
            | [] -> acc
            | direction::remainingDirections ->
                match direction with
                | UP -> 
                    if ((position.y > maxUpperRight.y) && (pathMatrix.[position.y - 1].[position.x]) = nextElevationLevel) then
                        helper remainingDirections ({ x = position.x; y = position.y - 1; v = (pathMatrix.[position.y - 1].[position.x]) } :: acc)
                    else
                        helper remainingDirections acc
                | DOWN -> 
                    if ((position.y < maxLowerLeft.y) && (pathMatrix.[position.y + 1].[position.x]) = nextElevationLevel) then
                        helper remainingDirections ({ x = position.x; y = position.y + 1; v = (pathMatrix.[position.y + 1].[position.x]) } :: acc)
                    else
                        helper remainingDirections acc
                | LEFT -> 
                    if ((position.x > maxLowerLeft.x) && (pathMatrix.[position.y].[position.x - 1]) = nextElevationLevel) then
                        helper remainingDirections ({ x = position.x - 1; y = position.y; v = (pathMatrix.[position.y].[position.x - 1]) } :: acc)
                    else
                        helper remainingDirections acc
                | RIGHT -> 
                    if ((position.x < maxUpperRight.x) && (pathMatrix.[position.y].[position.x + 1]) = nextElevationLevel) then
                        helper remainingDirections ({ x = position.x + 1; y = position.y; v = (pathMatrix.[position.y].[position.x + 1]) } :: acc)
                    else
                        helper remainingDirections acc
        
        match nextElevationLevel with
        | '0' -> []
        | _ -> helper directions []

    let findAllXPositions (matrixYRow : string) (currentYIndex : int) =
            matrixYRow.ToCharArray()
            |> Array.mapi (fun i x -> if x = '0' then { x = i; y = currentYIndex; v = '0' } else { x = -99; y = -99; v = '0' })
            |> Array.filter (fun x -> x.x <> -99)
            |> Array.toList

    let rec findAllPathStartingPoints (currentMatrixIndex : int) (pathMatrix : string list) (acc : matrixPosition list) =
        match pathMatrix with
        | [] -> acc
        | x::xs -> 
            (findAllXPositions x currentMatrixIndex) @ (findAllPathStartingPoints (currentMatrixIndex + 1) xs acc)

    let rec endingPositions (positionsList : matrixPosition list) (pathMatrix : string list) (acc : matrixPosition list) =
        match positionsList with
        | [] -> acc
        | position::rest -> 
            let positionValue = pathMatrix.[position.y].[position.x]
            let newPoints = findAllAdjacentPathPoints pathMatrix position (positionValue |> charToInt)
            //printfn "Fresh call --- curr position (%A, %A, %A) - newPoints %A" position.x position.y position.v newPoints
            match positionValue with 
                | n when n = '8' -> (newPoints @ acc) |> endingPositions rest pathMatrix
                | _ ->
                    let newAcc = endingPositions newPoints pathMatrix acc 
                    //printfn "POPPED!!! (%A, %A, %A)" position.x position.y position.v            
                    endingPositions rest pathMatrix newAcc
                
    let findPath (positionsList : matrixPosition list) (pathMatrix : string list) : int =
        let mutable pathCount : int list = []
        
        let rec helper (positionsList : matrixPosition list) (pathMatrix : string list) (acc : matrixPosition list) =
            match positionsList with
            | [] -> acc
            | position::remainingPositions ->
                let endingPointsForCurrentPosition = endingPositions [position] pathMatrix []
                let noDuplicates = endingPointsForCurrentPosition |> Set.ofList |> Set.toList
                pathCount <- noDuplicates.Length :: pathCount
                helper remainingPositions pathMatrix (noDuplicates @ acc)

        helper positionsList pathMatrix [] |> ignore
        pathCount |> List.sum

    let sumPathsFound () =
        let matrixTest = matrixReal
        let startingPoints = findAllPathStartingPoints 0 matrixTest []
        if (debug) then printfn "matrix starting points %A" startingPoints.[0]
        findPath startingPoints matrixTest
        