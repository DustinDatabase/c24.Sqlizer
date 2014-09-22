#!/bin/bash

NUGET=./.nuget/nuget.exe
FAKE=./packages/FAKE.Core/tools/FAKE.exe

[ ! -e $FAKE ] && mono $NUGET install FAKE.Core -ExcludeVersion -NonInteractive -OutputDirectory ./packages
mono $FAKE ./build.fsx $@

