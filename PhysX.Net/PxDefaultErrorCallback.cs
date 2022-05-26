namespace ChickenWithLips.PhysX.Net;

public class PxDefaultErrorCallback : PxErrorCallback<PxDefaultAllocator>
{
    public PxDefaultErrorCallback() : base(Native.PxDefaultErrorCallback.Create(), true)
    {
    }
}
