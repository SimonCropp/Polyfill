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
}