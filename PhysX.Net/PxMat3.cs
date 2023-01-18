using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX;

[StructLayout(LayoutKind.Sequential)]
public readonly struct PxMat33
{
    public readonly float M11;
    public readonly float M12;
    public readonly float M13;
    public readonly float M21;
    public readonly float M22;
    public readonly float M23;
    public readonly float M31;
    public readonly float M32;
    public readonly float M33;
    public readonly float M41;
    public readonly float M42;
    public readonly float M43;
}
