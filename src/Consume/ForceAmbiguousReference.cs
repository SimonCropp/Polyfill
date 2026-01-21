// To trigger ambiguous reference for System.Runtime.CompilerServices types https://github.com/SimonCropp/Polyfill/issues/454

global using Foo;

using System;

namespace Foo;

public sealed class RequiredMemberAttribute;

public static class IsExternalInit;

public sealed class CallerArgumentExpressionAttribute;

public sealed class CompilerFeatureRequiredAttribute;

public sealed class ModuleInitializerAttribute;

public sealed class SkipLocalsInitAttribute;

public sealed class InterpolatedStringHandlerAttribute;

public sealed class InterpolatedStringHandlerArgumentAttribute;

public readonly struct DefaultInterpolatedStringHandler;

public sealed class CollectionBuilderAttribute;

public sealed class OverloadResolutionPriorityAttribute;

public sealed class ParamCollectionAttribute;

public sealed class DisableRuntimeMarshallingAttribute;

public sealed class AllowNullAttribute;

public sealed class DisallowNullAttribute;

public sealed class MaybeNullAttribute;

public sealed class NotNullAttribute;

public sealed class MaybeNullWhenAttribute;

public sealed class NotNullWhenAttribute;

public sealed class NotNullIfNotNullAttribute;

public sealed class MemberNotNullAttribute;

public sealed class MemberNotNullWhenAttribute;

public sealed class DoesNotReturnAttribute;

public sealed class DoesNotReturnIfAttribute;

public sealed class SetsRequiredMembersAttribute;

public sealed class ConstantExpectedAttribute;

public sealed class StringSyntaxAttribute;

public sealed class DynamicallyAccessedMembersAttribute;

public enum DynamicallyAccessedMemberTypes;

public sealed class RequiresUnreferencedCodeAttribute;

public sealed class RequiresDynamicCodeAttribute;

public sealed class DynamicDependencyAttribute;

public sealed class UnconditionalSuppressMessageAttribute;

public sealed class FeatureSwitchDefinitionAttribute;

public sealed class FeatureGuardAttribute;

public sealed class ExperimentalAttribute;

public sealed class UnscopedRefAttribute;

public sealed class StackTraceHiddenAttribute;

public sealed class DebuggerDisableUserUnhandledExceptionsAttribute;

public class UnreachableException;

public abstract class OSPlatformAttribute;

public sealed class SupportedOSPlatformAttribute;

public sealed class UnsupportedOSPlatformAttribute;

public sealed class ObsoletedOSPlatformAttribute;

public sealed class SupportedOSPlatformGuardAttribute;

public sealed class UnsupportedOSPlatformGuardAttribute;

public sealed class TargetPlatformAttribute;

public sealed class RequiresPreviewFeaturesAttribute;

public sealed class UnmanagedCallersOnlyAttribute;

public sealed class SuppressGCTransitionAttribute;

public class Lock;

public static class KeyValuePair;

public enum UnixFileMode;

public class NullabilityInfo;

public enum NullabilityState;

public class NullabilityInfoContext;
public ref struct SpanLineEnumerator;

public ref struct ValueMatch;

public readonly struct Index;

public readonly struct Range;

#if FeatureMemory
public delegate void SpanAction<T, in TArg>(Span<T> span, TArg arg);
public delegate void ReadOnlySpanAction<T, in TArg>(ReadOnlySpan<T> span, TArg arg);
#endif
