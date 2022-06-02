using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxSimulationFilterCallback
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxSimulationFilterCallback_New")]
    public static extern IntPtr Create(OnStatusChange onStatusChange);

    public delegate bool OnStatusChange(ref uint pairId, ref PxPairFlag pairFlags, ref PxFilterFlag filterFlags);
}
