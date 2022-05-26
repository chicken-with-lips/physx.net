using System.Numerics;

namespace ChickenWithLips.PhysX.Net;

/// <summary>
/// PxRigidBody is a base class shared between dynamic rigid body objects.
/// </summary>
public interface PxRigidBody : PxRigidActor
{
    /// <summary>
    /// Gets or sets the angular damping coefficient.
    /// </summary>
    /// <remarks>
    /// Zero represents no damping. The angular damping coefficient must be nonnegative. Default: 0.05.
    /// </remarks>
    public float AngularDamping { get; set; }

    /// <summary>
    /// Gets or sets the linear damping coefficient.
    /// </summary>
    public float LinearDamping { get; set; }

    /// <summary>
    /// Raises or clears a particular rigid body flag.
    /// </summary>
    public void SetFlag(PxRigidBodyFlag flag, bool value);

    /// <summary>
    /// Applies a force (or impulse) defined in the global coordinate frame to the actor at its center of mass.
    /// </summary>
    /// <remarks>
    /// <para><b>This will not induce a torque</b>.</para>
    /// <para>::PxForceMode determines if the force is to be conventional or impulsive.</para>
    /// <para>Each actor has an acceleration and a velocity change accumulator which are directly modified using the
    /// modes PxForceMode::eACCELERATION and PxForceMode::eVELOCITY_CHANGE respectively.  The modes PxForceMode::eFORCE
    /// and PxForceMode::eIMPULSE also modify these same accumulators and are just short hand for multiplying the vector
    /// parameter by inverse mass and then using PxForceMode::eACCELERATION and PxForceMode::eVELOCITY_CHANGE
    /// respectively.</para>
    /// <para>It is invalid to use this method if the actor has not been added to a scene already or if
    /// PxActorFlag::eDISABLE_SIMULATION is set.</para>
    /// <para>The force modes PxForceMode::eIMPULSE and PxForceMode::eVELOCITY_CHANGE can not be applied to articulation
    /// links.</para>
    /// <para>If this is called on an articulation link, only the link is updated, not the entire articulation.</para>
    /// <para>See #PxRigidBodyExt::computeVelocityDeltaFromImpulse for details of how to compute the change in linear
    /// velocity that will arise from the application of an impulsive force, where an impulsive force is applied force
    /// multiplied by a timestep.</para>
    /// <para><b>Sleeping:</b> This call wakes the actor if it is sleeping and the autowake parameter is true (default)
    /// or the force is non-zero.</para>
    /// </remarks>
    /// <param name="force">Force/Impulse to apply defined in the global frame.</param>
    /// <param name="mode">The mode to use when applying the force/impulse(see #PxForceMode).</param>
    /// <param name="autowake">Specify if the call should wake up the actor if it is currently asleep. If true and the current wake counter value is smaller than #PxSceneDesc::wakeCounterResetValue it will get increased to the reset value.</param>
    public void AddForce(Vector3 force, PxForceMode mode, bool autowake = true);

    /// <summary>
    /// Applies an impulsive torque defined in the global coordinate frame to the actor.
    /// </summary>
    /// <remarks>
    /// <para>::PxForceMode determines if the torque is to be conventional or impulsive.</para>
    /// <para>Each actor has an angular acceleration and an angular velocity change accumulator which are directly
    /// modified using the modes PxForceMode::eACCELERATION and PxForceMode::eVELOCITY_CHANGE respectively.  The modes
    /// PxForceMode::eFORCE and PxForceMode::eIMPULSE also modify these same accumulators and are just short hand for
    /// multiplying the vector parameter by inverse inertia and then using PxForceMode::eACCELERATION and
    /// PxForceMode::eVELOCITY_CHANGE respectively.</para>
    /// <para>It is invalid to use this method if the actor has not been added to a scene already or if
    /// PxActorFlag::eDISABLE_SIMULATION is set.</para>
    /// <para>The force modes PxForceMode::eIMPULSE and PxForceMode::eVELOCITY_CHANGE can not be applied to articulation
    /// links.</para>
    /// <para>If this called on an articulation link, only the link is updated, not the entire articulation.</para>
    /// <para>See #PxRigidBodyExt::computeVelocityDeltaFromImpulse for details of how to compute the change in angular
    /// velocity that will arise from the application of an impulsive torque, where an impulsive torque is an applied
    /// torque multiplied by a timestep.</para>
    /// <para><b>Sleeping:</b> This call wakes the actor if it is sleeping and the autowake parameter is true (default)
    /// or the torque is non-zero.</para>
    /// </remarks>
    /// <param name="torque">Torque to apply defined in the global frame.</param>
    /// <param name="mode">The mode to use when applying the force/impulse(see #PxForceMode).</param>
    /// <param name="autowake">Whether to wake up the object if it is asleep. If true and the current wake counter value is smaller than #PxSceneDesc::wakeCounterResetValue it will get increased to the reset value.</param>
    public void AddTorque(Vector3 torque, PxForceMode mode, bool autowake = true);
}

