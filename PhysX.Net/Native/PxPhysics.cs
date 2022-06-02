using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxPhysics
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxPhysics_CreateScene")]
    public static extern IntPtr CreateScene(IntPtr physics, ref PxSceneDescTransfer sceneDesc);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxPhysics_CreateMaterial")]
    public static extern IntPtr CreateMaterial(IntPtr physics, float staticFriction, float dynamicFriction, float restitution);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxPhysics_CreateShape")]
    public static extern IntPtr CreateShape(IntPtr physics, ref PxBoxGeometry geometry, IntPtr material, bool isExclusive, PxShapeFlag shapeFlags);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxPhysics_CreateShape")]
    public static extern IntPtr CreateShape(IntPtr physics, ref PxSphereGeometry geometry, IntPtr material, bool isExclusive, PxShapeFlag shapeFlags);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxPhysics_CreateRigidDynamic")]
    public static extern IntPtr CreateRigidDynamic(IntPtr physics, ref PxTransform transform);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxPhysics_CreateRigidStatic")]
    public static extern IntPtr CreateRigidStatic(IntPtr physics, ref PxTransform transform);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxPhysics_GetTolerancesScale")]
    public static extern ref PxTolerancesScale GetTolerancesScale(IntPtr physics);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxPhysics_InitExtensions")]
    public static extern bool InitExtensions(IntPtr physics, IntPtr pvd);
}
