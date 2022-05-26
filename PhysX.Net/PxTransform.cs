using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net;

/// <summary>
/// A rigid euclidean transform as a quaternion and a vector.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PxTransform
{
    public readonly Quaternion Quaternion;
    public readonly Vector3 Position;

    public PxTransform(Quaternion quaternion, Vector3 position)
    {
        Quaternion = quaternion;
        Position = position;
    }
}
