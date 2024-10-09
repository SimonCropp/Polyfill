
#pragma warning disable

namespace Polyfills;

using System;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if (NETSTANDARD || NETFRAMEWORK || NETCOREAPP2_0) && FeatureMemory
#endif

#if !NET8_0_OR_GREATER
#if FeatureMemory
#endif
#endif
}