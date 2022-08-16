using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxVersion
{
    [DllImport("PhysX.Native", EntryPoint="physx_PxVersion_VersionConst")]
    public static extern uint Version();
}
