
#if !NETCOREAPP2_0_OR_GREATER && !NETSTANDARD2

namespace Polyfills;

using System.Threading;
using System.Threading.Tasks;
using System.Xml;
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
static partial class XNodePolyfill
{
    extension(XNode)
    {
        /// <summary>
        /// Creates an <see cref="XNode"/> from an <see cref="XmlReader"/>. The runtime type of the node is determined by the NodeType of the first node encountered in the reader.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xnode.readfromasync?view=net-11.0
        public static Task<XNode> ReadFromAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(XNode.ReadFrom(reader));
        }
    }
}
#endif
