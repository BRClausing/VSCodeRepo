namespace AdventOfCode

open DayFiveData

module DayFive =
    
    let debug = false
    let rules = rulesReal
    let updates = updatesReal

    let getRulesForValue valueInUpdate=
        List.filter (fun (y : int list) -> y.[0] = valueInUpdate) rules

    let getRulesThatIncludeValue valueInUpdate =
        List.filter (fun (y : int list) -> y.[1] = valueInUpdate) rules

    let rec doAllRulesApplyForCurrentIndex (update : int list) (updateIndex : int) (rules : int list list) (acc) =
        match rules with
        | [] -> acc
        | rule::rest -> 
            if (debug) then printfn "update: %A, updateIndex: %A, rule: %A, acc: %A" update updateIndex rule acc
            try
                let ruleIndex = List.findIndex (fun x -> x = rule.[1]) update
                (updateIndex < ruleIndex) :: (doAllRulesApplyForCurrentIndex update updateIndex rest acc)
            with
            | :? System.Collections.Generic.KeyNotFoundException -> true :: (doAllRulesApplyForCurrentIndex update updateIndex rest acc)

    let swapValues value ruleValue update =
        try
            let mutable updateAsArray = Array.ofList update
            let fstIndex = List.findIndex (fun x -> x = value) update
            let sndIndex = List.findIndex (fun x -> x = ruleValue) update
            if (debug) then printfn "fstIndex: %A, sndIndex: %A sndIndex < fstIndex = %A" fstIndex sndIndex (sndIndex < fstIndex)
            if (sndIndex < fstIndex) then 
                updateAsArray.[fstIndex] <- ruleValue
                updateAsArray.[sndIndex] <- value
                updateAsArray |> Array.toList
            else 
                update
        with
            | :? System.Collections.Generic.KeyNotFoundException -> update

    let rec applyAllRules (rules : int list list) sortedUpdate = 
        match rules with 
        | [] -> sortedUpdate
        | rule::rest ->
            //applyAllRules (getRulesForValue rule.[1]) sortedUpdate
            (swapValues rule.[0] rule.[1] sortedUpdate) 
            |> applyAllRules rest 
    
    let rec countAllParents rules count =
        match rules with
        | [] -> count
        | rule::rest -> 
            if (debug) then printfn "rule: %A, count: %A" rule count
            countAllParents (getRulesThatIncludeValue rule.[0]) (count + 1)
            |> countAllParents rest
     
    let getIndexValue (updateValue: int) (indexAcc: int) = 
        getRulesThatIncludeValue updateValue
        |> List.length
            
    let rec indexValuePairForUpdate (update : int list) (indexValuePairs : (int * int) list) =
        match update with
        | [] -> indexValuePairs
        | x::xs -> 
            (getIndexValue x 0, x) :: (indexValuePairForUpdate xs indexValuePairs)

    let fixOrderingOfUpdate update acc =
        let indexValuePairs : (int * int) list = indexValuePairForUpdate update []
        indexValuePairs
        |> List.sortBy (fun x -> fst x)
        |> List.map (fun x -> snd x)
        
    let verifyRulesForAnUpdate update checkOnlyValidUpdatesFlag = 
        let mutable rulesApply = true
        let mutable i = 0
        while (i <= List.length update-1 && rulesApply) do
            let rulesForCurrentIndex = List.filter (fun (x : int list) -> x.[0] = update.[i]) rules
            if (debug) then printfn "CurrentIndex: %A, rules = %A" i rulesForCurrentIndex
            rulesApply <- (doAllRulesApplyForCurrentIndex update i rulesForCurrentIndex []) |> List.forall (fun x -> x)
            i <- i + 1

        match rulesApply, checkOnlyValidUpdatesFlag with
        | false, true | true, false -> 0
        | true, true -> 
            let middleIndexValue = update.[List.length update / 2] 
            if (debug) then printfn "This update passes all tests -- middle index value: %A" middleIndexValue
            middleIndexValue
        | false, false -> 
            if (debug) then printfn "Fixing this update: %A" update
            (fixOrderingOfUpdate update []).[List.length update / 2]

    let isUpdateCorrectlyOrdered update = 
        let mutable rulesApply = true
        let mutable i = 0
        while (i <= List.length update-1 && rulesApply) do
            let rulesForCurrentIndex = List.filter (fun (x : int list) -> x.[0] = update.[i]) rules
            if (debug) then printfn "CurrentIndex: %A, rules = %A" i rulesForCurrentIndex
            rulesApply <- (doAllRulesApplyForCurrentIndex update i rulesForCurrentIndex []) |> List.forall (fun x -> x)
            i <- i + 1
        rulesApply

    let verifyAllValidUpdates () = 
        updates 
        |> List.map (fun x -> verifyRulesForAnUpdate x true) 
        |> List.sum

    let verifyAllInvalidUpdates () = 
        updates 
        |> List.map (fun x -> verifyRulesForAnUpdate x false) 
        |> List.sum
