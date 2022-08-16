using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxRigidBody
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_AddForce")]
    public static extern void AddForce(IntPtr actor, ref Vector3 force, PxForceMode mode, bool autowake = true);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_AddTorque")]
    public static extern void AddTorque(IntPtr actor, ref Vector3 torque, PxForceMode mode, bool autowake = true);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_SetLinearDamping")]
    public static extern void SetLinearDamping(IntPtr actor, float value);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_GetLinearDamping")]
    public static extern float GetLinearDamping(IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_SetAngularDamping")]
    public static extern void SetAngularDamping(IntPtr actor, float value);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_GetAngularDamping")]
    public static extern float GetAngularDamping(IntPtr actor);
}
