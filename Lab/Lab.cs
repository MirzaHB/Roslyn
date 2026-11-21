using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace RoslynLab;

/// <summary>
/// Shared setup for the exercises. Read it once: it is the whole "get source
/// text into Roslyn" story when you are not loading a real project file.
/// </summary>
public static class Lab
{
    public static string SampleDir { get; } = FindSampleDir();

    public static string SampleProjectPath => Path.Combine(SampleDir, "SampleApp.csproj");

    /// <summary>
    /// Parses every .cs file in SampleApp into a SyntaxTree. Parsing needs
    /// nothing but the text: no references, no other files.
    /// </summary>
    public static IReadOnlyList<SyntaxTree> ParseSampleTrees()
    {
        return Directory
            .EnumerateFiles(SampleDir, "*.cs", SearchOption.AllDirectories)
            .Where(path => !IsBuildOutput(path))
            .OrderBy(path => path, StringComparer.Ordinal)
            // Passing the path makes tree.FilePath available later.
            .Select(path => CSharpSyntaxTree.ParseText(File.ReadAllText(path), path: path))
            .ToList();
    }

    /// <summary>
    /// Bundles the trees with references into a Compilation. This is what
    /// makes semantic questions ("what does this name refer to?") answerable.
    /// </summary>
    public static CSharpCompilation CreateSampleCompilation()
    {
        // The compiler has to be told where System.Object, List<T> etc. live.
        // The assemblies of the runtime we are running on are good enough.
        var runtimeAssemblies = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
            .Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path));

        return CSharpCompilation.Create(
            assemblyName: "SampleApp",
            syntaxTrees: ParseSampleTrees(),
            references: runtimeAssemblies,
            options: new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                nullableContextOptions: NullableContextOptions.Enable));
    }

    /// <summary>Path of a tree relative to SampleApp, for readable output.</summary>
    public static string RelativePath(SyntaxTree tree) =>
        Path.GetRelativePath(SampleDir, tree.FilePath);

    private static bool IsBuildOutput(string path)
    {
        var relative = Path.GetRelativePath(SampleDir, path);
        var firstSegment = relative.Split(Path.DirectorySeparatorChar)[0];
        return firstSegment is "obj" or "bin";
    }

    private static string FindSampleDir()
    {
        // Walk up from bin/Debug/net8.0 until we find the folder next to Lab.
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            var candidate = Path.Combine(dir.FullName, "SampleApp");
            if (File.Exists(Path.Combine(candidate, "SampleApp.csproj")))
                return candidate;
            dir = dir.Parent;
        }

        throw new DirectoryNotFoundException("Could not find the SampleApp folder.");
    }
}
