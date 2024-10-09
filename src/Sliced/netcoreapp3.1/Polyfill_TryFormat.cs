
#pragma warning disable

namespace Polyfills;

using System;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if FeatureMemory && (NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X)
#endif

#if (FeatureMemory && (NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X)) || NET6_0
#endif
#if NET6_0
#endif

#if NET6_0 || (FeatureMemory && (NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X))
#endif
}