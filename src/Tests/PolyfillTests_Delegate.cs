partial class PolyfillTests
{
    public static event EventHandler? EventForHasSingleTarget;

    [Test]
    public async Task HasSingleTarget()
    {
        EventForHasSingleTarget += Handler;
        await Assert.That(EventForHasSingleTarget.HasSingleTarget).IsTrue();
        EventForHasSingleTarget += Handler;
        await Assert.That(EventForHasSingleTarget.HasSingleTarget).IsFalse();
        var action = () =>
        {
        };
        await Assert.That(action.HasSingleTarget).IsTrue();
        action += action;
        await Assert.That(action.HasSingleTarget).IsFalse();
    }

    static void Handler(object? sender, EventArgs e)
    {
    }

    public event EventHandler? EventForEnumerateInvocationList;

    [Test]
    public async Task EnumerateInvocationList()
    {
        var count = 0;

        EventForEnumerateInvocationList += (_, _) => count++;

        foreach (var item in Delegate.EnumerateInvocationList(EventForEnumerateInvocationList))
        {
            item(this, EventArgs.Empty);
        }

        await Assert.That(count).IsEqualTo(1);
    }

    [Test]
    public async Task NullEnumerateInvocationList()
    {
        var count = 0;

        foreach (var item in Delegate.EnumerateInvocationList<EventHandler>(null))
        {
            item(this, EventArgs.Empty);
        }

        await Assert.That(count).IsEqualTo(0);
    }
}