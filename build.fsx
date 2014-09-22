#r @"packages/FAKE.Core/tools/FakeLib.dll"
open Fake

let version = "1.0.3"

let outputDir = "./output"
let reportDir = "./reports"

let dropDir = outputDir @@ "bin"
let packDir = outputDir @@ "pkg"

let nugetParams = (fun p ->
    { p with
        Authors = [ "CHECK24 Development Team" ]
        Project = "c24.Sqlizer"
        Description = "Tool that runs *.sql files against database. Use it for schema and/or data migration."
        OutputPath = packDir
        Summary = "Tool that runs *.sql files against database. Use it for schema and/or data migration."
        WorkingDir = dropDir
        Version = version
        Files = [ ("*.*", Some "tools", None) ]
    }
)

Target "Clean" (fun _ ->
    CleanDirs [outputDir; reportDir]
)

Target "Restore" RestorePackages

Target "Compile" (fun _ ->
    !! "*.sln"
        |> MSBuildRelease dropDir "Build"
        |> ignore
)

Target "Test" (fun _ ->
    !! (dropDir @@ "*.Tests.dll")
        |> NUnit (fun p -> { p with
            OutputFile = reportDir @@ "TestResult.xml"
        })
)

Target "Package" (fun _ ->
    CleanDir packDir
    NuGet nugetParams "c24.Sqlizer.nuspec"
)

Target "Publish" (fun _ ->
    NuGetPublish nugetParams
)

"Clean"
    ==> "Restore"
    ==> "Compile"
    ==> "Test"

"Package"
    ==> "Publish"

RunTargetOrDefault "Test"
