#if NETFRAMEWORK || NETSTANDARD2_0

namespace Polyfills;

using System.Xml;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Output this <see cref="XElement"/> to an <see cref="XmlWriter"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.saveasync?view=net-11.0#system-xml-linq-xelement-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken)
    public static Task SaveAsync(
        this XElement target,
        XmlWriter writer,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        target.Save(writer);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Output this <see cref="XElement"/> to a <see cref="Stream"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.saveasync?view=net-11.0#system-xml-linq-xelement-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken)
    public static Task SaveAsync(
        this XElement target,
        Stream stream,
        SaveOptions options,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        target.Save(stream, options);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Output this <see cref="XElement"/> to a <see cref="TextWriter"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.saveasync?view=net-11.0#system-xml-linq-xelement-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken)
    public static Task SaveAsync(
        this XElement target,
        TextWriter textWriter,
        SaveOptions options,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        target.Save(textWriter, options);
        return Task.CompletedTask;
    }
}
#endif
