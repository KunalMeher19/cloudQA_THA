# CloudQA Automation Assessment - C# Selenium Tests

This project contains automated tests for the CloudQA Automation Practice Form, written in C# using Selenium and NUnit. The tests are designed to be **robust** against HTML changes (IDs, positions, attributes).

---

## 📋 Table of Contents

- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Running the Tests](#running-the-tests)
- [Robustness Strategy](#robustness-strategy)
- [Project Structure](#project-structure)
- [Test Coverage](#test-coverage)

---

## ⚙️ Prerequisites

Before cloning and running this project, ensure you have the following installed:

### 1. **.NET SDK** (6.0 or later)
Check if installed:
```bash
dotnet --version
```

If not installed, download from: https://dotnet.microsoft.com/download

### 2. **Google Chrome Browser**
Download from: https://www.google.com/chrome/

### 3. **Git** (for cloning)
Check if installed:
```bash
git --version
```

Download from: https://git-scm.com/downloads

---

## 🚀 Installation & Setup

### Step 1: Clone the Repository
```bash
git clone <YOUR_GITHUB_REPO_URL>
cd CloudQATests
```

### Step 2: Restore Dependencies
The project uses NuGet packages (Selenium, NUnit, ChromeDriver). Restore them:
```bash
dotnet restore
```

This will automatically download:
- `Selenium.WebDriver` (v4.38.0)
- `Selenium.Support` (v4.38.0)
- `Selenium.WebDriver.ChromeDriver` (v142.x)
- `NUnit` (v4.4.0)
- `NUnit3TestAdapter` (v5.2.0)
- `Microsoft.NET.Test.Sdk` (v18.0.1)

### Step 3: Build the Project
```bash
dotnet build
```

Expected output:
```
Build succeeded in X.Xs
```

---

## ▶️ Running the Tests

### Run All Tests
```bash
dotnet test
```

### Run with Detailed Output
```bash
dotnet test --logger "console;verbosity=detailed"
```

### Expected Results
```
Passed!  - Failed:     0, Passed:     4, Skipped:     0, Total:     4, Duration: ~30s
```

**Note:** Tests will open Chrome browser windows automatically. This is normal behavior.

---

## 🛡️ Robustness Strategy

### The Problem
Traditional Selenium locators break when:
- Element IDs change: `id="input_123"` → `id="input_456"` ❌
- Positions change: `div:nth-child(3)` → `div:nth-child(5)` ❌
- Class names change: `.form-control` → `.input-field` ❌

### The Solution: Label-Based Locators

Instead of relying on fragile attributes, this project uses **semantic locators** based on visible label text:

```csharp
// ❌ Fragile approach
driver.FindElement(By.Id("firstName"));

// ✅ Robust approach (used in this project)
//label[normalize-space()='First Name']/following::input[1]
```

### Implementation

**BasePage.cs** contains reusable helper methods:

1. **`GetInputByLabel(string labelText)`**
   - Finds inputs associated with a `<label>` element
   - Works for text fields, emails, etc.

2. **`GetRadioByFollowingText(string textValue)`**
   - Finds radio buttons where text appears AFTER the input
   - Used for Gender field: `<input id='male'/> Male`

3. **`WaitUntilVisible(By locator)`**
   - Explicit wait (10 seconds) to ensure elements are ready
   - Prevents flaky tests

### Why This Works
✅ Label text ("First Name", "Email") rarely changes  
✅ Uses relationships (`/following::input`) not absolute positions  
✅ Fallback mechanisms for different DOM structures  

---

## 📁 Project Structure

```
CloudQATests/
├── Pages/
│   ├── BasePage.cs                      # Base class with robust locator helpers
│   └── AutomationPracticeFormPage.cs    # Page Object for the practice form
├── Tests/
│   ├── BaseTest.cs                      # WebDriver setup/teardown
│   └── AutomationPracticeFormTests.cs   # 4 test cases
├── CloudQATests.csproj                  # Project file with dependencies
├── .gitignore                           # Ignores bin/, obj/, etc.
└── README.md                            # This file
```

### Key Files

| File | Purpose |
|------|---------|
| `BasePage.cs` | Core innovation - contains `GetInputByLabel()`, `GetRadioByFollowingText()` helpers |
| `AutomationPracticeFormPage.cs` | Page Object Model for the form |
| `BaseTest.cs` | Handles ChromeDriver initialization and cleanup |
| `AutomationPracticeFormTests.cs` | 4 NUnit test cases |

---

## ✅ Test Coverage

| Test Method | Field Tested | What It Validates |
|-------------|--------------|-------------------|
| `FirstName_ShouldBeEditable` | First Name | Input accepts text and stores value |
| `Email_ShouldBeEditable` | Email | Email field functionality |
| `Gender_ShouldBeSelectable` | Gender (Radio) | Can select Male/Female radio buttons |
| `Mobile_ShouldBeEditable` | Mobile # | Mobile number input (BONUS - 4th test) |

**Note:** Assignment required 3 fields, this project tests **4 fields** to exceed expectations.

---

## 🎯 Submission Checklist

- [x] Tests for at least 3 fields (we have 4)
- [x] Robust locators that survive DOM changes
- [x] Page Object Model architecture
- [x] Clear README with instructions
- [x] .gitignore for clean repository
- [x] All dependencies in `.csproj` file

---

## 📞 Questions?

If tests fail, check:
1. Is Chrome browser installed?
2. Is .NET SDK installed? (`dotnet --version`)
3. Did you run `dotnet restore`?

For any issues, ensure Chrome and ChromeDriver versions are compatible (auto-handled by `Selenium.WebDriver.ChromeDriver` package).

---

**Project by:** Ardhendu Abhishek Meher |
**Assignment for:** CloudQA Developer Internship  
**Date:** November 2025
