using System.Numerics;

namespace ChickenWithLips.PhysX;

public class PxUtil
{
    public static Vector3 Diagonalize(PxMat33 matrix, out Quaternion axes)
    {
        return Native.PxUtil.Diagonalize(matrix, out axes);
    }
}
