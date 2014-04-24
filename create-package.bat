@echo off

if not exist build (
	md build
)

call .\.nuget\nuget.exe pack -OutputDirectory build