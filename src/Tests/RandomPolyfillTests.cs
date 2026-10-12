public class RandomPolyfillTests
{
    [Test, Repeat(100)]
    public async Task NextInt()
    {
        var number = Random.Shared.Next(10);

        await Assert.That(number).IsGreaterThanOrEqualTo(0).And.IsLessThanOrEqualTo(10);
    }

    [Test]
    public async Task SharedDiffersAcrossThreadsStartedTogether()
    {
        const int threadCount = 16;
        var sequences = new string[threadCount];
        using var barrier = new Barrier(threadCount);
        var threads = new List<Thread>();
        for (var index = 0; index < threadCount; index++)
        {
            var local = index;
            var thread = new Thread(() =>
            {
                barrier.SignalAndWait();
                var shared = Random.Shared;
                sequences[local] = $"{shared.Next()},{shared.Next()},{shared.Next()},{shared.Next()}";
            });
            threads.Add(thread);
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        await Assert.That(sequences.Distinct().Count()).IsEqualTo(threadCount);
    }
}