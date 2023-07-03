using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxDistanceJoint
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxDistanceJoint_Create")]
    public static extern IntPtr Create(IntPtr physics, IntPtr actor0, ref PxTransform localFrame0, IntPtr actor1, ref PxTransform localFrame1);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxDistanceJoint_GetDistance")]
    public static extern float GetDistance(IntPtr joint);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxDistanceJoint_SetMinDistance")]
    public static extern void SetMinDistance(IntPtr joint, float distance);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxDistanceJoint_GetMinDistance")]
    public static extern float GetMinDistance(IntPtr joint);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxDistanceJoint_SetMaxDistance")]
    public static extern void SetMaxDistance(IntPtr joint, float distance);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxDistanceJoint_GetMaxDistance")]
    public static extern float GetMaxDistance(IntPtr joint);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxDistanceJoint_SetDistanceJointFlag")]
    public static extern void SetDistanceJointFlag(IntPtr joint, PxDistanceJointFlag flag, bool value);
}
