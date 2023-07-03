namespace ChickenWithLips.PhysX;

/// <summary>
/// Base interface providing common functionality for PhysX joints.
/// </summary>
public abstract class PxJoint : PxBase<PxJoint>
{
    protected PxJoint(IntPtr ptr, bool automaticallyRegisterInCache = false)
        : base(ptr, automaticallyRegisterInCache)
    {
    }
}

/// <summary>
/// A fixed joint permits no relative movement between two bodies. ie the bodies are glued together.
/// </summary>
public class PxFixedJoint : PxJoint
{
    internal PxFixedJoint(IntPtr ptr, bool automaticallyRegisterInCache)
        : base(ptr, automaticallyRegisterInCache)
    {
    }

    public static PxFixedJoint Create(PxPhysics physics, PxActor? actor0, PxTransform localFrame0, PxActor? actor1, PxTransform localFrame1)
    {
        return new PxFixedJoint(
            Native.PxFixedJoint.Create(
                physics.NativePtr,
                actor0?.NativePtr ?? IntPtr.Zero,
                ref localFrame0,
                actor1?.NativePtr ?? IntPtr.Zero,
                ref localFrame1
            ),
            true
        );
    }
}

/// <summary>
/// A joint that maintains an upper or lower bound (or both) on the distance between two points on different objects.
/// </summary>
public class PxDistanceJoint : PxJoint
{
    internal PxDistanceJoint(IntPtr ptr, bool automaticallyRegisterInCache)
        : base(ptr, automaticallyRegisterInCache)
    {
    }

    /// <summary>The current distance of the joint.</summary>
    public float Distance => Native.PxDistanceJoint.GetDistance(NativePtr);

    /// <summary>
    /// The allowed minimum distance for the joint.
    /// </summary>
    public float MinDistance {
        get => Native.PxDistanceJoint.GetMinDistance(NativePtr);
        set => Native.PxDistanceJoint.SetMinDistance(NativePtr, value);
    }

    /// <summary>
    /// The allowed maximum distance for the joint.
    /// </summary>
    public float MaxDistance {
        get => Native.PxDistanceJoint.GetMaxDistance(NativePtr);
        set => Native.PxDistanceJoint.SetMaxDistance(NativePtr, value);
    }

    /// <summary>
    /// Set a single flag specific to a Distance Joint to true or false.
    /// </summary
    public void SetDistanceJointFlag(PxDistanceJointFlag flag, bool value)
    {
        Native.PxDistanceJoint.SetDistanceJointFlag(NativePtr, flag, value);
    }

    public static PxDistanceJoint Create(PxPhysics physics, PxActor? actor0, PxTransform localFrame0, PxActor? actor1, PxTransform localFrame1)
    {
        return new PxDistanceJoint(
            Native.PxDistanceJoint.Create(
                physics.NativePtr,
                actor0?.NativePtr ?? IntPtr.Zero,
                ref localFrame0,
                actor1?.NativePtr ?? IntPtr.Zero,
                ref localFrame1
            ),
            true
        );
    }
}

/// <summary>
/// Flags for configuring the drive of a PxDistanceJoint.
/// </summary>
[Flags]
public enum PxDistanceJointFlag : ushort
{
    MaxDistanceEnabled = 1 << 1,
    MinDistanceEnabled = 1 << 2,
    SpringEnabled = 1 << 3
}
