namespace AdventOfCode

open ContainerForListThree

module DayTwo =
    let listofLevels = listOfLevelsOfListsOfReportsReal 
    
    let isInitialDirectionAscending (listOfReports: int list) = 
        match listOfReports with
        | [] -> false
        | _::xs when (List.length xs = 0) -> false
        | x::xs -> x < xs.[0]
    
    let isPairSafe (lv: int) (rv: int) (directionIsAscending: bool) = 
        (((lv < rv) = directionIsAscending) && (abs (lv - rv) <= 3) && (abs (lv - rv) > 0))

    let rec isLevelSafe (listOfReports: int list) (directionIsAscending: bool) (acc: bool list) = 
        match listOfReports with
        | [] -> acc
        | x::xs when (List.length xs = 1) -> 
            isPairSafe x xs.[0] directionIsAscending :: acc
        | x::xs ->
            isPairSafe x xs.[0] directionIsAscending :: (isLevelSafe xs directionIsAscending acc)

    // Day two part one - count the number of safe levels    
    let rec statusOfLevelSafety (listOfReports: int list list) (acc: int list) =
        match listOfReports with
        | [] -> acc
        | x::xs ->
            let directionIsAscending = isInitialDirectionAscending x
            let safetyStatus = isLevelSafe x directionIsAscending []
            match (safetyStatus |> List.exists (fun x -> x = false)) with
            | true -> 0 :: (statusOfLevelSafety xs acc)
            | false -> 1 :: (statusOfLevelSafety xs acc)
    
    // Day two part two - allow for one false report per level
    // Take a failed level and apply the dampner to see if it can be made safe - MUST return a 0 or 1 only.
    let rec applyDampner (listOfReports: int list) (currentIndex: int) (maxIndex: int) =
        // return 0 if the level is still not safe after applying the dampner
        // return 1 if the level is safe after applying the dampner
        match currentIndex with
        | x when x = maxIndex -> 0
        | _ ->
            let removeReport = listOfReports |> List.removeAt currentIndex
            let directionIsAscending = isInitialDirectionAscending removeReport
            match ((isLevelSafe removeReport directionIsAscending []) |> List.exists (fun x -> x = false)) with
            | true -> applyDampner listOfReports (currentIndex + 1) maxIndex
            | false -> 1
    
    let rec statusOfLevelSafetyWithTolerance (listOfReports: int list list) (acc: int list) =
        match listOfReports with
        | [] -> acc
        | x::xs ->
            let directionIsAscending = isInitialDirectionAscending x
            let safetyStatus = isLevelSafe x directionIsAscending []
            match (safetyStatus |> List.exists (fun x -> x = false)) with
            | true -> (applyDampner (x) (0) (List.length x)) :: (statusOfLevelSafetyWithTolerance xs acc)
            | false -> 1 :: (statusOfLevelSafetyWithTolerance xs acc)
    
    let totalNumberOfSafeLevels =
        statusOfLevelSafety listofLevels []
        |> List.sum

    let totalNumberOfSafeLevelsWithTolerance =
        statusOfLevelSafetyWithTolerance listofLevels []
        |> List.sum