
#pragma warning disable

namespace Polyfills;

using System;
using System.IO;
using System.Runtime.InteropServices;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if FeatureValueTask && FeatureMemory && (NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0)
#endif

#if !NET7_0_OR_GREATER
#endif
}