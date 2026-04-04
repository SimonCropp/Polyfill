using System;

public class BclMemoryConsume
{
    public Index GetIndex() => new Index(0);

    public Range GetRange() => new Range(new Index(0), new Index(1));
}
