public class LockTests
{
    [Test]
    public async Task Enter()
    {
        var locker = new Lock();

        await Assert.That(locker.IsHeldByCurrentThread).IsFalse();
        locker.Enter();
        var isHeld = locker.IsHeldByCurrentThread;
        await Assert.That(isHeld).IsTrue();
    }

    [Test]
    public async Task TryEnter()
    {
        var locker = new Lock();

        await Assert.That(locker.IsHeldByCurrentThread).IsFalse();
        var result = locker.TryEnter();
        await Assert.That(result).IsTrue();
        await Assert.That(locker.IsHeldByCurrentThread).IsTrue();
    }

    [Test]
    public async Task TryEnter_Timeout()
    {
        var locker = new Lock();

        await Assert.That(locker.IsHeldByCurrentThread).IsFalse();
        var result = locker.TryEnter(100);
        await Assert.That(result).IsTrue();
        await Assert.That(locker.IsHeldByCurrentThread).IsTrue();
    }

    [Test]
    public async Task Exit()
    {
        var locker = new Lock();

        await Assert.That(locker.IsHeldByCurrentThread).IsFalse();
        locker.Enter();
        await Assert.That(locker.IsHeldByCurrentThread).IsTrue();
        locker.Exit();
        await Assert.That(locker.IsHeldByCurrentThread).IsFalse();
    }

    [Test]
    public async Task EnterScope()
    {
        var locker = new Lock();
        bool isHeldInside;

        await Assert.That(locker.IsHeldByCurrentThread).IsFalse();
        using (locker.EnterScope())
        {
            isHeldInside = locker.IsHeldByCurrentThread;
        }

        await Assert.That(isHeldInside).IsTrue();
        await Assert.That(locker.IsHeldByCurrentThread).IsFalse();
    }

    [Test]
    public async Task EnterScope_Layered()
    {
        var locker = new Lock();
        bool isHeldInner;
        bool isHeldMiddle;
        bool isHeldAfterInner;

        await Assert.That(locker.IsHeldByCurrentThread).IsFalse();
        using (locker.EnterScope())
        {
            isHeldMiddle = locker.IsHeldByCurrentThread;
            using (locker.EnterScope())
            {
                isHeldInner = locker.IsHeldByCurrentThread;
            }

            isHeldAfterInner = locker.IsHeldByCurrentThread;
        }

        await Assert.That(isHeldMiddle).IsTrue();
        await Assert.That(isHeldInner).IsTrue();
        await Assert.That(isHeldAfterInner).IsTrue();
        await Assert.That(locker.IsHeldByCurrentThread).IsFalse();
    }
}
