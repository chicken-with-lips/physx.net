namespace ChickenWithLips.PhysX.Net;

/// <summary>
/// PxRigidDynamic represents a dynamic rigid simulation object in the physics SDK.
/// </summary>
public class PxRigidDynamic : PxRigidBody<PxRigidDynamic>
{
    /// <summary>
    /// Gets or sets the lock flags.
    /// </summary>
    public PxRigidDynamicLockFlag LockFlags {
        get => Native.PxRigidDynamic.GetRigidDynamicLockFlags(NativePtr);
        set => Native.PxRigidDynamic.SetRigidDynamicLockFlags(NativePtr, value);
    }
    
    protected PxRigidDynamic(IntPtr ptr, bool automaticallyRegisterInCache)
        : base(ptr, automaticallyRegisterInCache)
    {
    }

    public static PxRigidDynamic Create(PxPhysics physics, PxTransform transform)
    {
        return new PxRigidDynamic(Native.PxPhysics.CreateRigidDynamic(physics.NativePtr, ref transform), true);
    }
}

/// <summary>Collection of flags providing a mechanism to lock motion along/around a specific axis.</summary>
[Flags]
public enum PxRigidDynamicLockFlag : byte
{
    LockLinearX = (1 << 0),
    LockLinearY = (1 << 1),
    LockLinearZ = (1 << 2),
    LockAngularX = (1 << 3),
    LockAngularY = (1 << 4),
    LockAngularZ = (1 << 5),
};
