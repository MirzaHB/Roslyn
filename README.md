# Roslyn Lab

Seven small exercises that teach the parts of Roslyn the codebase navigator is
built on. Each one is a stub you fill in, with the expected output to check
against.

## Layout

```
SampleApp/        A tiny fake web app. This is the code you analyse.
Lab/Lab.cs        Shared setup: parses SampleApp and builds a Compilation.
Lab/Exercises/    Ex0 is written for you. Ex1 to Ex7 are yours.
EXPECTED.md       What each exercise should print.
```

SampleApp defines its own `[HttpGet]`, `[Route]` and `ControllerBase` so it
needs no packages. They have the same shape as the ASP.NET Core ones.

## Running

From this folder:

```
dotnet run --project Lab -- 0
```

Replace `0` with the exercise number.

## The three ideas

| Thing | What it is | You get it from |
|---|---|---|
| `SyntaxTree` / `SyntaxNode` | The text of one file, with structure. Knows nothing about meaning. | `CSharpSyntaxTree.ParseText` |
| `Compilation` | All trees plus references. The whole program as the compiler sees it. | `CSharpCompilation.Create` or `MSBuildWorkspace` |
| `SemanticModel` → `ISymbol` | The bridge from a syntax node to the thing it declares or refers to. | `compilation.GetSemanticModel(tree)` |

Almost every analysis has the same shape: find interesting syntax nodes, ask
the semantic model for their symbols, then work with the symbols.

## Exercises and what they become

| # | Exercise | Navigator feature it leads to |
|---|---|---|
| 0 | Syntax tree tour (done) | |
| 1 | List declarations from syntax | Type and method nodes |
| 2 | Symbols, base types, interfaces | Inherits and implements edges |
| 3 | Call graph | Tracing an execution path |
| 4 | Endpoint discovery | Searching for an endpoint |
| 5 | Constructor dependencies | Which services depend on a class |
| 6 | Interface resolution | Traces that continue through DI |
| 7 | Loading a real project | Opening any solution |

Do them in order. Each file's header comment lists the API you need and
something to think about.

## When you are stuck

- Call `Dump` from Ex0 on any node to see what it is made of.
- Paste code into https://sharplab.io and pick "Syntax Tree" in the results
  dropdown.
- If `GetSymbolInfo(...).Symbol` is null, look at `.CandidateSymbols` and at
  `compilation.GetDiagnostics()`. Null usually means the code does not compile.
