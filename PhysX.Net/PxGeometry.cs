namespace ChickenWithLips.PhysX.Net;

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
