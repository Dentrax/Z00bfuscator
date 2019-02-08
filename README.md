test
dotnet test .\Z00bfuscator.Tests\Z00bfuscator.Tests.csproj

build 
dotnet build --configuration Release --output ./bin/Release --verbosity quiet

run
dotnet run --configuration Release --project .\Z00bfuscator\Z00bfuscator.csproj .\Z00bfuscator.Test\bin
\Release\Z00bfuscator.Test.dll

test
1 dotnet run --configuration Release --project .\Z00bfuscator.Test\Z00bfuscator.Test.csproj
2 cp .\Z00bfuscator.Test\bin\Release\Z00bfuscator.Test.runtimeconfig.json .\Z00bfuscator.Test\bin\R
elease\Obfuscated_Z00bfuscator.Test.runtimeconfig.json
3 dotnet .\Z00bfuscator.Test\bin\Release\Obfuscated_Z00bfuscator.Test.dll