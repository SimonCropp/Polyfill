using System.Runtime.ExceptionServices;

partial class PolyfillTests
{
    [Test]
    public async Task ExceptionDispatchInfoSetCurrentStackTrace()
    {
        var exception = new Exception("test");
        var result = ExceptionDispatchInfo.SetCurrentStackTrace(exception);
        await Assert.That(result).IsEqualTo(exception);
        await Assert.That(exception.StackTrace).IsNotNull();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static Exception CaptureStackTraceInNamedMethod()
    {
        var exception = ExceptionDispatchInfo.SetCurrentStackTrace(new Exception("test"));
        // prevent a tail call from removing this frame
        GC.KeepAlive(exception);
        return exception;
    }

    [Test]
    public async Task ExceptionDispatchInfoSetCurrentStackTrace_SurvivesThrow()
    {
        var exception = CaptureStackTraceInNamedMethod();
        await Assert.That(exception.StackTrace!).Contains(nameof(CaptureStackTraceInNamedMethod));

        string? thrownStackTrace = null;
        try
        {
            throw exception;
        }
        catch (Exception caught)
        {
            thrownStackTrace = caught.StackTrace;
        }

        await Assert.That(thrownStackTrace!).Contains(nameof(CaptureStackTraceInNamedMethod));
        await Assert.That(thrownStackTrace!).Contains("End of stack trace from previous location");
    }

    [Test]
    public async Task ExceptionDispatchInfoSetCurrentStackTrace_SurvivesAwait()
    {
        var task = Task.FromException(CaptureStackTraceInNamedMethod());

        string? thrownStackTrace = null;
        try
        {
            await task;
        }
        catch (Exception caught)
        {
            thrownStackTrace = caught.StackTrace;
        }

        await Assert.That(thrownStackTrace!).Contains(nameof(CaptureStackTraceInNamedMethod));
    }
}
