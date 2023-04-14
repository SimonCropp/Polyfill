// ReSharper disable RedundantUsingDirective
#nullable enable

using System.Collections.Concurrent;

namespace System.Reflection
{
    /// <summary>
    /// Static and thread safe wrapper around <see cref="NullabilityInfoContext"/>.
    /// </summary>
    internal static class NullabilityInfoExtensions
    {
        static ConcurrentDictionary<ParameterInfo, NullabilityInfo> parameterCache = new();
        static ConcurrentDictionary<PropertyInfo, NullabilityInfo> propertyCache = new();
        static ConcurrentDictionary<EventInfo, NullabilityInfo> eventCache = new();
        static ConcurrentDictionary<FieldInfo, NullabilityInfo> fieldCache = new();

        internal static NullabilityInfo GetNullabilityInfo(this MemberInfo info)
        {
            if (info is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetNullabilityInfo();
            }

            if (info is EventInfo eventInfo)
            {
                return eventInfo.GetNullabilityInfo();
            }

            if (info is FieldInfo fieldInfo)
            {
                return fieldInfo.GetNullabilityInfo();
            }

            throw new ArgumentException($"Unsupported type:{info.GetType().FullName}");
        }

        internal static NullabilityState GetNullability(this MemberInfo info)
        {
            return GetReadOrWriteState(info.GetNullabilityInfo());
        }

        internal static bool IsNullable(this MemberInfo info)
        {
            var nullability = info.GetNullabilityInfo();
            return IsNullable(info.Name, nullability);
        }

        internal static NullabilityInfo GetNullabilityInfo(this FieldInfo info)
        {
            return fieldCache.GetOrAdd(info, inner =>
            {
                var nullabilityContext = new NullabilityInfoContext();
                return nullabilityContext.Create(inner);
            });
        }

        internal static NullabilityState GetNullability(this FieldInfo info)
        {
            return GetReadOrWriteState(info.GetNullabilityInfo());
        }

        internal static bool IsNullable(this FieldInfo info)
        {
            var nullability = info.GetNullabilityInfo();
            return IsNullable(info.Name, nullability);
        }

        internal static NullabilityInfo GetNullabilityInfo(this EventInfo info)
        {
            return eventCache.GetOrAdd(info, inner =>
            {
                var nullabilityContext = new NullabilityInfoContext();
                return nullabilityContext.Create(inner);
            });
        }

        internal static NullabilityState GetNullability(this EventInfo info)
        {
            return GetReadOrWriteState(info.GetNullabilityInfo());
        }

        internal static bool IsNullable(this EventInfo info)
        {
            var nullability = info.GetNullabilityInfo();
            return IsNullable(info.Name, nullability);
        }

        internal static NullabilityInfo GetNullabilityInfo(this PropertyInfo info)
        {
            return propertyCache.GetOrAdd(info, inner =>
            {
                var nullabilityContext = new NullabilityInfoContext();
                return nullabilityContext.Create(inner);
            });
        }

        internal static NullabilityState GetNullability(this PropertyInfo info)
        {
            return GetReadOrWriteState(info.GetNullabilityInfo());
        }

        internal static bool IsNullable(this PropertyInfo info)
        {
            var nullability = info.GetNullabilityInfo();
            return IsNullable(info.Name, nullability);
        }

        internal static NullabilityInfo GetNullabilityInfo(this ParameterInfo info)
        {
            return parameterCache.GetOrAdd(info, inner =>
            {
                var nullabilityContext = new NullabilityInfoContext();
                return nullabilityContext.Create(inner);
            });
        }

        internal static NullabilityState GetNullability(this ParameterInfo info)
        {
            return GetReadOrWriteState(info.GetNullabilityInfo());
        }

        internal static bool IsNullable(this ParameterInfo info)
        {
            var nullability = info.GetNullabilityInfo();
            return IsNullable(info.Name!, nullability);
        }

        static NullabilityState GetReadOrWriteState(NullabilityInfo nullability)
        {
            if (nullability.ReadState == NullabilityState.Unknown)
            {
                return nullability.WriteState;
            }

            return nullability.ReadState;
        }

        static NullabilityState GetKnownState(string name, NullabilityInfo nullability)
        {
            var readState = nullability.ReadState;
            if (readState != NullabilityState.Unknown)
            {
                return readState;
            }

            var writeState = nullability.WriteState;
            if (writeState != NullabilityState.Unknown)
            {
                return writeState;
            }

            throw new($"The nullability of '{nullability.Type.FullName}.{name}' is unknown. Assembly: {nullability.Type.Assembly.FullName}.");
        }

        static bool IsNullable(string name, NullabilityInfo nullability)
        {
            return GetKnownState(name, nullability) == NullabilityState.Nullable;
        }

        //Patching
        internal static MemberInfo GetMemberWithSameMetadataDefinitionAs(Type type, MemberInfo member)
        {
            const BindingFlags all = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
            foreach (var info in type.GetMembers(all))
            {
                if (info.HasSameMetadataDefinitionAs(member))
                {
                    return info;
                }
            }

            throw new MissingMemberException(type.FullName, member.Name);
        }

        //https://github.com/dotnet/runtime/blob/main/src/coreclr/System.Private.CoreLib/src/System/Reflection/MemberInfo.Internal.cs
        static bool HasSameMetadataDefinitionAs(this MemberInfo target, MemberInfo other)
        {
            return target.MetadataToken == other.MetadataToken &&
                   target.Module.Equals(other.Module);
        }

        //https://github.com/dotnet/runtime/issues/23493
        internal static bool IsGenericMethodParameter(this Type target)
        {
            return target.IsGenericParameter &&
                   target.DeclaringMethod != null;
        }
    }
}