partial class PolyfillExtensionsTests
{
    [Test]
    public void CancellationToken_Register_Exceptions()
    {
        CancellationToken token = default;

#nullable disable
        Assert.Throws<ArgumentNullException>(() => token.Register((Action<object, CancellationToken>)null, null));

        Assert.Throws<ArgumentNullException>(() => token.UnsafeRegister((Action<object>)null, null));
        Assert.Throws<ArgumentNullException>(() => token.UnsafeRegister((Action<object, CancellationToken>)null, null));
#nullable enable
    }

    [Test]
    [TestCase(false)]
    [TestCase(true)]
    public static void CancellationToken_Register_ExecutionContextFlowsIfExpected(bool callbackWithToken)
    {
        var cts = new CancellationTokenSource();

        const int Iters = 5;
        int invoked = 0;

        AsyncLocal<int> al = new AsyncLocal<int>();
        for (int i = 1; i <= Iters; i++)
        {
            bool flowExecutionContext = i % 2 == 0;

            al.Value = i;
            Action<object?> callback = s =>
            {
                invoked++;
                Assert.AreEqual(flowExecutionContext ? (int)s! : 0, al.Value);
            };

            CancellationToken ct = cts.Token;
            if (flowExecutionContext && callbackWithToken)
            {
                ct.Register((s, t) =>
                {
                    Assert.AreEqual(ct, t);
                    callback(s);
                }, i);
            }
            else if (flowExecutionContext)
            {
                ct.Register(callback, i);
            }
            else if (callbackWithToken)
            {
                ct.UnsafeRegister((s, t) =>
                {
                    Assert.AreEqual(ct, t);
                    callback(s);
                }, i);
            }
            else
            {
                ct.UnsafeRegister(callback, i);
            }
        }
        al.Value = 0;

        cts.Cancel();
        Assert.AreEqual(Iters, invoked);
    }
}
