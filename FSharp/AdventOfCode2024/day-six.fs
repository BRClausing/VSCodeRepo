namespace AdventOfCode

open DaySixData

module DaySix =
    
    let debug = false
    let specificDebug = false
    let _thresholdValue = 15
    let mutable _matrix : string array = matrixReal

    let (|OOB|FREE|BLOCK|IGNORE|) (matrixValue : char) =
        match matrixValue with
        | 'O' -> OOB
        | '.' -> FREE
        | 'X' -> IGNORE
        | '#' -> BLOCK
        | _ -> failwith "Invalid matrix value"

    let countDirection direction listOfPositionsAndCounts =
        listOfPositionsAndCounts |> List.filter (fun (position, count) -> position.direction = direction) |> List.length
    
    let hasOneOfEachDirection (listOfPositionsAndCounts : list<matrixPosition * int>) : bool = 
        let rightCount = countDirection RIGHT listOfPositionsAndCounts
        let upCount = countDirection UP listOfPositionsAndCounts
        let downCount = countDirection DOWN listOfPositionsAndCounts
        let leftCount = countDirection LEFT listOfPositionsAndCounts
        rightCount > 0 && upCount > 0 && downCount > 0 && leftCount > 0

    let solutionFound listOfPositionsAndCounts =
        try
            let _, maxCount = listOfPositionsAndCounts |> List.maxBy (fun (_, count) -> count)
            if (maxCount < _thresholdValue) then
                false
            else
                if (debug) then 
                    printfn "Max count: %A" maxCount
                    List.filter (fun (position, count) -> count = maxCount) listOfPositionsAndCounts |> printfn "%A"
            
                listOfPositionsAndCounts
                |> List.filter (fun (position, count) -> count = maxCount)
                |> hasOneOfEachDirection
        with
            | :? System.ArgumentException -> false

    let updatePositionCount listOfPositionsAndCounts currPosition = 
        if ((List.tryFind (fun (position, _) -> position.y = currPosition.y && position.x = currPosition.x && position.direction = currPosition.direction) listOfPositionsAndCounts) = None) then
            (currPosition, 1) :: listOfPositionsAndCounts
        else
            listOfPositionsAndCounts 
            |> List.map (fun (position, count) -> if (position.x = currPosition.x && position.y = currPosition.y && position.direction = currPosition.direction) then (position, count + 1) else (position, count))
    
    let updateMatrix position =
        let mutable row = _matrix.[position.y].ToCharArray()
        row.[position.x] <- 'X'
        _matrix.[position.y] <- row |> System.String

    let newMatrixWithNewObsticle markAsIgnorePosition markAsBlockPosition =
        let mutable ignoreRow = _matrix.[markAsIgnorePosition.y].ToCharArray()
        ignoreRow.[markAsIgnorePosition.x] <- 'X'
        _matrix.[markAsIgnorePosition.y] <- ignoreRow |> System.String

        let mutable blockRow = _matrix.[markAsBlockPosition.y].ToCharArray()
        blockRow.[markAsBlockPosition.x] <- '#'
        _matrix.[markAsBlockPosition.y] <- blockRow |> System.String
        _matrix
        
    let findInitialMatrixPosition =
        let mutable initialPosition = { x = 0; y = 0; direction = UP }
        for y = 0 to ((Array.length _matrix) - 1) do
            for x = 0 to ((String.length _matrix.[y]) - 1) do
                if (_matrix.[y].[x] = '^') then
                    initialPosition <- { x = x; y = y; direction = UP }
        if (debug) then printfn "Initial position: %A" initialPosition
        updateMatrix initialPosition
        initialPosition
    
    let rotateDirection (position : matrixPosition) =
        match position.direction with
        | UP -> { x = position.x; y = position.y + 1; direction = RIGHT }
        | DOWN -> { x = position.x; y = position.y - 1; direction = LEFT }
        | LEFT -> { x = position.x + 1; y = position.y; direction = UP }
        | RIGHT -> { x = position.x - 1; y = position.y; direction = DOWN }

    let findNextValue (position : matrixPosition) (localMatrix : string array) = 
        let newPosition = 
            match position.direction with
            | UP -> { x = position.x; y = position.y - 1; direction = UP }
            | DOWN -> { x = position.x; y = position.y + 1; direction = DOWN }
            | LEFT -> { x = position.x - 1; y = position.y; direction = LEFT }
            | RIGHT -> { x = position.x + 1; y = position.y; direction = RIGHT }
        if (newPosition.y < 0 || newPosition.y >= (Array.length localMatrix) || newPosition.x < 0 || newPosition.x >= (String.length localMatrix.[newPosition.y])) then
            if (debug) then printfn "Out of bounds"
            ('O', newPosition)
        else 
            if (debug) then printfn "New position: %A with value [%A]" newPosition localMatrix.[newPosition.y].[newPosition.x]
            (localMatrix.[newPosition.y].[newPosition.x], newPosition)

    let countMovesMade (position : matrixPosition) =
        let mutable movesMade = 1
        let mutable currentPosition = position
        let mutable moreMovesToMake = true
        while moreMovesToMake do   
            let nextValue, nextPosition = findNextValue currentPosition _matrix
            match nextValue with
            | FREE ->
                movesMade <- movesMade + 1
                currentPosition <- nextPosition
                updateMatrix currentPosition
                if (debug) then printfn "Free space - current count: %A" movesMade
            | BLOCK ->
                currentPosition <- rotateDirection nextPosition
            | OOB ->
                moreMovesToMake <- false
            | IGNORE ->
                currentPosition <- nextPosition 
        if (debug) then printfn "The Matrix: %A" _matrix
        movesMade

    let isMatrixASolution (position : matrixPosition) =
        let mutable currentPosition = position
        let mutable moreMovesToMake = true
        let mutable matrixIsASolution = false
        let mutable listOfPositionsAndCounts : list<matrixPosition * int> = []
        let mutable movesMade = 0
        while moreMovesToMake do   
            movesMade <- movesMade + 1
            if (specificDebug) then 
                printfn "Current iteration on the board: %A" movesMade
            let nextValue, nextPosition = findNextValue currentPosition _matrix
            match nextValue with
            | FREE ->
                currentPosition <- nextPosition
                printfn "SUB SEARCH - Free space - current count: %A" movesMade
            | BLOCK ->
                printfn "SUB SEARCH - Block space [%A,%A]- current count: %A" currentPosition.y currentPosition.x movesMade
                printfn "SUB SEARCH - Block space - Checking solutions in: %A" listOfPositionsAndCounts
                if (specificDebug) then 
                    //printfn "Blocker found at (%A,%A)" currentPosition.y currentPosition.x
                    printfn "Checking solutions in: %A" listOfPositionsAndCounts
                    //printfn "The Matrix: %A" _matrix
                currentPosition <- rotateDirection nextPosition
                if (solutionFound listOfPositionsAndCounts) then
                    moreMovesToMake <- false
                    matrixIsASolution <- true
                else
                    listOfPositionsAndCounts <- updatePositionCount listOfPositionsAndCounts currentPosition
            | OOB ->
                printfn "SUB SEARCH - OOB space - current count: %A" movesMade
                moreMovesToMake <- false
            | IGNORE ->
                printfn "SUB SEARCH - Ignore space - current count: %A" movesMade
                currentPosition <- nextPosition 
        matrixIsASolution

    let countSolutions (position : matrixPosition) (partOneMatrix : string array) =
        let mutable solutionsFound = 0
        let mutable currentPosition = position
        let mutable moreSolutionsToFind = true
        let mutable movesLeftToTry = 5177
        while moreSolutionsToFind do
            let nextValue, nextPosition = findNextValue currentPosition partOneMatrix
            match nextValue with
            | FREE ->
                moreSolutionsToFind <- false
                if (debug) then printfn "Free space - Should never happen!"
            | BLOCK ->
                currentPosition <- rotateDirection nextPosition
                movesLeftToTry <- movesLeftToTry - 1 
                printfn "Reduced moves left to try - now at: %A" movesLeftToTry
            | OOB ->
                moreSolutionsToFind <- false
            | IGNORE ->
                _matrix <- newMatrixWithNewObsticle currentPosition nextPosition
                if (isMatrixASolution currentPosition) then
                    solutionsFound <- solutionsFound + 1
                    printfn "Solution found - total found: %A" solutionsFound 
                currentPosition <- nextPosition
                movesLeftToTry <- movesLeftToTry - 1 
                printfn "Reduced moves left to try - now at: %A" movesLeftToTry
        if (debug) then printfn "The counting Matrix: %A" _matrix
        solutionsFound

    let countMovesMadeExposed () = countMovesMade findInitialMatrixPosition
    
    let countSolutionsExposed () = 
       let initialPosition = findInitialMatrixPosition
       // Configure the global _matrix data by running the part one code
       countMovesMade initialPosition |> printfn "%A"
       countSolutions initialPosition (Array.copy _matrix) |> printfn "Solutions found: %A" 
