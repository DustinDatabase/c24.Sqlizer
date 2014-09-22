#r @"packages/FAKE.Core/tools/FakeLib.dll"
open Fake

let outputDir = "./output"
let reportDir = "./reports"

Target "Clean" (fun _ ->
    CleanDirs [outputDir; reportDir]
)

Target "Restore" RestorePackages

Target "Compile" (fun _ ->
    !! "*.sln"
        |> MSBuildRelease outputDir "Build"
        |> ignore
)

Target "Test" (fun _ ->
    !! (outputDir @@ "*.Tests.dll")
        |> NUnit (fun p -> { p with
            OutputFile = reportDir @@ "TestResult.xml"
        })
)

"Clean"
    ==> "Restore"
    ==> "Compile"
    ==> "Test"

RunTargetOrDefault "Test"
