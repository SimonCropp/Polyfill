
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Linq;

static partial class Polyfill
{
#if !NET6_0_OR_GREATER && (NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1)
#endif
}