# Larder.UITests

This is an NUnit project for end-to-end testing Larder by automating Chrome with Selenium Webdriver.

Tests in this project should focus on the happy path, verifying that features are working in broad strokes, without any exhaustive testing of branching logic.

Before running the tests, the React app and API should be running, and a test user with the details in `settings.json` should exist in the local database.

## Troubleshooting

An error like `OneTimeSetUp: System.InvalidOperationException : session not created: This version of ChromeDriver only supports Chrome version 130
Current browser version is 132.0.6834.160 with binary path ...` may be addressed by updating the `Selenium.WebDriver.ChromeDriver` package version in the `.csproj` file (for example from 130 to 132). 