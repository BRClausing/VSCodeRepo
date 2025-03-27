namespace AdventOfCode

open System
open System.Text.RegularExpressions
open ContainerForListFour

module DayThree =
    
    let regExPattern = @"mul\(\d{1,3},\d{1,3}\)"
    let multiplicationStartPattern = @"mul("
    let disableCommand = "don't()"
    let enableCommand = "do()"
    
    let listOfCorruptedInput = listOfCorruptedInputPartTwoReal

    let getEnabledIndex (input: string) (startIndex) =
        let enabledIndex = (input.IndexOf(enableCommand, startIndex + 1))
        match enabledIndex with
        | x when x < 0 -> input.Length
        | _ -> enabledIndex
        
    let rec scrubber (input: string) =
        let disabledIndex = input.IndexOf(disableCommand)
        match (disabledIndex) with
        | x when x < 0 -> input
        | _ -> 
            let enabledIndex = getEnabledIndex input disabledIndex 
            let scrubbedInput = input.Remove(disabledIndex, enabledIndex - disabledIndex)
            scrubber scrubbedInput 

    let applyMultiplication (input: string) =
        let splitOutput = input.Split(",")
        let firstNumber = UInt32.Parse(splitOutput.[0].Substring(multiplicationStartPattern.Length))
        let secondNumber = UInt32.Parse(splitOutput.[1].Substring(0, splitOutput.[1].Length - 1))
        (firstNumber * secondNumber)

    let rec parseString (input: string) (regExMatch: Match) (acc: uint list) =
        match regExMatch.Success with
        | true -> 
            let result = applyMultiplication regExMatch.Value
            result :: (parseString input (regExMatch.NextMatch()) acc)
        | false -> acc

    let rec parseCorruptedInput (listOfCorruptedInput: string list) (acc: uint list) =
        match listOfCorruptedInput with
        | [] -> acc
        | x :: xs -> 
            (parseString x (Regex.Match(x, regExPattern)) []) @ (parseCorruptedInput xs acc)

    let rec scrubCorruptedInput (listOfCorruptedInput: string list) (acc: string list) =
        match listOfCorruptedInput with
        | [] -> acc
        | x :: xs -> 
            let scrubbedInput = scrubber x
            scrubCorruptedInput xs (scrubbedInput :: acc)

    let sumForCorruptedInput = 
        parseCorruptedInput listOfCorruptedInput []
        |> List.sum

    let sumScrubbedInput list = 
        parseCorruptedInput list []
        |> List.sum
    
    let sumForCorruptedInputPartTwo = 
        (scrubCorruptedInput listOfCorruptedInput [])
        |> sumScrubbedInput
