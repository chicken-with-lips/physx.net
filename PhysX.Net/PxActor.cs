namespace ChickenWithLips.PhysX;

/// <summary>
/// PxActor is the base class for the main simulation objects in the physics SDK.
/// </summary>
public interface PxActor
{
    public string Name { get; }
    public IntPtr NativePtr { get; }

    public void SetActorFlag(PxActorFlag flag, bool value);
}

public abstract class PxActor<T> : PxBase<T>, PxActor
    where T : class
{
    public string Name => Native.PxActor.GetName(NativePtr);

    protected PxActor(IntPtr ptr, bool automaticallyRegisterInCache)
        : base(ptr, automaticallyRegisterInCache)
    {
    }

    public void SetActorFlag(PxActorFlag flag, bool value)
    {
	    Native.PxActor.SetActorFlag(NativePtr, flag, value);
    }
}

/// <summary>
/// Flags which control the behavior of an actor.
/// </summary>
public enum PxActorFlag : byte
{
    /**
		\brief Enable debug renderer for this actor

		@see PxScene.getRenderBuffer() PxRenderBuffer PxVisualizationParameter
		*/
    Visualization = (1 << 0),

    /**
		\brief Disables scene gravity for this actor
		*/
    DisableGravity = (1 << 1),

    /**
		\brief Enables the sending of PxSimulationEventCallback::onWake() and PxSimulationEventCallback::onSleep() notify events

		@see PxSimulationEventCallback::onWake() PxSimulationEventCallback::onSleep()
		*/
    SendSleepNotifies = (1 << 2),

    /**
		\brief Disables simulation for the actor.
		
		\note This is only supported by PxRigidStatic and PxRigidDynamic actors and can be used to reduce the memory footprint when rigid actors are
		used for scene queries only.

		\note Setting this flag will remove all constraints attached to the actor from the scene.

		\note If this flag is set, the following calls are forbidden:
		\li PxRigidBody: setLinearVelocity(), setAngularVelocity(), addForce(), addTorque(), clearForce(), clearTorque()
		\li PxRigidDynamic: setKinematicTarget(), setWakeCounter(), wakeUp(), putToSleep()

		\par <b>Sleeping:</b>
		Raising this flag will set all velocities and the wake counter to 0, clear all forces, clear the kinematic target, put the actor
		to sleep and wake up all touching actors from the previous frame.
		*/
    DisableSimulation = (1 << 3)
}
