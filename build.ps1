param([string]$GameDir = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path)
$ErrorActionPreference = 'Stop'
$managed = Join-Path $GameDir 'Cities_Data\Managed'
$harmony = Join-Path $GameDir 'Files\Mods\CitiesHarmony'
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
$out = Join-Path $PSScriptRoot 'dist'
New-Item -ItemType Directory -Force -Path $out | Out-Null
$outArg = '/out:' + (Join-Path $out 'MetropolisBuildingRules.dll')
$refs = @('ICities.dll','UnityEngine.dll','Assembly-CSharp.dll','ColossalManaged.dll') | ForEach-Object { '/reference:' + (Join-Path $managed $_) }
$refs += '/reference:' + (Join-Path $harmony 'CitiesHarmony.dll')
$refs += '/reference:' + (Join-Path $harmony 'CitiesHarmony.Harmony.dll')
$refs += '/reference:' + (Join-Path $GameDir 'Files\Mods\PerformanceBooster\CitiesHarmony.API.dll')
& $compiler /nologo /target:library /optimize+ $outArg $refs (Join-Path $PSScriptRoot 'src\RuleEngine.cs') (Join-Path $PSScriptRoot 'src\MetropolisBuildingRules.cs')
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
$testOut = '/out:' + (Join-Path $out 'RuleEngineTests.exe')
& $compiler /nologo /target:exe $testOut (Join-Path $PSScriptRoot 'src\RuleEngine.cs') (Join-Path $PSScriptRoot 'tests\RuleEngineTests.cs')
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
& (Join-Path $out 'RuleEngineTests.exe')
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
