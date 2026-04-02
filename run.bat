@echo off
title Playwright C# NUnit Framework - Setup & Run
color 0A

echo.
echo ============================================================
echo   🎭 Playwright C# NUnit Framework - Setup ^& Run
echo ============================================================
echo.

:: ── Check .NET is installed ──────────────────────────────────────────────────
echo [1/5] Checking .NET 8.0 installation...
dotnet --version >nul 2>&1
IF %ERRORLEVEL% NEQ 0 (
    echo.
    echo ❌ ERROR: .NET SDK is not installed!
    echo    Please download it from: https://dotnet.microsoft.com/download/dotnet/8.0
    echo    Then re-run this file.
    echo.
    pause
    exit /b 1
)
echo ✅ .NET SDK found: 
dotnet --version
echo.

:: ── Restore NuGet Packages ───────────────────────────────────────────────────
echo [2/5] Restoring NuGet packages...
echo.
dotnet restore
IF %ERRORLEVEL% NEQ 0 (
    echo.
    echo ❌ ERROR: Failed to restore packages. Check your internet connection.
    pause
    exit /b 1
)
echo.
echo ✅ Packages restored successfully!
echo.

:: ── Build the Project ────────────────────────────────────────────────────────
echo [3/5] Building the project...
echo.
dotnet build --configuration Debug
IF %ERRORLEVEL% NEQ 0 (
    echo.
    echo ❌ ERROR: Build failed. Check the errors above.
    pause
    exit /b 1
)
echo.
echo ✅ Build successful!
echo.

:: ── Install Playwright Browsers ──────────────────────────────────────────────
echo [4/5] Installing Playwright browsers (Chrome, Firefox, Edge)...
echo       This may take 1-2 minutes on first run...
echo.
pwsh bin/Debug/net8.0/playwright.ps1 install
IF %ERRORLEVEL% NEQ 0 (
    echo.
    echo ⚠️  WARNING: Browser install may have failed.
    echo    Make sure PowerShell (pwsh) is installed.
    echo    Download: https://github.com/PowerShell/PowerShell/releases
    echo.
)
echo.
echo ✅ Browsers installed!
echo.

:: ── Ask which environment to run ─────────────────────────────────────────────
echo [5/5] Select Environment to run tests:
echo.
echo    [1] DEV
echo    [2] STAGING
echo    [3] PROD
echo.
set /p ENV_CHOICE="Enter choice (1/2/3) and press Enter: "

IF "%ENV_CHOICE%"=="1" set RUNSETTINGS=dev.runsettings
IF "%ENV_CHOICE%"=="2" set RUNSETTINGS=staging.runsettings
IF "%ENV_CHOICE%"=="3" set RUNSETTINGS=prod.runsettings
IF "%RUNSETTINGS%"=="" set RUNSETTINGS=dev.runsettings

echo.
echo ============================================================
echo   🚀 Running tests with: %RUNSETTINGS%
echo ============================================================
echo.

dotnet test --settings %RUNSETTINGS% --logger "console;verbosity=normal"

echo.
IF %ERRORLEVEL% EQU 0 (
    echo ============================================================
    echo   ✅ ALL TESTS PASSED!
    echo ============================================================
) ELSE (
    echo ============================================================
    echo   ❌ SOME TESTS FAILED - Check output above
    echo   📸 Screenshots saved in: screenshots\
    echo ============================================================
)

echo.
echo To view Allure report, run:
echo    allure serve allure-results
echo.
pause
