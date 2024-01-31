module ThreeSum
///////////////////////////////////////////////////////////////////////////////////////////
// Given an array nums of n integers, are there elements a, b, c in nums such that a + b + c = 0? Find all unique triplets in the array which gives the sum of zero.
// Note:
//    The solution set must not contain duplicate triplets.
// Example:
//    Given array nums = [-1, 0, 1, 2, -1, -4],
// A solution set is:
//    [
//      [-1, 0, 1],
//      [-1, -1, 2]
//    ]
//
// UPDATE:  This solution updated the threeSum problem by allowing the user to provide the "width" of the sums to be.  3 => threeSum; 2 => twoSum.
//             However, it was observed that the twoSum solutions were not very intersting.  As it turns out, the threeSum problem summed to zero, 
//              but the twoSum problem summed to any amount.  So that was also added.
//
//  The user can provide the width of the arrays to sum and the target sum for the arrays.
//      ThreeSum    => array.length = 3 and a target = 0
//      TwoSum      => array.length = 2 and a target = 4
//
///////////////////////////////////////////////////////////////////////////////////////////

    let rec subListCombo comboListIndexValue subList comboWidth finalComboList = 
        match subList with
        | [] -> finalComboList
        | _::subListTail ->
            let subListTakeSize = (comboWidth - 1)
            match subList.Length with
            | len when len >= subListTakeSize -> 
                (([comboListIndexValue] @ List.take subListTakeSize subList)::finalComboList)
                |> subListCombo comboListIndexValue subListTail comboWidth 
            | _ -> finalComboList
        

    let rec comboList comboWidth listOfNums (acc: int list list) = 
        match listOfNums with
        | [] -> acc
        | indexValue::listOfNumsTail -> 
            acc
            |> subListCombo indexValue listOfNumsTail comboWidth
            |> comboList comboWidth listOfNumsTail

    let rec sumCombo acc singleCombo = 
        match singleCombo with
        | [] -> acc
        | head::tail -> tail |> sumCombo (head + acc)

    let rec sumToTargetComboList (acc: int list list) (target: int) (listOfCombos: int list list) =
        match listOfCombos with
        | [] -> acc
        | head::tail -> 
            let comboSum = head |> sumCombo 0
            if (comboSum = target) then 
                tail
                |> sumToTargetComboList ((List.sort head)::acc) target
            else
                tail
                |> sumToTargetComboList acc target


    let emptyList: int list list = List.Empty
    let threeSumsComboList = comboList 3 [-1; 0; 1; 2; -1; -4]
    let targetToZeroComboList = sumToTargetComboList emptyList 0
    let twoSumsComboList = comboList 2 [-1; 0; 1; 2; -1; -4; 3; -2; 4; 5; -5]
    let targetToFourComboList = sumToTargetComboList emptyList 4

    let threeSumsToZeroList: int list list = 
        emptyList
        |> threeSumsComboList
        |> targetToZeroComboList
        |> List.distinct 

    let twoSumsToFourList: int list list = 
        emptyList
        |> twoSumsComboList
        |> targetToFourComboList
        |> List.distinct 

    let sumsPrinter (sumsList: int list list) = 
        printfn "["
        sumsList 
        |> List.iter (fun x -> 
                        printfn "  %A" [ String.concat ", " (List.map string x) ] |> ignore)
        printfn "]"