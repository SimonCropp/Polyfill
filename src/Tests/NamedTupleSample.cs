public class NamedTupleSample
{
    #region NamedTupleSample

    [Test]
    public Task Usage()
    {
        var (value1, value2) = Method();
        Trace.WriteLine(value1);
        Trace.WriteLine(value2);
        return Task.CompletedTask;
    }

    static (string value1, bool value2) Method() =>
        new("value", false);

    #endregion
}

