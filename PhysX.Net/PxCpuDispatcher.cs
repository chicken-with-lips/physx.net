namespace ChickenWithLips.PhysX;

public interface PxCpuDispatcher
{
    public IntPtr NativePtr { get; }
    public uint WorkerCount { get; }
}

public abstract class PxCpuDispatcher<T> : PxBase<T>, PxCpuDispatcher
    where T : class
{
    public uint WorkerCount => Native.PxCpuDispatcher.GetWorkerCount(NativePtr);
    
    protected PxCpuDispatcher(IntPtr ptr, bool automaticallyRegisterInCache) : base(ptr, automaticallyRegisterInCache)
    {
    }
}
