using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxPvd
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxPvd_New")]
    public static extern IntPtr Create(IntPtr foundation);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxPvd_Connect")]
    public static extern bool Connect(IntPtr pvd, IntPtr transport, PxPvdInstrumentationFlag flags);
}

/// <summary>
/// Types of instrumentation that PVD can do.
/// </summary>
public enum PxPvdInstrumentationFlag : byte
{
    /// <summary>
    /// Send debugging information to PVD.
    /// </summary>
    /// <remarks>
    /// This information is the actual object data of the rigid statics, shapes, articulations, etc.  Sending this
    /// information has a noticeable impact on performance and thus this flag should not be set if you want an accurate
    /// performance profile.
    /// </remarks>
    Debug = 1 << 0,

    /// <sumamry>
    /// Send profile information to PVD.
    /// </summary>
    /// <remarks>
    /// This information populates PVD's profile view.  It has (at this time) negligible cost compared to Debug
    /// information and makes PVD *much* more useful so it is quite highly recommended.
    ///</remarks>
    Profile = 1 << 1,

    /// <summary>
    /// Send memory information to PVD.
    /// </summary>
    /// <remarks>
    /// The PVD sdk side hooks into the Foundation memory controller and listens to allocation/deallocation events. This
    /// has a noticable hit on the first frame, however, this data is somewhat compressed and the PhysX SDK doesn't
    /// allocate much once it hits a steady state.  This information also has a fairly negligible impact and thus is
    /// also highly recommended.
    /// </remarks>
    Memory = 1 << 2,

    All = (Debug | Profile | Memory)
}
