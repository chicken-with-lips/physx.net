namespace ChickenWithLips.PhysX;

public class PxFoundation : PxBase<PxFoundation>
{
    private PxFoundation(IntPtr ptr) : base(ptr)
    {
    }

    public static PxFoundation Create(uint version)
    {
        return GetOrCreateCache(Native.PxFoundation.Create(version), ptr => new PxFoundation(ptr));
    }
}
