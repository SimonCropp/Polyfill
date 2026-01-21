namespace Fake;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

class Program
{
    private static void Main() =>
        Test<Program>();

    public void MethodA<T>() { }

    private static void Test<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]T>()
    {
        var mInfo = typeof(T).GetMethod("MethodA", 1, BindingFlags.Public | BindingFlags.Instance, new Type[] { typeof(int) });
        if (mInfo is null)
        {
            throw new InvalidOperationException("MethodA not found");
        }
    }
}
