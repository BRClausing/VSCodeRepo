namespace AdventOfCode

open ContainerForList1
open ContainerForList2

module DayOne =
    let listLeftSide = listLeftSideReal
    let listRightSide = listRightSideReal

    let similarityScoreByIndexValue (leftSideValue : int) (intList: list<int>) =
        let isEqualTo (x: int) = 
            x = leftSideValue 
        
        let applySimliarityScore (value: int) (length: int) = 
            length * value

        intList 
        |> List.filter isEqualTo
        |> List.length
        |> applySimliarityScore leftSideValue


    let totalDisanceBetweenLists = 
        (List.sort listLeftSide, List.sort listRightSide)
        ||> List.map2 (fun x y -> abs (y - x)) 
    
    let listOfSimilarityScores = 
        List.sort listLeftSide
        |>  List.map (fun x -> similarityScoreByIndexValue (x) (List.sort listRightSide))
