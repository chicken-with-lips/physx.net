using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxUtil
{
    [DllImport("PhysX.Native", EntryPoint="physx_PxUtil_Diagonalize")]
    public static extern Vector3 Diagonalize(PxMat33 matrix, out Quaternion axes);
}
