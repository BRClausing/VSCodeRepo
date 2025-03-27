namespace AdventOfCode

open DayFourData

module DayFour =
    
    let debug = false
    let matrix = matrixRealPartOne
    let matrixUpperLeftCoords = (0,0)
    // Matrix access is down first, then across, so set up the x, y coordinates accordingly
    let matrixLowerRightCoords = (List.length matrix - 1, matrix.[0].Length - 1)
    if (debug) then
        printfn "matrixUpperLeftCoords = %A" matrixUpperLeftCoords
        printfn "matrixLowerRightCoords = %A" matrixLowerRightCoords
    let checkMatrixAtCoordinates (matrix: string list) (coords: int * int) (value: char) =
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        let matrixUpperLeftY, matrixUpperLeftX = matrixUpperLeftCoords
        let matrixLowerRightY, matrixLowerRightX = matrixLowerRightCoords

        if y >= matrixUpperLeftY && x >= matrixUpperLeftX && y <= matrixLowerRightY && x <= matrixLowerRightX  then
            if (debug) then printfn "matrix.[%A].[%A] = %A" y x matrix.[y].[x]
            matrix.[y].[x] = value
        else
            if (debug) then
                printfn "y = %A, x = %A, matrixUpperLeftY = %A, matrixUpperLeftX = %A, matrixLowerRightY = %A, matrixLowerRightX = %A" y x matrixUpperLeftY matrixUpperLeftX matrixLowerRightY matrixLowerRightX
                printfn "y >= matrixUpperLeftY (%A) x >= matrixUpperLeftX (%A) y <= matrixLowerRightY (%A) x <= matrixLowerRightX (%A)" (y >= matrixUpperLeftY) (x >= matrixUpperLeftX) (y <= matrixLowerRightY) (x <= matrixLowerRightX)
                printfn "matrix.[%A].[%A] = BOOM! -- matrix.UL(%A,%A) matrix.LR(%A,%A)" y x matrixUpperLeftY matrixUpperLeftX matrixLowerRightY matrixLowerRightX
            false

    let rightDownCount matrix coords  = 
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        if (checkMatrixAtCoordinates matrix (y + 1, x + 1) 'M') && (checkMatrixAtCoordinates matrix (y + 2, x + 2) 'A') && (checkMatrixAtCoordinates matrix (y + 3, x + 3) 'S') then
            1
        else
            0

    let rightUpCount matrix coords =
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        if (checkMatrixAtCoordinates matrix (y - 1, x + 1) 'M') && (checkMatrixAtCoordinates matrix (y - 2, x + 2) 'A') && (checkMatrixAtCoordinates matrix (y - 3, x + 3) 'S') then
            1
        else
            0
    
    let rightCount matrix coords =
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        if (checkMatrixAtCoordinates matrix (y, x + 1) 'M') && (checkMatrixAtCoordinates matrix (y, x + 2) 'A') && (checkMatrixAtCoordinates matrix (y, x + 3) 'S') then
            1
        else
            0
    
    let leftDownCount matrix coords =
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        if (checkMatrixAtCoordinates matrix (y + 1, x - 1) 'M') && (checkMatrixAtCoordinates matrix (y + 2, x - 2) 'A') && (checkMatrixAtCoordinates matrix (y + 3, x - 3) 'S') then
            1
        else
            0

    let leftUpCount matrix coords =
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        if (checkMatrixAtCoordinates matrix (y - 1, x - 1) 'M') && (checkMatrixAtCoordinates matrix (y - 2, x - 2) 'A') && (checkMatrixAtCoordinates matrix (y - 3, x - 3) 'S') then
            1
        else
            0

    let leftCount matrix coords = 
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        if (checkMatrixAtCoordinates matrix (y, x - 1) 'M') && (checkMatrixAtCoordinates matrix (y, x - 2) 'A') && (checkMatrixAtCoordinates matrix (y, x - 3) 'S') then
            1
        else
            0
    
    let upCount matrix coords = 
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        if (checkMatrixAtCoordinates matrix (y - 1, x) 'M') && (checkMatrixAtCoordinates matrix (y - 2, x) 'A') && (checkMatrixAtCoordinates matrix (y - 3, x) 'S') then
            1
        else
            0

    let downCount matrix coords = 
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        if (checkMatrixAtCoordinates matrix (y + 1, x) 'M') && (checkMatrixAtCoordinates matrix (y + 2, x) 'A') && (checkMatrixAtCoordinates matrix (y + 3, x) 'S') then
            1
        else
            0
    let crissCrossCount matrix coords = 
        // Matrix access is down first, then across, so set up the x, y coordinates accordingly
        let y, x = coords
        if ((checkMatrixAtCoordinates matrix (y + 1, x + 1) 'M') && (checkMatrixAtCoordinates matrix (y - 1, x - 1) 'S')
            || (checkMatrixAtCoordinates matrix (y + 1, x + 1) 'S') && (checkMatrixAtCoordinates matrix (y - 1, x - 1) 'M')) then
            if ((checkMatrixAtCoordinates matrix (y + 1, x - 1) 'M') && (checkMatrixAtCoordinates matrix (y - 1, x + 1) 'S')
                || (checkMatrixAtCoordinates matrix (y + 1, x - 1) 'S') && (checkMatrixAtCoordinates matrix (y - 1, x + 1) 'M')) then
                1
            else
                0
        else
            0

    let countInstancesOfXmas () : int =
        let yMin, xMin = matrixUpperLeftCoords
        let yMax, xMax = matrixLowerRightCoords
        if (debug) then printfn "for y in %A..(%A) and for x in %A..(%A)" yMin yMax xMin xMax
        let mutable count: int list = []
        for y in yMin..(yMax) do
            for x in xMin..(xMax) do
                if (debug) then printfn "checking for X at [%A].[%A]" y x
                if (matrix.[y].[x] = 'X') then 
                    if (debug) then printfn "found X at [%A].[%A]" y x
                    count <- (rightDownCount matrix (y, x)) :: count
                    count <- (rightUpCount matrix (y, x)) :: count
                    count <- (rightCount matrix (y, x)) :: count
                    count <- (leftDownCount matrix (y, x)) :: count
                    count <- (leftUpCount matrix (y, x)) :: count
                    count <- (leftCount matrix (y, x)) :: count
                    count <- (upCount matrix (y, x)) :: count
                    count <- (downCount matrix (y, x)) :: count
        count |> List.sum

    let countInstancesOfXmasPartTwo () : int =
        let yMin, xMin = matrixUpperLeftCoords
        let yMax, xMax = matrixLowerRightCoords
        if (debug) then printfn "for y in %A..(%A) and for x in %A..(%A)" yMin yMax xMin xMax
        let mutable count: int list = []
        for y in yMin..(yMax) do
            for x in xMin..(xMax) do
                if (debug) then printfn "checking for A at [%A].[%A]" y x
                if (matrix.[y].[x] = 'A') then 
                    if (debug) then printfn "found A at [%A].[%A]" y x
                    count <- (crissCrossCount matrix (y, x)) :: count
        count |> List.sum