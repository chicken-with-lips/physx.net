using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxCpuDispatcher
{
    [DllImport("PhysX.Native", EntryPoint="physx_PxCpuDispatcher_getWorkerCount")]
    public static extern uint GetWorkerCount(IntPtr cpuDispatcher);
}
