using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxRigidDynamic
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidDynamic_SetRigidDynamicFlag")]
    public static extern bool SetRigidDynamicFlag(IntPtr actor, PxRigidBodyFlag flag, bool value);
    
    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidDynamic_SetRigidDynamicLockFlags")]
    public static extern void SetRigidDynamicLockFlags(IntPtr actor, PxRigidDynamicLockFlag flags);
    
    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidDynamic_GetRigidDynamicLockFlags")]
    public static extern PxRigidDynamicLockFlag GetRigidDynamicLockFlags(IntPtr actor);
}
