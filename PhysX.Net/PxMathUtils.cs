using System.Numerics;

namespace ChickenWithLips.PhysX;

public class PxMathUtils
{
    public static Vector3 Diagonalize(PxMat33 matrix, out Quaternion axes)
    {
        return Native.PxMathUtils.Diagonalize(matrix, out axes);
    }
}
