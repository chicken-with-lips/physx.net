namespace ChickenWithLips.PhysX.Net;

public enum PxPairFilteringMode
{
    /// <summary>
    /// Output pair from BP, potentially send to user callbacks, create regular interaction object.
    /// </summary>
    /// <remarks>
    /// <para>Enable contact pair filtering between kinematic/static or kinematic/kinematic rigid bodies.</para>
    /// <para>By default contacts between these are suppressed (see #PxFilterFlag::eSUPPRESS) and don't get reported to
    /// the filter mechanism.</para>
    /// <para>Use this mode if these pairs should go through the filtering pipeline nonetheless.</para>
    /// <para>This mode is not mutable, and must be set in PxSceneDesc at scene creation.</para>
    /// </remarks>
    Keep,

    /// <summary>Output pair from BP, create interaction marker. Can be later switched to regular interaction.</summary>
    Suppress,

    /// <summary>Don't output pair from BP. Cannot be later switched to regular interaction, needs "resetFiltering" call.</summary>
    Kill,

    /// <summary>Default is eSUPPRESS for compatibility with previous PhysX versions.</summary>
    Default = Suppress,
}
