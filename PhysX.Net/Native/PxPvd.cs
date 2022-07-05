using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxPvd
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxPvd_New")]
    public static extern IntPtr Create(IntPtr foundation);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxPvd_Connect")]
    public static extern bool Connect(IntPtr pvd, IntPtr transport, PxPvdInstrumentationFlag flags);
}
