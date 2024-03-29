using ChickenWithLips.PhysX.Native;

namespace ChickenWithLips.PhysX;

public class PxPhysics : PxBase<PxPhysics>
{
    public PxTolerancesScale TolerancesScale => Native.PxPhysics.GetTolerancesScale(NativePtr);

    private PxPhysics(IntPtr ptr) : base(ptr)
    {
    }

    public PxScene CreateScene(PxSceneDesc sceneDesc)
    {
        return PxScene.Create(this, sceneDesc);
    }

    /// <summary>
    /// Creates a new material with default properties.
    /// </summary>
    /// <param name="staticFriction">The coefficient of static friction.</param>
    /// <param name="dynamicFriction">The coefficient of dynamic friction.</param>
    /// <param name="restitution">The coefficient of restitution.</param>
    /// <returns>The new material.</returns>
    public PxMaterial CreateMaterial(float staticFriction, float dynamicFriction, float restitution)
    {
        return PxMaterial.Create(this, staticFriction, dynamicFriction, restitution);
    }

    /// <summary>
    /// Creates a shape which may be attached to multiple actors.
    /// </summary>
    /// <remarks>
    /// Shared shapes are not mutable when they are attached to an actor.
    /// </remarks>
    /// <param name="geometry">The geometry for the shape.</param>
    /// <param name="material">The material for the shape.</param>
    /// <param name="isExclusive">Whether this shape is exclusive to a single actor or maybe be shared.</param>
    /// <param name="shapeFlags">The PxShapeFlags to be set</param>
    public PxShape CreateShape(PxGeometry geometry, PxMaterial material, bool isExclusive = false, PxShapeFlag shapeFlags = PxShapeFlag.Visualization | PxShapeFlag.SceneQueryShape | PxShapeFlag.SimulationShape)
    {
        return PxShape.Create(this, geometry, material, isExclusive, shapeFlags);
    }

    /// <summary>
    /// Creates a dynamic rigid actor with the specified pose and all other fields initialized to their default values.
    /// </summary>
    /// <param name="transform">The initial pose of the actor. Must be a valid transform.</param>
    public PxRigidDynamic CreateRigidDynamic(PxTransform transform)
    {
        return PxRigidDynamic.Create(this, transform);
    }

    /// <summary>
    /// Creates a static rigid actor with the specified pose and all other fields initialized to their default values.
    /// </summary>
    /// <param name="transform">The initial pose of the actor. Must be a valid transform.</param>
    public PxRigidStatic CreateRigidStatic(PxTransform transform)
    {
        return PxRigidStatic.Create(this, transform);
    }

    /// <summary>
    /// Create a fixed joint.
    /// </summary>
    /// <param name="actor0">An actor to which the joint is attached. NULL may be used to attach the joint to a specific point in the world frame.</param>
    /// <param name="localFrame0">The position and orientation of the joint relative to actor0.</param>
    /// <param name="actor1">An actor to which the joint is attached. NULL may be used to attach the joint to a specific point in the world frame.</param>
    /// <param name="localFrame1">The position and orientation of the joint relative to actor1.</param>
    public PxFixedJoint CreateFixedJoint(PxActor? actor0, PxTransform localFrame0, PxActor? actor1, PxTransform localFrame1)
    {
        return PxFixedJoint.Create(this, actor0, localFrame0, actor1, localFrame1);
    }

    /// <summary>
    /// Create a distance joint.
    /// </summary>
    /// <param name="actor0">An actor to which the joint is attached. NULL may be used to attach the joint to a specific point in the world frame.</param>
    /// <param name="localFrame0">The position and orientation of the joint relative to actor0.</param>
    /// <param name="actor1">An actor to which the joint is attached. NULL may be used to attach the joint to a specific point in the world frame.</param>
    /// <param name="localFrame1">The position and orientation of the joint relative to actor1.</param>
    public PxDistanceJoint CreateDistanceJoint(PxActor? actor0, PxTransform localFrame0, PxActor? actor1, PxTransform localFrame1)
    {
        return PxDistanceJoint.Create(this, actor0, localFrame0, actor1, localFrame1);
    }

    /// <summary>
    /// Initialize the PhysXExtensions library. This should be called before calling any functions or methods in
    /// extensions which may require allocation.
    /// </summary>
    public bool InitExtensions(PxPvd pvd = null)
    {
        return Native.PxPhysics.InitExtensions(NativePtr, pvd?.NativePtr ?? IntPtr.Zero);
    }

    public static PxPhysics Create(PxFoundation foundation, uint version, PxTolerancesScale scale, bool trackOutstandingAllocations, PxPvd? pvd)
    {
        return GetOrCreateCache(Native.PxFoundation.CreatePhysics(foundation.NativePtr, version, ref scale, trackOutstandingAllocations, pvd?.NativePtr ?? IntPtr.Zero), ptr => new PxPhysics(ptr));
    }
}
