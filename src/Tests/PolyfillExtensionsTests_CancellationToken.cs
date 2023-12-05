partial class PolyfillExtensionsTests
{
    [Test]
    public void CancellationToken_Register_Exceptions()
    {
        Cancel token = default;

#nullable disable
        Assert.Throws<ArgumentNullException>(() => token.Register((Action<object, Cancel>) null, null));

        // ReSharper disable once RedundantCast
        Assert.Throws<ArgumentNullException>(() => token.UnsafeRegister((Action<object>) null, null));
        Assert.Throws<ArgumentNullException>(() => token.UnsafeRegister((Action<object, Cancel>) null, null));
#nullable enable
    }

    [Test]
    [TestCase(false)]
    [TestCase(true)]
    public static void CancellationToken_Register_ExecutionContextFlowsIfExpected(bool callbackWithToken)
    {
        var cancelSource = new CancelSource();

        const int iterations = 5;
        var invoked = 0;

        var asyncLocal = new AsyncLocal<int>();
        for (var i = 1; i <= iterations; i++)
        {
            var flowExecutionContext = i % 2 == 0;

            asyncLocal.Value = i;
            Action<object?> callback = s =>
            {
                invoked++;
                Assert.AreEqual(flowExecutionContext ? (int) s! : 0, asyncLocal.Value);
            };

            var token = cancelSource.Token;
            if (flowExecutionContext && callbackWithToken)
            {
                token.Register(
                    (s, t) =>
                    {
                        Assert.AreEqual(token, t);
                        callback(s);
                    },
                    i);
            }
            else if (flowExecutionContext)
            {
                token.Register(callback, i);
            }
            else if (callbackWithToken)
            {
                token.UnsafeRegister(
                    (s, t) =>
                    {
                        Assert.AreEqual(token, t);
                        callback(s);
                    },
                    i);
            }
            else
            {
                token.UnsafeRegister(callback, i);
            }
        }

        asyncLocal.Value = 0;

        cancelSource.Cancel();
        Assert.AreEqual(iterations, invoked);
    }
}