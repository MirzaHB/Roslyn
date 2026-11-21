using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace RoslynLab.Exercises;

/// <summary>
/// Exercise 7: loading a real project. Until now Lab.cs built the Compilation
/// by hand from loose files. Real code lives in .csproj and .sln files with
/// package references, target frameworks and build settings. MSBuildWorkspace
/// reads those and gives you the same Compilation the build would use.
///
/// Open SampleApp.csproj and print:
///
///   Project: SampleApp
///   Documents: 9
///   Errors: 0
///   Types: 13
///
/// "Documents" counts only the files under SampleApp that are not in obj/.
/// "Types" is the number of classes and interfaces declared in those files.
///
/// Useful API:
///   MSBuildWorkspace.Create()                  dispose it when done
///   workspace.WorkspaceFailed                  subscribe and print; load
///                                              problems are reported here
///                                              instead of being thrown
///   await workspace.OpenProjectAsync(Lab.SampleProjectPath)
///   project.Documents, document.FilePath
///   await project.GetCompilationAsync()
///   compilation.GetDiagnostics()               filter on
///                                              DiagnosticSeverity.Error
///
/// Then prove the point of the whole lab: take your exercise 3 or 4 code,
/// change it to accept a Compilation parameter, and feed it this one. It
/// should print the same thing.
///
/// For a whole solution the call is workspace.OpenSolutionAsync(path), and
/// you loop over solution.Projects.
/// </summary>
public static class Ex7_MSBuildWorkspace
{
    public static Task RunAsync()
    {
        throw new NotImplementedException();
    }
}
