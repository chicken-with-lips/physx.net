namespace ChickenWithLips.PhysX.Net;

public interface PxErrorCallback
{
    public IntPtr NativePtr { get; }
}

public abstract class PxErrorCallback<T> : PxBase<T>, PxErrorCallback
    where T : class
{
    protected PxErrorCallback(IntPtr ptr, bool automaticallyRegisterInCache) : base(ptr, automaticallyRegisterInCache)
    {
    }
}
