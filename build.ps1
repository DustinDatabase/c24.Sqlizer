$NUGET='.nuget\nuget.exe'
$FAKE='packages\FAKE.Core\tools\FAKE.exe'

if (-not (test-path $FAKE)) {
    & $NUGET install FAKE.Core -ExcludeVersion -NonInteractive -OutputDirectory packages
}
& $FAKE 'build.fsx' @args

