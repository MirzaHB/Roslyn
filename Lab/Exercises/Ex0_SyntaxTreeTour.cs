using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynLab.Exercises;

/// <summary>
/// Exercise 0 is already written. Run it, read the output next to this code,
/// then change the snippet and run it again.
///
/// Concept: a SyntaxTree is the source text with structure and nothing else.
/// It knows "this is an invocation with one argument". It does not know what
/// is being invoked; that is the semantic model's job (exercise 2 onwards).
/// </summary>
public static class Ex0_SyntaxTreeTour
{
    private const string Snippet = """
        class Greeter
        {
            string Hello(string name) => Format("hi " + name);
        }
        """;

    public static void Run()
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(Snippet);
        SyntaxNode root = tree.GetRoot();

        Console.WriteLine("== Every node in the tree ==");
        Dump(root, depth: 0);

        // The pattern you will use constantly: walk all descendants and keep
        // the node type you care about.
        Console.WriteLine();
        Console.WriteLine("== Just the methods ==");
        foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
        {
            // Each syntax class exposes its parts as typed properties.
            Console.WriteLine($"{method.ReturnType} {method.Identifier.Text}, " +
                              $"{method.ParameterList.Parameters.Count} parameter(s)");
        }

        // Note that Format does not exist anywhere and parsing still worked.
        // Syntax never checks meaning.
    }

    private static void Dump(SyntaxNode node, int depth)
    {
        // Kind() is the fine-grained node type; the C# class (for example
        // InvocationExpressionSyntax) is what you filter on with OfType<T>().
        var firstLine = node.ToString().Split('\n')[0].Trim();
        Console.WriteLine($"{new string(' ', depth * 2)}{node.Kind()}  \"{firstLine}\"");

        foreach (var child in node.ChildNodes())
            Dump(child, depth + 1);
    }
}
