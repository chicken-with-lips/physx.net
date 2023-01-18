using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxRigidBody
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_AddForce")]
    public static extern void AddForce(IntPtr actor, ref Vector3 force, PxForceMode mode, bool autowake = true);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_AddTorque")]
    public static extern void AddTorque(IntPtr actor, ref Vector3 torque, PxForceMode mode, bool autowake = true);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_ClearForce")]
    public static extern void ClearForce(IntPtr actor, PxForceMode mode);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_ClearTorque")]
    public static extern void ClearTorque(IntPtr actor, PxForceMode mode);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_SetForceAndTorque")]
    public static extern void SetForceAndTorque(IntPtr actor, ref Vector3 force, ref Vector3 torque, PxForceMode mode);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_SetLinearDamping")]
    public static extern void SetLinearDamping(IntPtr actor, float value);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_GetLinearDamping")]
    public static extern float GetLinearDamping(IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_SetAngularDamping")]
    public static extern void SetAngularDamping(IntPtr actor, float value);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_GetAngularDamping")]
    public static extern float GetAngularDamping(IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_GetLinearVelocity")]
    public static extern Vector3 GetLinearVelocity(IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_GetAngularVelocity")]
    public static extern Vector3 GetAngularVelocity(IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_SetMassSpaceInertiaTensor")]
    public static extern void SetMassSpaceInertiaTensor(IntPtr actor, ref Vector3 m);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_GetMassSpaceInertiaTensor")]
    public static extern Vector3 GetMassSpaceInertiaTensor(IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_GetMassSpaceInvInertiaTensor")]
    public static extern Vector3 GetMassSpaceInvInertiaTensor(IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_GetCMassLocalPose")]
    public static extern PxTransform GetCMassLocalPose(IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxRigidBody_SetCMassLocalPose")]
    public static extern void SetCMassLocalPose(IntPtr actor, ref PxTransform pose);
}
