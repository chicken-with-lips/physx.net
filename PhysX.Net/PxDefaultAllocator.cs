namespace ChickenWithLips.PhysX.Net;

public class PxDefaultAllocator : PxAllocatorCallback<PxDefaultAllocator>
{
    public PxDefaultAllocator() : base(Native.PxDefaultAllocator.Create(), true)
    {
    }
}
