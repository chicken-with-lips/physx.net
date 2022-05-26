using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxRigidActor
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidActor_AttachShape")]
    public static extern bool AttachShape(IntPtr actor, IntPtr shape);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidActor_GetGlobalPose")]
    public static extern PxTransform GetGlobalPose(IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidActor_SetGlobalPose")]
    public static extern void SetGlobalPose(IntPtr actor, ref PxTransform pose, bool autowake);
}
