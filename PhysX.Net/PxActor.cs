namespace ChickenWithLips.PhysX.Net;

/// <summary>
/// PxActor is the base class for the main simulation objects in the physics SDK.
/// </summary>
public interface PxActor
{
    public IntPtr NativePtr { get; }
}

public abstract class PxActor<T> : PxBase<T>, PxActor
    where T : class
{
    protected PxActor(IntPtr ptr, bool automaticallyRegisterInCache)
        : base(ptr, automaticallyRegisterInCache)
    {
    }
}
