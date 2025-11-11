# CareerCompassWebApi — Project Initiation Tutorial

This README is written as a personal tutorial to record the very first steps I took to create this project from the template produced by the .NET CLI, and the minimal Git workflow I used to capture the initial code on a dedicated branch named "Project Initiation" (see notes about branch naming below).

## Goal

Capture how the project was created and pushed so I can find the very first code later in the branch called `Project Initiation`. Future branches will be created per feature.

## Prerequisites

- .NET 9 SDK (this project targets `net9.0`)
- git

Verify your SDK with:

```bash
dotnet --version
```

## How the project was created

From an empty folder I ran the .NET CLI template command that created the Web API skeleton used in this repository:

```bash
dotnet new webapi --use-controllers
```

This creates the minimal controller-based web API template (files and folders listed below).

## Typical first commits & branch creation (recommended)

There are two typical ways to get this initial template into a branch named `Project Initiation` on a remote.

Option A — create branch locally first (recommended when starting a fresh repo):

```bash
# initialize git (if not already a git repo)
git init

# create a branch named exactly as you want it locally
git checkout -b "proj-init"

# stage and commit generated files
git add .
git commit -m "chore: project initiation - add template from dotnet new webapi --use-controllers"

# add remote and push this branch up
git remote add origin git@github.com:devzwide/CareerCompassWebApi.git
git push -u origin "proj-init"
```

Option B — commit to default branch then create a branch from it (if you already committed to `master`/`main`):

```bash
git add .
git commit -m "chore: initial template"

# create the branch from current commit
git checkout -b "proj-init"
git push -u origin "proj-init"
```

## What the template created (short tour)

- `Program.cs` — app entry point, host and middleware setup.
- `WebAPI.csproj` — project file describing target framework and packages.
- `Controllers/WeatherForecastController.cs` — example controller created by the template.
- `WeatherForecast.cs` — example model used by the sample controller.
- `appsettings.json` and `appsettings.Development.json` — configuration files.
- `Properties/launchSettings.json` — local launch profiles (ports, environment).

You can inspect these files to learn more about the default structure.

## How to run the project locally

From the project root:

```bash
dotnet build
dotnet run
```

When the app starts the console will show the listening URL(s). You can also run the project from an IDE (Visual Studio / VS Code) which will use `launchSettings.json`.

To call the sample endpoint (adjust the port if your app uses a different one):

```bash
curl http://localhost:5000/weatherforecast
```

If you don't know the port, check the console output from `dotnet run` or open `Properties/launchSettings.json` for the local profile.

## Git tips — inspect where the first code lives

List branches locally and remotely:

```bash
git branch         # local branches
git branch -a      # all (including remotes)
```

Show the latest commits (useful to find the initialization commit):

```bash
git log --oneline --decorate --graph -n 10
```

If you pushed `Project Initiation` to the remote, you'll find the initial files there. In a web UI (GitHub/GitLab) search or filter by branch name.

## Pull request / branch checklist (keep short)

- Branch name: use clear names (e.g. `feature/auth`, `fix/typo`, or `chore/project-initiation`).
- Keep PRs small and focused (1–2 logical changes).
- Add a descriptive title and short description.
- Link to any issue or ticket if relevant.
- Add a short testing note: how to run and verify.

## Suggested next steps

- Create an issue tracker board or a minimal README section describing feature priorities.
- Add CI that builds the project on each push (GitHub Actions, GitLab CI, etc.).
- Add a CODEOWNERS, CONTRIBUTING.md, and a basic .gitignore if not already present.

## Where to find the first code

- The initial template commit is recorded on the branch named `Project Initiation` (or `project-initiation` if you prefer a safer name). Look for the first commit message containing `initial` or `project initiation` in the branch's history.

---
