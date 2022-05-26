namespace ChickenWithLips.PhysX.Net;

public enum PxForceMode
{
    /// <summary>Parameter has unit of mass * distance / time^2, i.e. a force.</summary>
    Force,

    /// <summary>Parameter has unit of mass * distance / time.</summary>
    Impulse,

    /// <summary>Parameter has unit of distance / time, i.e. the effect is mass independent: a velocity change.</summary>
    VelocityChange,

    /// <summary>Parameter has unit of distance/ time^2, i.e. an acceleration. It gets treated just like a force except the mass is not divided out before integration.</summary>
    Acceleration,
}
