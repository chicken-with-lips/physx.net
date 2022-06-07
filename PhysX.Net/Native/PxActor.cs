using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxActor
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxActor_GetName")]
    public static extern string GetName(IntPtr actor);
    
    [DllImport("PhysX.Native", EntryPoint = "physx_PxActor_SetActorFlag")]
    public static extern void SetActorFlag(IntPtr actor, PxActorFlag flag, bool value);
}
