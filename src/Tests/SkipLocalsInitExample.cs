#region SkipLocalsInit

class SkipLocalsInitSample
{
    [SkipLocalsInit]
    static void ReadUninitializedMemory()
    {
        Span<int> numbers = stackalloc int[120];
        for (int i = 0; i < 120; i++)
        {
            Console.WriteLine(numbers[i]);
        }
    }
}

#endregion