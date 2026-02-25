
#if !NETCOREAPP2_0_OR_GREATER && !NETSTANDARD2

namespace Polyfills;

using System.Threading;
using System.Threading.Tasks;
using System.Xml;
// ReSharper disable once RedundantUsingDirective
using System;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static partial class XDocumentPolyfill
{
    extension(XDocument)
    {
        /// <summary>
        /// Asynchronously creates a new XDocument and initializes its underlying XML tree using the specified stream, optionally preserving white space.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.loadasync?view=net-11.0#system-xml-linq-xdocument-loadasync(system-io-stream-system-xml-linq-loadoptions-system-threading-cancellationtoken)
        public static async Task<XDocument> LoadAsync(Stream stream, LoadOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            return XDocument.Parse(content, options);
        }

        /// <summary>
        /// Asynchronously creates a new XDocument and initializes its underlying XML tree using the specified text reader, optionally preserving white space.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.loadasync?view=net-11.0#system-xml-linq-xdocument-loadasync(system-io-textreader-system-xml-linq-loadoptions-system-threading-cancellationtoken)
        public static async Task<XDocument> LoadAsync(TextReader textReader, LoadOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var content = await textReader.ReadToEndAsync();
            return XDocument.Parse(content, options);
        }

        /// <summary>
        /// Asynchronously creates a new XDocument and initializes its underlying XML tree using the specified text reader, optionally preserving white space.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.loadasync?view=net-11.0#system-xml-linq-xdocument-loadasync(system-xml-xmlreader-system-xml-linq-loadoptions-system-threading-cancellationtoken)
        public static Task<XDocument> LoadAsync(XmlReader reader, LoadOptions options, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(XDocument.Load(reader, options));
        }

    }
}
#endif
