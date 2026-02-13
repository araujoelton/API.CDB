@echo off
cd /d "%~dp0"

echo Limpando resultados antigos...
if exist TestResults rmdir /s /q TestResults
if exist CoverageReport rmdir /s /q CoverageReport

echo Rodando testes com cobertura...
dotnet test CDB.Api.Tests\CDB.Api.Tests.csproj --settings coverlet.runsettings --collect:"XPlat Code Coverage" --results-directory ./TestResults --nologo
if errorlevel 1 exit /b 1

echo Gerando relatorio HTML...
reportgenerator -reports:"TestResults/**/coverage.cobertura.xml" -targetdir:"CoverageReport" -reporttypes:Html

echo.
echo Pronto. Abra CoverageReport\index.html no navegador.
pause
