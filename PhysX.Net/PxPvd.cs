using ChickenWithLips.PhysX.Native;

namespace ChickenWithLips.PhysX;

/// <summary>
/// PxPvd is the top-level class for the PVD framework, and the main customer interface for PVD configuration.
/// </summary>
public class PxPvd : PxBase<PxPvd>
{
    #region Methods

    public PxPvd(PxFoundation foundation)
        : base(Native.PxPvd.Create(foundation.NativePtr), true)
    {
    }

    /// <summary>
    /// Connects the SDK to the PhysX Visual Debugger application.
    /// </summary>
    public bool Connect(PxPvdTransport transport, PxPvdInstrumentationFlag flags)
    {
        return Native.PxPvd.Connect(NativePtr, transport.NativePtr, flags);
    }

    #endregion
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
