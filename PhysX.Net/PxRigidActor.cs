namespace ChickenWithLips.PhysX;

/// <summary>
/// PxRigidActor represents a base class shared between dynamic and static rigid bodies in the physics SDK.
/// </summary>
public interface PxRigidActor : PxActor
{
    /// <summary>
    /// Retrieves or sets the actors world space transform.
    /// </summary>
    /// <remarks><see cref="SetGlobalPose"/> to control the sleeping state.</remarks>
    public PxTransform GlobalPose { get; set; }
    
    /// <summary>
    /// Attach a shared shape to an actor.
    /// </summary>
    /// <remarks>
    /// <para>Mass properties of dynamic rigid actors will not automatically be recomputed to reflect the new mass
    /// distribution implied by the shape. Follow this call with a call to the PhysX extensions method
    /// <see cref="UpdateMassAndInertia"/> to do that.</para>
    /// <para>Attaching a triangle mesh, heightfield or plane geometry shape configured as eSIMULATION_SHAPE is not
    /// supported for non-kinematic PxRigidDynamic instances. Sleeping: Does NOT wake the actor up automatically.</para>
    /// </remarks>
    /// <param name="shape">The shape to attach.</param>
    /// <returns></returns>
    public bool AttachShape(PxShape shape);

    /// <summary>
    /// Method for setting an actor's pose in the world.
    /// </summary>
    /// <remarks>
    /// <para>This method instantaneously changes the actor space to world space transformation.</para>
    /// <para>This method is mainly for dynamic rigid bodies (see #PxRigidDynamic). Calling this method on static actors
    /// is likely to result in a performance penalty, since internal optimization structures for static actors may need
    /// to be recomputed. In addition, moving static actors will not interact correctly with dynamic actors or joints.</para>
    /// <para>To directly control an actor's position and have it correctly interact with dynamic bodies and joints,
    /// create a dynamic body with the PxRigidBodyFlag::eKINEMATIC flag, then use the setKinematicTarget() commands to
    /// define its path.</para>
    /// <para>Even when moving dynamic actors, exercise restraint in making use of this method. Where possible, avoid:
    /// - moving actors into other actors, thus causing overlap (an invalid physical state)
    /// - moving an actor that is connected by a joint to another away from the other (thus causing joint error)
    /// </para>
    /// <para><b>Sleeping:</b> This call wakes dynamic actors if they are sleeping and the autowake parameter is true
    /// (default).</para>
    /// </remarks>
    /// <param name="transform">Transformation from the actors local frame to the global frame. <b>Range:</b> rigid body transform.</param>
    /// <param name="autowake">Whether to wake the object if it is dynamic. This parameter has no effect for static or kinematic actors. If true and the current wake counter value is smaller than #PxSceneDesc::wakeCounterResetValue it will get increased to the reset value.</param>
    public void SetGlobalPose(PxTransform transform, bool autowake = true);
}

public abstract class PxRigidActor<T> : PxActor<T>, PxRigidActor
    where T : class
{
    public PxTransform GlobalPose {
        get => Native.PxRigidActor.GetGlobalPose(NativePtr);
        set => SetGlobalPose(value);
    }

    protected PxRigidActor(IntPtr ptr, bool automaticallyRegisterInCache)
        : base(ptr, automaticallyRegisterInCache)
    {
    }

    public bool AttachShape(PxShape shape)
    {
        return Native.PxRigidActor.AttachShape(NativePtr, shape.NativePtr);
    }

    public void SetGlobalPose(PxTransform transform, bool autowake = true)
    {
        Native.PxRigidActor.SetGlobalPose(NativePtr, ref transform, autowake);
    }
}
