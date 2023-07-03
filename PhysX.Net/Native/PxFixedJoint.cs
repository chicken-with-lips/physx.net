using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxFixedJoint
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxFixedJoint_Create")]
    public static extern IntPtr Create(IntPtr physics, IntPtr actor0, ref PxTransform localFrame0, IntPtr actor1, ref PxTransform localFrame1);
}
