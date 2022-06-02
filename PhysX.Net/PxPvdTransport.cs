namespace ChickenWithLips.PhysX.Net;

/// <summary>
/// PxPvdTransport is an interface representing the data transport mechanism.
/// </summary>
/// <remarks>
/// <para>This class defines all services associated with the transport: configuration, connection, reading, writing
/// etc. It is owned by the application, and can be realized as a file or a socket (using one-line PxDefault<...>
/// methods in PhysXExtensions) or in a custom implementation.</para>
/// <para>This is a class that is intended for use by PVD, not by the application, the application entry points are
/// PxPvd and PvdClient.</para>
/// </remarks>
public class PxPvdTransport : PxBase<PxPvdTransport>
{
    #region Methods

    private PxPvdTransport(IntPtr ptr) : base(ptr)
    {
    }

    public static PxPvdTransport CreateDefaultSocketTransport(string host, int port, uint timeoutInMilliseconds)
    {
        return GetOrCreateCache(Native.PxPvdTransport.CreateSocketTransferTransport(host, port, timeoutInMilliseconds), ptr => new PxPvdTransport(ptr));
    }

    #endregion
}
