[TestFixture]
public class LockTests
{
    [Test]
    public void Enter()
    {
        var locker = new Lock();

        Assert.False(locker.IsHeldByCurrentThread);
        locker.Enter();
        Assert.True(locker.IsHeldByCurrentThread);
    }

    [Test]
    public void TryEnter()
    {
        var locker = new Lock();

        Assert.False(locker.IsHeldByCurrentThread);
        Assert.True(locker.TryEnter());
        Assert.True(locker.IsHeldByCurrentThread);
    }

    [Test]
    public void TryEnter_Timeout()
    {
        var locker = new Lock();

        Assert.False(locker.IsHeldByCurrentThread);
        Assert.True(locker.TryEnter(100));
        Assert.True(locker.IsHeldByCurrentThread);
    }

    [Test]
    public void Exit()
    {
        var locker = new Lock();

        Assert.False(locker.IsHeldByCurrentThread);
        locker.Enter();
        Assert.True(locker.IsHeldByCurrentThread);
        locker.Exit();
        Assert.False(locker.IsHeldByCurrentThread);
    }

    [Test]
    public void EnterScope()
    {
        var locker = new Lock();

        Assert.False(locker.IsHeldByCurrentThread);
        using (locker.EnterScope())
        {
            Assert.True(locker.IsHeldByCurrentThread);
        }

        Assert.False(locker.IsHeldByCurrentThread);
    }

    [Test]
    public void EnterScope_Layered()
    {
        var locker = new Lock();

        Assert.False(locker.IsHeldByCurrentThread);
        using (locker.EnterScope())
        {
            Assert.True(locker.IsHeldByCurrentThread);
            using (locker.EnterScope())
            {
                Assert.True(locker.IsHeldByCurrentThread);
            }

            Assert.True(locker.IsHeldByCurrentThread);
        }

        Assert.False(locker.IsHeldByCurrentThread);
    }
}