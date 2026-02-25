#if NETFRAMEWORK || NETSTANDARD2_0

namespace Polyfills;

using System.Xml;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Writes the current node to an <see cref="XmlWriter"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xnode.writetoasync?view=net-11.0
    public static Task WriteToAsync(
        this XNode target,
        XmlWriter writer,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        target.WriteTo(writer);
        return Task.CompletedTask;
    }
}
#endif
