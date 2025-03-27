open AdventOfCode

// printfn "%A" (List.sum DayOne.totalDisanceBetweenLists)
// printfn "%A" (List.sum DayOne.listOfSimilarityScores)

// printfn "%A" (DayTwo.totalNumberOfSafeLevels)
// printfn "%A" (DayTwo.totalNumberOfSafeLevelsWithTolerance)

// DayThree.sumForCorruptedInput |> printfn "%A" 
// DayThree.sumForCorruptedInputPartTwo |> printfn "%A" 

// DayFour.countInstancesOfXmasPartTwo () |> printfn "%A"

// Day Five
//DayFive.verifyAllValidUpdates () |> printfn "%A"
// part one answer is correct - 4766

//DaySix.hasOneOfEachDirection DaySixData.testForAllDirections |> printfn "%A"
//DaySix.solutionFound DaySixData.testForAllDirections |> printfn "%A"
//DaySix.updatePositionCount DaySixData.testForAllDirections DaySixData.testPosition |> printfn "%A"
//DaySix.isMatrixASolution DaySixData.testPosition |> printfn "%A"
//DaySix.countSolutionsExposed () |> printfn "%A"

// let sum, nums = DaySeven.parsePuzzleInputRow DaySevenData.puzzleInputTest[1]
// let operators = DaySeven.creatOperatorTemplate (nums.Length - 1)
// printfn "sum: %A, nums: %A, ops: %A" sum nums operators
// DaySeven.applyOperator '+' (nums) |> printfn "%A"
// DaySeven.applyPattern (DaySeven.stringToCharList operators.[0]) (nums) |> printfn "%A"
// printfn "-----------------"
// let results = operators |> List.map (fun x -> DaySeven.applyPattern (DaySeven.stringToCharList x) (nums))
// results |> List.iter (fun x -> printfn "%A" x)
// results |> List.exists (fun x -> x.[0] = sum) |> printfn "%A"
//DaySeven.daySevenPartOneExposed () |> printfn "%A"

// Day Eight - Did not understand the problem

// Day Nine - Two solutions provided the same output, both wrong.
//printfn "%A" DayNineData.puzzleInputReal
//DayNine.compressDiskBlocksExposed () |> printfn "%A"
// yeilds 5437566989104L, which is too low
// another try yields 5480557422100L
// new approach was to "zip" two lists together and then sum the values
// that try yeilds 5480557422100UL - which is the same value as before!

// Day Ten
DayTen.sumPathsFound () |> printfn "%A"