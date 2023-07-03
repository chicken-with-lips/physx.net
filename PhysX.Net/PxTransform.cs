using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX;

/// <summary>
/// A rigid euclidean transform as a quaternion and a vector.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PxTransform
{
    public readonly Quaternion Quaternion = default;
    public readonly Vector3 Position = default;

    public PxTransform()
    {
        Quaternion = Quaternion.Identity;
        Position = Vector3.Zero;
    }
    
    public PxTransform(Quaternion quaternion, Vector3 position)
    {
        Quaternion = quaternion;
        Position = position;
    }
}
