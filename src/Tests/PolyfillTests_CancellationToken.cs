partial class PolyfillTests
{
    [Test]
    public async Task CancellationToken_Register_Exceptions()
    {
        Cancel token = default;

#nullable disable
        // ReSharper disable AssignNullToNotNullAttribute
        await Assert.That(() => token.Register((Action<object, Cancel>) null, null)).Throws<ArgumentNullException>();

        // ReSharper disable once RedundantCast
        await Assert.That(() => token.UnsafeRegister((Action<object>) null, null)).Throws<ArgumentNullException>();
        await Assert.That(() => token.UnsafeRegister((Action<object, Cancel>) null, null)).Throws<ArgumentNullException>();
        // ReSharper restore AssignNullToNotNullAttribute
#nullable enable
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task CancellationToken_Register_ExecutionContextFlowsIfExpected(bool callbackWithToken)
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
                var expected = flowExecutionContext ? (int) s! : 0;
                if (asyncLocal.Value != expected)
                {
                    throw new($"Expected {expected} but got {asyncLocal.Value}");
                }
            };

            var token = cancelSource.Token;
            if (flowExecutionContext && callbackWithToken)
            {
                token.Register(
                    (s, t) =>
                    {
                        if (!token.Equals(t))
                        {
                            throw new("Token mismatch");
                        }
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
                        if (!token.Equals(t))
                        {
                            throw new("Token mismatch");
                        }
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
        await Assert.That(invoked).IsEqualTo(iterations);
    }
}
