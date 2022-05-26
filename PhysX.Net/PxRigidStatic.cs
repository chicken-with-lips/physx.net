namespace ChickenWithLips.PhysX.Net;

/// <summary>
/// PxRigidDynamic represents a dynamic rigid simulation object in the physics SDK.
/// </summary>
public class PxRigidStatic : PxRigidBody<PxRigidStatic>
{
    protected PxRigidStatic(IntPtr ptr, bool automaticallyRegisterInCache)
        : base(ptr, automaticallyRegisterInCache)
    {
    }

    public static PxRigidStatic Create(PxPhysics physics, PxTransform transform)
    {
        return new PxRigidStatic(Native.PxPhysics.CreateRigidStatic(physics.NativePtr, ref transform), true);
    }
}
