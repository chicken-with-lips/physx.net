namespace ChickenWithLips.PhysX;

public class PxVersion
{
    public static uint Version => Native.PxVersion.Version();
}
