partial class PolyfillTests
{
    public static event EventHandler? EventForHasSingleTarget;

    [Test]
    public void HasSingleTarget()
    {
        EventForHasSingleTarget += Handler;
        Assert.IsTrue(EventForHasSingleTarget.HasSingleTarget());
        EventForHasSingleTarget += Handler;
        Assert.IsFalse(EventForHasSingleTarget.HasSingleTarget());
        var action = () =>
        {
        };
        Assert.IsTrue(action.HasSingleTarget());
        action += action;
        Assert.IsFalse(action.HasSingleTarget());
    }

    static void Handler(object? sender, EventArgs e)
    {
    }

    public static event EventHandler? EventForEnumerateInvocationList;

    [Test]
    public void EnumerateInvocationList()
    {
        var count = 0;

        EventForEnumerateInvocationList += (_, _) => count++;

        foreach (var item in DelegatePolyfill.EnumerateInvocationList(EventForEnumerateInvocationList))
        {
            item(this, EventArgs.Empty);
        }

        Assert.AreEqual(1, count);
    }
}