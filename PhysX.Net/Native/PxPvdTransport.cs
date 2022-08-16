using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxPvdTransport
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxPvdTransport_NewSocketTransferTransport")]
    public static extern IntPtr CreateSocketTransferTransport(string host, int port, uint timeoutInMilliseconds);
}
