using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxDefaultAllocator
{
    [DllImport("PhysX.Native", EntryPoint="physx_PxDefaultAllocator_New")]
    public static extern IntPtr Create();
}
