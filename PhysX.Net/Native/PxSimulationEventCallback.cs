using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxSimulationEventCallback
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxSimulationEventCallback_New")]
    public static extern IntPtr Create(OnContact onContact);

    internal delegate void OnContact(PxContactPairHeaderTransfer pairHeader, PxContactPairTransfer[] pairs, uint pairCount);
}
