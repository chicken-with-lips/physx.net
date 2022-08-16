namespace ChickenWithLips.PhysX;

public class PxDefaultErrorCallback : PxErrorCallback<PxDefaultAllocator>
{
    public PxDefaultErrorCallback() : base(Native.PxDefaultErrorCallback.Create(), true)
    {
    }
}
