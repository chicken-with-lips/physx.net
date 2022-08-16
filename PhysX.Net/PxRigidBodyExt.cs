using System.Numerics;

namespace ChickenWithLips.PhysX;

public static class PxRigidBodyExt
{
    public static bool UpdateMassAndInertia(PxRigidBody body, float density, bool includeNonSimShapes = false)
    {
        return UpdateMassAndInertia(body, density, Vector3.Zero, includeNonSimShapes);
    }

    public static bool UpdateMassAndInertia(PxRigidBody body, float density, Vector3 massLocalPose, bool includeNonSimShapes = false)
    {
        return Native.PxRigidBodyExt.UpdateMassAndInertia(body.NativePtr, density, massLocalPose, includeNonSimShapes);
    }
}
