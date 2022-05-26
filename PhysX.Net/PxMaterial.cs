namespace ChickenWithLips.PhysX.Net;

/// <summary>
/// Material class to represent a set of surface properties.
/// </summary>
public class PxMaterial : PxBase<PxMaterial>
{
    private PxMaterial(IntPtr ptr) : base(ptr)
    {
    }

    public static PxMaterial Create(PxPhysics physics, float staticFriction, float dynamicFriction, float restitution)
    {
        return new PxMaterial(Native.PxPhysics.CreateMaterial(physics.NativePtr, staticFriction, dynamicFriction, restitution));
    }
}
