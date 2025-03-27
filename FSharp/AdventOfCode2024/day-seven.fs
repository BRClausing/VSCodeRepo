namespace AdventOfCode

open System
open DaySevenData

module DaySeven =

    let twoIntsToOneInt (x : Int64) (y : Int64) : Int64 = 
        x.ToString() + y.ToString() |> Int64.Parse
    let stringListToInt64List = List.map (fun x -> Int64.Parse(x))
    let stringToCharList (input : string) : char list = input.ToCharArray() |> Array.toList
    let applyOperator operator operands =
         match operator, operands with
            | '+', x::y::rest -> (x + y :: rest)
            | '*', x::y::rest -> (x * y :: rest)
            | '|', x::y::rest -> (twoIntsToOneInt x y :: rest)
            | _, _ -> operands

    let rec applyPattern operatorPattern nums =
        match operatorPattern with
        | [] -> nums
        | head::tail -> 
            let result = applyOperator head nums
            applyPattern tail result

    let creatOperatorTemplate numberOfOperands =
        // given the number of operands, provide all the possible combinations of operators using '+' and '*'
        let rec createOperatorTemplateHelper numberOfOperands acc =
            if numberOfOperands = 0 then
                acc
            else
                let newAcc = 
                    acc 
                    |> List.collect (fun x -> 
                        allOperators 
                        |> List.map (fun y -> x + y))
                createOperatorTemplateHelper (numberOfOperands - 1) newAcc
        createOperatorTemplateHelper numberOfOperands [""]

    let parsePuzzleInputRow (input : string) : Int64 * Int64 list =
        input  
        |> fun (s : string) -> s.Split( ":" )
        |> fun (l : string array) -> 
            let sumValue = (l.[0].Trim()) |> Int64.Parse
            let inputList = l.[1].Trim() |> fun (s : string) -> s.Split(" ") |> Array.toList |> stringListToInt64List
            (sumValue, inputList )

    let rec sumAllPatterns (puzzleInput : string list) (acc : Int64) =
        match puzzleInput with
        | [] -> acc
        | head::tail -> 
            let sum, nums = parsePuzzleInputRow head
            let operators = creatOperatorTemplate (nums.Length - 1)
            let results = operators |> List.map (fun x -> applyPattern (stringToCharList x) (nums))
            if (results |> List.exists (fun x -> x.[0] = sum)) then
                sumAllPatterns tail (acc + sum)
            else
                sumAllPatterns tail acc

    let daySevenPartOneExposed unit : Int64 =
        sumAllPatterns puzzleInputReal 0


