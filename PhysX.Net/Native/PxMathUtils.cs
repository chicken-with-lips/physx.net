using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxMathUtils
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxMathUtils_PxDiagonalize")]
    public static extern Vector3 Diagonalize(PxMat33 m, out Quaternion massFrame);
}
