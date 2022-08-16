namespace ChickenWithLips.PhysX;

public class PxDefaultCpuDispatcher : PxCpuDispatcher<PxDefaultCpuDispatcher>
{
    private PxDefaultCpuDispatcher(IntPtr ptr) : base(ptr, false)
    {
    }

    public static PxDefaultCpuDispatcher Create(uint numThreads)
    {
        return GetOrCreateCache(Native.PxDefaultCpuDispatcher.Create(numThreads), ptr => new PxDefaultCpuDispatcher(ptr));
    }
}
