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

    public event EventHandler? EventForEnumerateInvocationList;

    [Test]
    public void EnumerateInvocationList()
    {
        var count = 0;

        EventForEnumerateInvocationList += (_, _) => count++;

        foreach (var item in Delegate.EnumerateInvocationList(EventForEnumerateInvocationList))
        {
            item(this, EventArgs.Empty);
        }

        Assert.AreEqual(1, count);
    }

    [Test]
    public void NullEnumerateInvocationList()
    {
        var count = 0;

        foreach (var item in Delegate.EnumerateInvocationList<EventHandler>(null))
        {
            item(this, EventArgs.Empty);
        }

        Assert.AreEqual(0, count);
    }
}