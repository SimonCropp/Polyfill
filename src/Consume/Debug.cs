using System;

// Test to make sure there are no clashes in the Polyfill code with classes that might be defined in user code.
//
// Some codebases, for better or for worse, define classes with names that match common classes in the base
// .NET libraries, like "Debug". This works find in those codebases because they are usually contained in the
// same namespace, or are guarded in some other way. But if a Polyfill attribute is imported and uses code like:
//     Debug.Assert(genericParameter.IsGenericParameter);
// instead of a fully qualified name like:
//     System.Debug.Assert(genericParameter.IsGenericParameter);
// then users of Polyfill will get errors like the following, which they can't easily fix:
//     'Debug' does not contain a definition for 'Assert'
//
// So, this file defines a custom Debug class to make sure that Polyfill code doesn't clash with user code.

class Debug
{
    public static void Log(string content) => Console.WriteLine(content);

    public static void Log(ConsoleColor color, string content)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(content);
        Console.ResetColor();
    }

    public static void LogWarning(string content) => Log(ConsoleColor.Yellow, content);
    public static void LogError(string content) => Log(ConsoleColor.Red, content);
}
