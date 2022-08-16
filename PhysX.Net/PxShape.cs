namespace ChickenWithLips.PhysX;

/// <summary>
/// Material class to represent a set of surface properties.
/// </summary>
public class PxShape : PxBase<PxShape>
{
    private PxShape(IntPtr ptr) : base(ptr)
    {
    }

    public static PxShape Create(PxPhysics physics, PxGeometry geometry, PxMaterial material, bool isExclusive, PxShapeFlag shapeFlags)
    {
        if (geometry is PxBoxGeometry boxGeometry) {
            return GetOrCreateCache(Native.PxPhysics.CreateShape(physics.NativePtr, ref boxGeometry, material.NativePtr, isExclusive, shapeFlags), ptr => new PxShape(ptr));
        } else if (geometry is PxSphereGeometry sphereGeometry) {
            return GetOrCreateCache(Native.PxPhysics.CreateShape(physics.NativePtr, ref sphereGeometry, material.NativePtr, isExclusive, shapeFlags), ptr => new PxShape(ptr));
        }

        throw new Exception("FIXME: errors");
    }
}

/// <summary>
/// Flags which affect the behavior of PxShapes.
/// </summary>
[Flags]
public enum PxShapeFlag : byte
{
    /// <summary>
    /// The shape will partake in collision in the physical simulation.</para>
    /// </summary>
    /// <remarks>
    /// <para>It is illegal to raise the <see cref="SimulationShape"/> and <see cref="TriggerShape"/> flags. In the
    /// event that one of these flags is already raised the sdk will reject any attempt to raise the other. To raise the
    /// <see cref="SimulationShape"/> first ensure that <see cref="SimulationShape"/> is already lowered.</para>
    /// <para>This flag has no effect if simulation is disabled for the corresponding actor
    /// <see cref="PxActorFlag.DisableSimulation"/>.</para>
    /// </remarks>
    SimulationShape = (1 << 0),

    /// <summary>
    /// The shape will partake in scene queries (ray casts, overlap tests, sweeps, ...).
    /// </summary>
    SceneQueryShape = (1 << 1),

    /// <summary>
    /// The shape is a trigger which can send reports whenever other shapes enter/leave its volume.
    /// </summary>
    /// <remarks>
    /// <para>Triangle meshes and height-fields can not be triggers. Shape creation will fail in these cases.</para>
    /// <para>Shapes marked as triggers do not collide with other objects. If an object should act both as a trigger
    /// shape and a collision shape then create a rigid body with two shapes, one being a trigger shape and the other a
    /// collision shape. It is illegal to raise the <see cref="TriggerShape"/> and <see cref="SimulationShape"/> flags
    /// on a single PxShape instance. In the event that one of these flags is already raised the sdk will reject any
    /// attempt to raise the other. To raise the <see cref="TriggerShape"/> flag first ensure that
    /// <see cref="SimulationShape"/> flag is already lowered.</para>
    /// <para>Trigger shapes will no longer send notification events for interactions with other trigger shapes.</para>
    /// <para>Shapes marked as triggers are allowed to participate in scene queries, provided the
    /// <see cref="SceneQueryShape"/> flag is set.</para>
    /// <para>This flag has no effect if simulation is disabled for the corresponding actor
    /// <see cref="PxActorFlag.DisableSimulation"/>.</para>
    /// </remarks>
    TriggerShape = (1 << 2),

    /// <summary>
    /// Enable debug renderer for this shape.
    /// </summary>
    Visualization = (1 << 3)
}
