using NUnit.Framework;

[TestFixture]
public class NamedTupleSample
{
    #region NamedTupleSample

    [Test]
    public void Usage()
    {
        var (value1, value2) = Method();
        Trace.WriteLine(value1);
        Trace.WriteLine(value2);
    }

    static (string value1, bool value2) Method() =>
        new("value", false);

    #endregion
}