public abstract class PxRigidBody<T> : PxRigidActor<T>, PxRigidBody
    where T : class
{
    #region Properties

    public float AngularDamping {
        get => Native.PxRigidBody.GetAngularDamping(NativePtr);
        set => Native.PxRigidBody.SetAngularDamping(NativePtr, value);
    }

    /// <summary>
    /// Gets or sets the linear damping coefficient.
    /// </summary>
    public float LinearDamping {
        get => Native.PxRigidBody.GetLinearDamping(NativePtr);
        set => Native.PxRigidBody.SetLinearDamping(NativePtr, value);
    }

    #endregion

    #region Methods

    protected PxRigidBody(IntPtr ptr, bool automaticallyRegisterInCache)
        : base(ptr, automaticallyRegisterInCache)
    {
    }

    public void SetFlag(PxRigidBodyFlag flag, bool value)
    {
        Native.PxRigidDynamic.SetRigidDynamicFlag(NativePtr, flag, value);
    }

    public void AddForce(Vector3 force, PxForceMode mode, bool autowake = true)
    {
        Native.PxRigidBody.AddForce(NativePtr, ref force, mode, autowake);
    }

    public void AddTorque(Vector3 torque, PxForceMode mode, bool autowake = true)
    {
        Native.PxRigidBody.AddTorque(NativePtr, ref torque, mode, autowake);
    }

    #endregion
}

/// <summary>
/// Collection of flags describing the behavior of a rigid body.
/// </summary>
public enum PxRigidBodyFlag
{
    /// <summary>Enables kinematic mode for the actor.</summary>
    /// <remarks>
    /// <para>Kinematic actors are special dynamic actors that are not influenced by forces (such as gravity), and have
    /// no momentum. They are considered to have infinite mass and can be moved around the world using the
    /// setKinematicTarget() method. They will push regular dynamic actors out of the way. Kinematics will not collide
    /// with static or other kinematic objects.</para>
    /// <para>Kinematic actors are great for moving platforms or characters, where direct motion control is desired.</para>
    /// <para>You can not connect Reduced joints to kinematic actors. Lagrange joints work ok if the platform is moving
    /// with a relatively low, uniform velocity.</para>
    /// <para><b>Sleeping:</b>
    /// <list type="bullet">
    ///   <item>Setting this flag on a dynamic actor will put the actor to sleep and set the velocities to 0.</item>
    ///   <item>If this flag gets cleared, the current sleep state of the actor will be kept.</item>
    /// </list>
    /// <para>Kinematic actors are incompatible with CCD so raising this flag will automatically clear eENABLE_CCD</para>
    /// </remarks>
    Kinematic = (1 << 0),

    /// <summary>Use the kinematic target transform for scene queries.</para>
    /// <remarks>
    /// <para>If this flag is raised, then scene queries will treat the kinematic target transform as the current pose
    /// of the body (instead of using the actual pose). Without this flag, the kinematic target will only take effect
    /// with respect to scene queries after a simulation step.</para>
    /// </remarks>
    UseKinematicTargetForSceneQueries = (1 << 1),

