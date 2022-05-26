using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net;

/// <summary>
/// Geometry of a box. The geometry of a box can be fully specified by its half extents. This is the half of its width, height, and depth.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PxBoxGeometry : PxGeometry
{
    private readonly PxGeometryType Type = PxGeometryType.Box;
    public readonly Vector3 HalfExtents;

    public PxBoxGeometry(Vector3 halfExtents)
    {
        HalfExtents = halfExtents;
    }
}
