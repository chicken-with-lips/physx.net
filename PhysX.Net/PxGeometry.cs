using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX;

public interface PxGeometry
{
}

/// <summary>
/// A geometry type.
/// </summary>
public enum PxGeometryType
{
    Sphere,
    Plane,
    Capsule,
    Box,
    ConvexMesh,
    TriangleMesh,
    HeightField,

    /// <summary>Internal use only!</summary>
    GeometryCount,

    /// <summary>Internal use only!</summary>
    Invalid = -1
}

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

/// <summary>
/// A class representing the geometry of a sphere. Spheres are defined by their radius.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PxSphereGeometry : PxGeometry
{
    private readonly PxGeometryType Type = PxGeometryType.Sphere;
    public readonly float Radius;

    public PxSphereGeometry(float radius)
    {
        Radius = radius;
    }
}
