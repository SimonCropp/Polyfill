partial class PolyfillTests
{
    public static event EventHandler? MyEvent;

    [Test]
    public void HasSingleTarget()
    {
        MyEvent += Handler;
        Assert.IsTrue(MyEvent.HasSingleTarget());
        MyEvent += Handler;
        Assert.IsFalse(MyEvent.HasSingleTarget());
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

    [Test]
    public void EnumerateInvocationList()
    {
        var count = 0;

        MyEvent += (_, _) => count++;

        foreach (var item in DelegatePolyfill.EnumerateInvocationList(MyEvent))
        {
            item(this, EventArgs.Empty);
        }

        Assert.AreEqual(1, count);
    }
}