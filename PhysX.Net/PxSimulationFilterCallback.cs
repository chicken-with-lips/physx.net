namespace ChickenWithLips.PhysX.Net;

/// <summary>
/// Filter callback to specify handling of collision pairs.
/// </summary>
/// <remarks>
/// <para>This class is provided to implement more complex and flexible collision pair filtering logic, for instance,
/// taking the state of the user application into account. Filter callbacks also give the user the opportunity to track
/// collision pairs and update their filter state. You might want to check the documentation on
/// #PxSimulationFilterShader as well since it includes more general information on filtering.</para>
/// <para>SDK state should not be modified from within the callbacks. In particular objects should not be created or
/// destroyed. If state modification is needed then the changes should be stored to a buffer and performed after the
/// simulation step.</para>
/// <para>The callbacks may execute in user threads or simulation threads, possibly simultaneously. The corresponding
/// objects may have been deleted by the application earlier in the frame. It is the application's responsibility to
/// prevent race conditions arising from using the SDK API in the callback while an application thread is making write
/// calls to the scene, and to ensure that the callbacks are thread-safe. Return values which depend on when the
/// callback is called during the frame will introduce nondeterminism into the simulation.</para>
/// </remarks>
public abstract class PxSimulationFilterCallback : PxBase<PxSimulationFilterCallback>
{
    private Native.PxSimulationFilterCallback.OnStatusChange _onStatusChange;
    
    public PxSimulationFilterCallback() : base(IntPtr.Zero)
    {
        _onStatusChange = OnStatusChange;
        
        NativePtr = Native.PxSimulationFilterCallback.Create(
            _onStatusChange
        );

        ManuallyRegisterCache(NativePtr, this);
    }

    protected abstract bool OnStatusChange(ref uint pairId, ref PxPairFlag pairFlags, ref PxFilterFlag filterFlags);
}
