// ReSharper disable PartialTypeWithSinglePart

partial class StringPolyfillTest
{
    public static void MyBool1([ConstantExpected] bool b) { }
    public static void MyLong1([ConstantExpected] long b) { }
    public static void MyLong2([ConstantExpected(Min = -5, Max = 10)] long b) { }
    public static void MyFloat1([ConstantExpected] float b) { }
    public static void MyFloat2([ConstantExpected(Min = -5.3f, Max = 10.1f)] float b) { }

    // Might want to warn for the negative values and out of range values
    // public static void MyInvalidUshort([ConstantExpected(Min = -5, Max = -1)] ushort b) { }
    // public static void MyInvalidRange([ConstantExpected(Min = 5, Max = -5)] int b) { }
    // flag any ref type usage as not applicable
    public static void MyString([ConstantExpected] ref string b) { }

    // Diagnostics examples
    public static void Test(long b, ushort u)
    {
        // OK
        const long a = 10;
        MyLong1(a);
        MyLong2(a);
        MyLong1(1L);
        MyLong2(2L);
        //MyInvalidUshort(1);
        //const ushort us = 0;
        //MyInvalidUshort(us); // Flag

        //MyLong1(b); // Flag
        //MyLong2(b); // Flag
        //MyLong2(20); // Flag, out of range
        //MyInvalidUshort(u); // Flag
        //MyInvalidUshort(10); // Flag, out of the range
    }
}