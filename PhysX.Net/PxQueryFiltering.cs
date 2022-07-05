namespace ChickenWithLips.PhysX;

[Flags]
public enum PxQueryFlag : ushort
{
    /// <summary>Traverse static shapes.</summary>
    Static = (1 << 0),

    /// <summary>Traverse dynamic shapes.</summary>
    Dynamic = (1 << 1),

    /// <summary>Run the pre-intersection-test filter (see #PxQueryFilterCallback::preFilter()).</summary>
    Prefilter = (1 << 2),

    /// <summary>Run the post-intersection-test filter (see #PxQueryFilterCallback::postFilter()).</summary>
    PostFilter = (1 << 3),

    /// <summary>Abort traversal as soon as any hit is found and return it via callback.block.</summary>
    AnyHit = (1 << 4),

    /// <summary>All hits are reported as touching. Overrides eBLOCK returned from user filters with eTOUCH.</summary>
    NoBlock = (1 << 5),

    /// <summary>Reserved for internal use.</summary>
    Reserved = (1 << 15)
}
