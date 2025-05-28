public class FilterTests
{
    [Theory, CombinatorialData]
    public Task Multiple_Framework_Or(bool hasElse, bool negateFirstTfm, bool negateSecondTfm, BinaryMode binaryMode, TargetMode targetMode)
    {
        var builder = new StringBuilder();

        builder.Append("#if ");
        if (negateFirstTfm)
        {
            builder.Append('!');
        }

        builder.AppendLine("FirstTfm");

        void AppendSecondTfm()
        {
            if (negateSecondTfm)
            {
                builder.Append('!');
            }

            builder.AppendLine("SecondTfm");
        }

        switch (binaryMode)
        {
            case BinaryMode.None:
                break;
            case BinaryMode.And:
                builder.Append(" && ");
                AppendSecondTfm();
                break;
            case BinaryMode.Or:
            {
                builder.Append(" || ");
                AppendSecondTfm();
                break;
            }
        }

        builder.AppendLine("Console.WriteLine(\"one\");");

        if (hasElse)
        {
            builder.AppendLine("#else");
            builder.AppendLine("Console.WriteLine(\"two\");");
        }

        builder.AppendLine("#endif");
        var code = builder.ToString();
        var result = Filter.ByTargetFramework(code, GetTargetFramework(targetMode));

        var output = $"""
                      code:

                      ```
                      {code}
                      ```

                      result:

                      ```
                      {result}
                      ```
                      """;
        return Verify(output);
    }

    static string GetTargetFramework(TargetMode mode) =>
        mode switch
        {
            TargetMode.None => "NoMatch",
            TargetMode.First => "FirstTfm",
            TargetMode.Second => "SecondTfm",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    public enum BinaryMode
    {
        None,
        And,
        Or
    }
    public enum TargetMode
    {
        None,
        First,
        Second
    }
}