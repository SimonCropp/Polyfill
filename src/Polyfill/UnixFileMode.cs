namespace System.IO;

#if !NET7_0_OR_GREATER

/// <summary>
/// Represents the Unix filesystem permissions.
///
/// This enumeration supports a bitwise combination of its member values.
/// </summary>
#if PolyPublic
public
#endif
[Flags]
enum UnixFileMode
{
    /// <summary>
    /// No permissions
    /// </summary>
    None = 0,
    /// <summary>
    /// Execute permission for others.
    /// </summary>
    OtherExecute = 1,
    /// Write permission for others.
    OtherWrite = 2,
    /// <summary>
    /// Read permission for others.
    /// </summary>
    OtherRead = 4,
    /// <summary>
    /// Execute permission for group.
    /// </summary>
    GroupExecute = 8,
    /// <summary>
    /// Write permission for group.
    /// </summary>
    GroupWrite = 16,
    /// <summary>
    /// Read permission for group.
    /// </summary>
    GroupRead = 32,
    /// <summary>
    /// Execute permission for owner.
    /// </summary>
    UserExecute = 64,
    /// <summary>
    /// Write permission for owner.
    /// </summary>
    UserWrite = 128,
    /// <summary>
    /// Read permission for owner.
    /// </summary>
    UserRead = 256,
    /// <summary>
    /// Sticky bit permission.
    /// </summary>
    StickyBit = 512,
    /// <summary>
    /// Set group permission.
    /// </summary>
    SetGroup = 1024,
    /// <summary>
    /// Set user permission.
    /// </summary>
    SetUser = 2048
}

#endif