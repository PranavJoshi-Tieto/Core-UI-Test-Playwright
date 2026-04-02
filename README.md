# 🎭 Playwright C# NUnit Framework

A production-ready Playwright test automation framework built with **C# (.NET 8)**, **NUnit**, and **Allure Reporting** — supporting parallel execution across Chrome, Firefox, and Edge.

---

## 📁 Project Structure

```
playwright-csharp-framework/
├── src/
│   ├── Pages/
│   │   ├── BasePage.cs          ← Base POM class (all page actions)
│   │   └── LoginPage.cs         ← Login page object
│   ├── Tests/
│   │   └── LoginTests.cs        ← Login test cases (TC001–TC007)
│   ├── Fixtures/
│   │   ├── BaseTest.cs          ← Base test (browser setup/teardown, screenshots)
│   │   └── BrowserFactory.cs   ← Creates Chrome / Firefox / Edge instances
│   ├── Utils/
│   │   ├── TestSettings.cs      ← Reads parameters from .runsettings
│   │   ├── Logger.cs            ← Serilog logger (console + file)
│   │   ├── ScreenshotHelper.cs  ← Auto screenshot on failure
│   │   └── TestDataHelper.cs    ← Loads JSON test data
│   └── TestData/
│       └── loginData.json       ← Test data (valid/invalid users)
├── config/
├── .github/
│   └── workflows/
│       └── playwright-tests.yml ← GitHub Actions CI/CD
├
├── prod.runsettings             ← Production environment config
├── allureConfig.json            ← Allure report config
└── PlaywrightFramework.csproj   ← Project file with all NuGet packages
```

---

## ⚡ Quick Start

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PowerShell](https://github.com/PowerShell/PowerShell) (for Playwright browser install)
- [Allure CLI](https://docs.qameta.io/allure/#_installing_a_commandline) (for reports)

### 1. Install dependencies
```bash
dotnet restore
```

### 2. Install Playwright browsers
```bash
dotnet build
pwsh bin/Debug/net8.0/playwright.ps1 install
```

### 3. Run all tests
```bash
dotnet test --settings dev.runsettings
```

---

## 🌍 Environment Selection

Switch environments using different `.runsettings` files:

```bash
# Development
dotnet test --settings dev.runsettings

# Staging
dotnet test --settings staging.runsettings

# Production
dotnet test --settings prod.runsettings
```

Each `.runsettings` file configures:
- `BaseUrl` — your app's URL
- `Browser` — chromium / firefox / edge
- `Headless` — true/false
- `Timeout` — element wait timeout (ms)
- `RetryCount` — retry failed tests N times
- `ScreenshotOnFailure` — true/false
- `Username` / `Password` — test credentials

---

## ⚡ Parallel Execution

Tests run in **parallel by default** — each test gets its own isolated browser instance.

```bash
# Run with 3 parallel workers (3 browsers simultaneously)
dotnet test --settings staging.runsettings -- NUnit.NumberOfTestWorkers=3

# Run with 5 parallel workers
dotnet test --settings staging.runsettings -- NUnit.NumberOfTestWorkers=5
```

The `NumberOfTestWorkers` in each `.runsettings` file controls this:
```xml
<NUnit>
  <NumberOfTestWorkers>3</NumberOfTestWorkers>
</NUnit>
```

---

## 🌐 Browser Selection

Override the browser at runtime without changing `.runsettings`:

```bash
# Chrome
dotnet test --settings dev.runsettings -- TestRunParameters.Parameter\(name=\"Browser\",\ value=\"chromium\"\)

# Firefox
dotnet test --settings dev.runsettings -- TestRunParameters.Parameter\(name=\"Browser\",\ value=\"firefox\"\)

# Edge
dotnet test --settings dev.runsettings -- TestRunParameters.Parameter\(name=\"Browser\",\ value=\"edge\"\)
```

---

## 📊 Allure Reports

```bash
# Generate and open Allure report
allure serve allure-results
```

Or generate a static report:
```bash
allure generate allure-results --clean -o allure-report
allure open allure-report
```

---

## 🔄 Retry on Failure

Retry is configured at two levels:

**1. Per-test using `[Retry]` attribute:**
```csharp
[Test]
[Retry(2)]  // Retry up to 2 times
public async Task MyTest() { ... }
```

**2. Via `.runsettings` (global):**
```xml
<Parameter name="RetryCount" value="2" />
```

---

## 📸 Screenshots on Failure

Screenshots are automatically captured when a test fails and:
- Saved to `screenshots/` folder
- Attached to the NUnit test result
- Included in the Allure report

---

## ➕ Adding New Page Objects

1. Create `src/Pages/YourPage.cs` extending `BasePage`
2. Define selectors as `private const string`
3. Write page action methods
4. Use in your test class

```csharp
public class DashboardPage : BasePage
{
    private const string WelcomeHeading = "h1.welcome";

    public DashboardPage(IPage page, string baseUrl) : base(page, baseUrl) { }

    public async Task<string> GetWelcomeTextAsync()
        => await GetTextAsync(WelcomeHeading);
}
```

---

## ➕ Adding New Tests

1. Create `src/Tests/YourFeatureTests.cs` extending `BaseTest`
2. Initialize your page objects in `[SetUp]`
3. Write test methods with `[Test]` and Allure attributes

```csharp
[TestFixture]
[AllureSuite("Your Feature")]
public class YourTests : BaseTest
{
    private YourPage _yourPage = null!;

    [SetUp]
    public void Init() => _yourPage = new YourPage(Page, BaseUrl);

    [Test]
    [AllureId("TC010")]
    [Retry(2)]
    public async Task TC010_YourTest_ShouldDoSomething()
    {
        // Arrange, Act, Assert
    }
}
```

---

## 🚀 CI/CD — GitHub Actions

The workflow runs automatically on:
- Push to `main` or `develop`
- Pull requests
- Manual trigger (choose environment + browser)

The pipeline:
1. Builds the project
2. Installs Playwright browsers
3. Runs tests in **parallel across Chrome + Firefox**
4. Uploads screenshots on failure
5. Generates and publishes Allure report to GitHub Pages

---

## 🛠️ Tech Stack

| Tool | Version | Purpose |
|---|---|---|
| .NET | 8.0 | Runtime |
| Playwright | 1.44.0 | Browser automation |
| NUnit | 3.14.0 | Test framework |
| Allure.NUnit | 2.12.1 | Reporting |
| Serilog | 3.1.1 | Logging |
| Newtonsoft.Json | 13.0.3 | JSON test data |
