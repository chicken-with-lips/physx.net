namespace ChickenWithLips.PhysX.Net;

public class PxVersion
{
    public static uint Version => Native.PxVersion.Version();
}
