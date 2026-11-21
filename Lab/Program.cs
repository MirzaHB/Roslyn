using RoslynLab.Exercises;

// Usage: dotnet run --project Lab -- <exercise number>

if (args.Length != 1 || !int.TryParse(args[0], out var number))
{
    Console.WriteLine("Usage: dotnet run --project Lab -- <0-7>");
    return 1;
}

try
{
    switch (number)
    {
        case 0: Ex0_SyntaxTreeTour.Run(); break;
        case 1: Ex1_ListDeclarations.Run(); break;
        case 2: Ex2_Symbols.Run(); break;
        case 3: Ex3_CallGraph.Run(); break;
        case 4: Ex4_Endpoints.Run(); break;
        case 5: Ex5_ConstructorDependencies.Run(); break;
        case 6: Ex6_InterfaceResolution.Run(); break;
        case 7: await Ex7_MSBuildWorkspace.RunAsync(); break;
        default:
            Console.WriteLine($"There is no exercise {number}.");
            return 1;
    }
}
catch (NotImplementedException)
{
    Console.WriteLine($"Exercise {number} isn't implemented yet. Open Lab/Exercises and fill it in.");
    return 1;
}

return 0;
