# AQ-10 — Interactive Autism Spectrum Screening App

A cross-platform mobile app that presents the clinically-used **AQ-10 (Autism Spectrum Quotient)** screening questionnaire as an interactive, narrative-driven experience — built as a university group project.

## My Role

I was the **sole software engineer** on a 6-person interdisciplinary team:
- 1x Software Engineer (me) — app architecture, all C#/XAML implementation, scoring logic, unit tests, documentation
- 2x Game Developers — scene design and narrative flow
- 1x Sound Engineer — audio design
- 1x Graphic Designer — visual assets
- 1x Web Designer — acted as Project Manager

I was responsible for turning the team's design and content into a working, tested application.

## What It Does

The app walks the user through the 10 AQ-10 questions as individual interactive scenes, with audio narration (toggleable), then calculates a score based on standard AQ-10 scoring rules.

## Tech Stack

- **.NET MAUI** (C#, XAML) — single codebase targeting Android, iOS, macOS, and Windows
- **MVVM architecture** — dedicated ViewModel per scene, shared `BaseViewModel`
- **xUnit** — unit tests for ViewModels and the scoring service
- **Doxygen** — auto-generated code documentation

## Project Structure

- `AQ_10/ViewModel` — one ViewModel per scene/question, plus shared base
- `AQ_10/Views` — XAML pages for each scene
- `AQ_10/Services/AnswersService.cs` — singleton managing answers and score calculation
- `Tests/` — unit tests covering ViewModels and scoring logic
- `AQ_10/Platforms/` — platform-specific entry points (Android, iOS, Windows, macOS)

## Getting Started

Requires the .NET MAUI workload:

```bash
dotnet workload install maui
```

Then open `AQ_10.sln` in Visual Studio and run on your target platform.
