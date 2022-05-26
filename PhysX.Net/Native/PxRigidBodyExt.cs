using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxRigidBodyExt
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBodyExt_UpdateMassAndInertia")]
    public static extern bool UpdateMassAndInertia(IntPtr body, float density, Vector3 massLocalPose, bool includeNonSimShapes);
}
