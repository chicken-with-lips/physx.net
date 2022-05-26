namespace ChickenWithLips.PhysX.Net;

public enum PxBroadPhaseType
{
    /// <summary>3-axes sweep-and-prune</summary>
    Sap,

    /// <summary>Multi box pruning</summary>
    Mbp,

    /// <summary>Automatic box pruning</summary>
    Abp,
    Gpu,
}
