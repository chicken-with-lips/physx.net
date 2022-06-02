using ChickenWithLips.PhysX.Net.Native;

namespace ChickenWithLips.PhysX.Net;

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
