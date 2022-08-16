namespace ChickenWithLips.PhysX;

public interface PxAllocatorCallback
{
    public IntPtr NativePtr { get; }
}

public abstract class PxAllocatorCallback<T> : PxBase<T>, PxAllocatorCallback
    where T : class
{
    protected PxAllocatorCallback(IntPtr ptr, bool automaticallyRegisterInCache)
        : base(ptr, automaticallyRegisterInCache)
    {
    }
}
