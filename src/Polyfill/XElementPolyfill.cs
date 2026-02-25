
#if !NETCOREAPP2_0_OR_GREATER && !NETSTANDARD2

namespace Polyfills;

using System.Threading;
using System.Threading.Tasks;
using System.Xml;
// ReSharper disable once RedundantUsingDirective
using System;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static partial class XElementPolyfill
{
    extension(XElement)
    {
        /// <summary>
        /// Asynchronously creates a new XElement and initializes its underlying XML tree using the specified stream, optionally preserving white space.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.loadasync?view=net-11.0#system-xml-linq-xelement-loadasync(system-io-stream-system-xml-linq-loadoptions-system-threading-cancellationtoken)
        public static async Task<XElement> LoadAsync(Stream stream, LoadOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            return XElement.Parse(content, options);
        }

        /// <summary>
        /// Asynchronously creates a new XElement and initializes its underlying XML tree using the specified text reader, optionally preserving white space.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.loadasync?view=net-11.0#system-xml-linq-xelement-loadasync(system-io-textreader-system-xml-linq-loadoptions-system-threading-cancellationtoken)
        public static async Task<XElement> LoadAsync(TextReader textReader, LoadOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var content = await textReader.ReadToEndAsync();
            return XElement.Parse(content, options);
        }

        /// <summary>
        /// Asynchronously creates a new XElement and initializes its underlying XML tree using the specified text reader, optionally preserving white space.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.loadasync?view=net-11.0#system-xml-linq-xelement-loadasync(system-xml-xmlreader-system-xml-linq-loadoptions-system-threading-cancellationtoken)
        public static Task<XElement> LoadAsync(XmlReader reader, LoadOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(XElement.Load(reader, options));
        }
    }
}
#endif