    /// <summary>Enables swept integration for the actor.</para>
    /// <remarks>
    /// <para>If this flag is raised and CCD is enabled on the scene, then this body will be simulated by the CCD system
    /// to ensure that collisions are not missed due to high-speed motion. Note individual shape pairs still need to
    /// enable PxPairFlag::eDETECT_CCD_CONTACT in the collision filtering to enable the CCD to respond to individual
    /// interactions.</para>
    /// <para>Kinematic actors are incompatible with CCD so this flag will be cleared automatically when raised on a
    /// kinematic actor</para>
    /// </remarks>
    EnableCcd = (1 << 2),

    /// <summary>
    /// Enabled CCD in swept integration for the actor.
    /// </summary>
    /// <remarks>
    /// <para>If this flag is raised and CCD is enabled, CCD interactions will simulate friction. By default, friction
    /// is disabled in CCD interactions because CCD friction has been observed to introduce some simulation artifacts.
    /// CCD friction was enabled in previous versions of the SDK. Raising this flag will result in behavior that is a
    /// closer match for previous versions of the SDK.
    /// </para>
    /// <para>This flag requires PxRigidBodyFlag::eENABLE_CCD to be raised to have any effect.</para>
    /// </remarks>
    EnableCcdFriction = (1 << 3),

    /// <summary>
    /// Register a rigid body for reporting pose changes by the simulation at an early stage.
    /// </summary>
    /// <remarks>
    /// <para>Sometimes it might be advantageous to get access to the new pose of a rigid body as early as possible and
    /// not wait until the call to fetchResults() returns. Setting this flag will schedule the rigid body to get
    /// reported in #PxSimulationEventCallback::onAdvance(). Please refer to the documentation of that callback to
    /// understand the behavior and limitations of this functionality.</para>
    /// </remarks>
    EnablePoseIntegrationPreview = (1 << 4),

    /// <summary>
    /// Register a rigid body to dynamically adjust contact offset based on velocity. This can be used to achieve a CCD
    /// effect.
    /// </summary>
    EnableSpeculativeCcd = (1 << 5),

    /// <summary>
    /// Permit CCD to limit maxContactImpulse. This is useful for use-cases like a destruction system but can cause
    /// visual artefacts so is not enabled by default.
    /// </summary>
    EnableCcdMaxContactImpulse = (1 << 6),

    /// <summary>
    /// Carries over forces/accelerations between frames, rather than clearing them.
    /// </summary>
    RetainAccelerations = (1 << 7),

    /// <summary>
    /// Forces kinematic-kinematic pairs notifications for this actor.
    /// </summary>
    /// <remarks>
    /// <para>This flag overrides the global scene-level PxPairFilteringMode setting for kinematic actors. This is
    /// equivalent to having <see cref="PxPairFilteringMode.Keep"/> for pairs involving this actor.</para>
    /// <para>A particular use case is when you have a large amount of kinematic actors, but you are only interested in
    /// interactions between a few of them. In this case it is best to use set PxSceneDesc.kineKineFilteringMode =
    /// <see cref="PxPairFilteringMode.Kill"/>, and then raise the <see cref="ForceKineKineNotifications"/> flag on the
    /// small set of kinematic actors that need notifications.</para>
    /// <para>This has no effect if PxRigidBodyFlag::eKINEMATIC is not set.</para>
    /// <para>Changing this flag at runtime will not have an effect until you remove and re-add the actor to the scene.</para>
    /// </remarks>
    ForceKineKineNotifications = (1 << 8),

    /// <summary>
    /// Forces static-kinematic pairs notifications for this actor.
    /// </summary>
    /// <remarks>
    /// <para>Similar to <see cref="ForceKineKineNotifications"/>, but for static-kinematic interactions. This has no
    /// effect if <see cref="PxRigidBodyFlag.Kinematic"/> is not set.</para>
    /// <para>Changing this flag at runtime will not have an effect until you remove and re-add the actor to the scene.</para>
    /// </remarks>
    ForceStaticKineNotifications = (1 << 9),

    /// <summary>
    /// Reserved for internal usage.
    /// </summary>
    Reserved = (1 << 15)
}
