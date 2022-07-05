using System.Numerics;
using System.Runtime.InteropServices;
using ChickenWithLips.PhysX.Native;

namespace ChickenWithLips.PhysX;

public class PxScene : PxBase<PxScene>
{
    #region Properties

    /// <summary>
    /// Gets or sets the gravity for the scene.
    /// </summary>
    public Vector3 Gravity {
        get => Native.PxScene.GetGravity(NativePtr);
        set => Native.PxScene.SetGravity(NativePtr, value);
    }

    #endregion

    #region Methods

    internal PxScene(IntPtr ptr) : base(ptr)
    {
    }

    /// <summary>
    /// Advances the simulation by an elapsedTime time.
    /// </summary>
    /// <remarks>
    /// <para>Large elapsedTime values can lead to instabilities. In such cases elapsedTime should be subdivided into
    /// smaller time intervals and simulate() should be called multiple times for each interval.</para>
    /// <para>Calls to simulate() should pair with calls to fetchResults():</para>
    /// <para>Each fetchResults() invocation corresponds to exactly one simulate() invocation; calling simulate() twice
    /// without an intervening fetchResults() or fetchResults() twice without an intervening simulate() causes an error
    /// condition.</para>
    /// </remarks>
    /// <param name="elapsedTime">Amount of time to advance simulation by. The parameter has to be larger than 0, else the resulting behavior will be undefined.</param>
    public void Simulate(float elapsedTime)
    {
        Native.PxScene.Simulate(NativePtr, elapsedTime);
    }

    public bool FetchResults(bool block = false)
    {
        return FetchResults(block, out var discard);
    }

    public bool FetchResults(bool block, out uint errorState)
    {
        errorState = 0;

        return Native.PxScene.FetchResults(NativePtr, block, ref errorState);
    }

    /// <summary>
    /// Adds an actor to this scene.
    /// </summary>
    /// <remarks>
    /// <para>If the actor is already assigned to a scene (see #PxActor::getScene), the call is ignored and an error
    /// is issued.</para>
    /// <para>If the actor has an invalid constraint, in checked builds the call is ignored and an error is issued.</para>
    /// <para>You can not add individual articulation links (see #PxArticulationLink) to the scene.
    /// Use #addArticulation() instead.</para>
    /// <para>If the actor is a PxRigidActor then each assigned PxConstraint object will get added to the scene
    /// automatically if it connects to another actor that is part of the scene already.</para>
    /// </remarks>
    /// <param name="actor">Actor to add to scene.</param>
    public void AddActor(PxActor actor)
    {
        Native.PxScene.AddActor(NativePtr, actor.NativePtr);
    }

    /// <summary>
    /// Removes an actor from this scene.
    /// </summary>
    /// <remarks>
    /// <para>You can not remove individual articulation links (see #PxArticulationLink) from the scene.
    /// Use #removeArticulation() instead.</para>
    /// <para>If the actor is a PxRigidActor then all assigned PxConstraint objects will get removed from the scene
    /// automatically.</para>
    /// <para>If the actor is in an aggregate it will be removed from the aggregate.</para>
    /// </remarks>
    /// <param name="actor">Actor to remove from scene.</param>
    /// <param name="wakeOnLostTouch">Specifies whether touching objects from the previous frame should get woken up in
    /// the next frame. Only applies to PxArticulation and PxRigidActor types.</param>
    public void RemoveActor(PxActor actor, bool wakeOnLostTouch = true)
    {
        Native.PxScene.RemoveActor(NativePtr, actor.NativePtr, wakeOnLostTouch);
    }

    public bool Overlap(PxGeometry geometry, PxTransform pose, PxQueryFlag filterData = PxQueryFlag.Dynamic | PxQueryFlag.Static)
    {
        if (geometry is PxBoxGeometry box) {
            return Native.PxScene.Overlap(NativePtr, ref box, ref pose, filterData);
        } else if (geometry is PxSphereGeometry sphere) {
            return Native.PxScene.Overlap(NativePtr, ref sphere, ref pose, filterData);
        }

        throw new ApplicationException("TODO: errors");
    }

    public static PxScene Create(PxPhysics physics, PxSceneDesc sceneDesc)
    {
        var transfer = new PxSceneDescTransfer(sceneDesc);
        return GetOrCreateCache(Native.PxPhysics.CreateScene(physics.NativePtr, ref transfer), ptr => new PxScene(ptr));
    }

    #endregion
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct PxTolerancesScale
{
    public readonly float Length;
    public readonly float Speed;
}

[StructLayout(LayoutKind.Sequential)]
public struct PxSceneDesc
{
    /// <summary>
    /// Gravity vector.
    /// </summary>
    /// <remarks>When setting gravity, you should probably also set bounce threshold.</remarks>
    public Vector3 Gravity;

    /// <summary>
    /// Possible notification callback.
    /// </summary>
    public PxSimulationEventCallback SimulationEventCallback;

    /// <summary>
    /// Possible asynchronous callback for contact modification.
    /// </summary>
    public IntPtr ContactModifyCallback;

    /// <summary>
    /// Possible asynchronous callback for contact modification.
    /// </summary>
    public IntPtr CcdContactModifyCallback;

    /// <summary>
    /// Shared global filter data which will get passed into the filter shader.
    /// </summary>
    /// <remarks>
    /// The provided data will get copied to internal buffers and this copy will be used for filtering calls.
    /// </remarks>
    public IntPtr FilterShaderData;

    /// <summary>
    /// Size (in bytes) of the shared global filter data #filterShaderData.
    /// </summary>
    public uint FilterShaderDataSize;

    /// <summary>
    /// The custom filter shader to use for collision filtering.
    /// </summary>
    /// <remarks>
    /// This parameter is compulsory. If you don't want to define your own filter shader you can use the default shader
    /// #PxDefaultSimulationFilterShader which can be found in the PhysX extensions library.
    /// </remarks>
    public PxFilterShaderCallback FilterShader;

    /// <summary>
    /// A custom collision filter callback which can be used to implement more complex filtering operations which need
    /// access to the simulation state, for example.
    /// </summary>
    public PxSimulationFilterCallback FilterCallback;

    /// <summary>
    /// Filtering mode for kinematic-kinematic pairs in the broadphase.
    /// </summary>
    public PxPairFilteringMode KineKineFilteringMode;

    /// <summary>
    /// Filtering mode for static-kinematic pairs in the broadphase.
    /// </summary>
    public PxPairFilteringMode StaticKineFilteringMode;

    /// <summary>
    /// Selects the broad-phase algorithm to use.
    /// </summary>
    public PxBroadPhaseType BroadPhaseType;

    /// <summary>
    /// Broad-phase callback
    /// </summary>
    public IntPtr BroadPhaseCallback;

    /// <summary>
    /// Expected scene limits.
    /// </summary>
    public PxSceneLimits Limits;

    /// <summary>
    /// Selects the friction algorithm to use for simulation.
    /// </summary>
    /// <remarks>
    /// Cannot be modified after the first call to any of PxScene::simulate, PxScene::solve and PxScene::collide.
    /// </remarks>
    public PxFrictionType FrictionType;

    /// <summary>
    /// Selects the solver algorithm to use.
    /// </summary>
    public PxSolverType SolverType;

//
// 	/**
// 	\brief A contact with a relative velocity below this will not bounce. A typical value for simulation.
// 	stability is about 0.2 * gravity.
//
// 	<b>Range:</b> [0, PX_MAX_F32)<br>
// 	<b>Default:</b> 0.2 * PxTolerancesScale::speed
//
// 	@see PxMaterial PxScene.setBounceThresholdVelocity() PxScene.getBounceThresholdVelocity()
// 	*/
    public float BounceThresholdVelocity;

//
// 	/**
// 	\brief A threshold of contact separation distance used to decide if a contact point will experience friction forces.
//
// 	\note If the separation distance of a contact point is greater than the threshold then the contact point will not experience friction forces. 
//
// 	\note If the aggregated contact offset of a pair of shapes is large it might be desirable to neglect friction
// 	for contact points whose separation distance is sufficiently large that the shape surfaces are clearly separated. 
// 	
// 	\note This parameter can be used to tune the separation distance of contact points at which friction starts to have an effect.  
//
// 	<b>Range:</b> [0, PX_MAX_F32)<br>
// 	<b>Default:</b> 0.04 * PxTolerancesScale::length
// 	*/
    public float FrictionOffsetThreshold;

//
// 	/**
// 	\brief A threshold for speculative CCD. Used to control whether bias, restitution or a combination of the two are used to resolve the contacts.
//
// 	\note This only has any effect on contacting pairs where one of the bodies has PxRigidBodyFlag::eENABLE_SPECULATIVE_CCD raised.
//
// 	<b>Range:</b> [0, PX_MAX_F32)<br>
// 	<b>Default:</b> 0.04 * PxTolerancesScale::length
// 	*/
//
    public float CcdMaxSeparation;

//
// 	/**
// 	\brief A slop value used to zero contact offsets from the body's COM on an axis if the offset along that axis is smaller than this threshold. Can be used to compensate
// 	for small numerical errors in contact generation.
//
// 	<b>Range:</b> [0, PX_MAX_F32)<br>
// 	<b>Default:</b> 0.0
// 	*/
//
    public float SolverOffsetSlop;

//
// 	/**
// 	\brief Flags used to select scene options.
//
// 	@see PxSceneFlag PxSceneFlags
// 	*/
    public PxSceneFlag Flags;

//
// 	/**
// 	\brief The CPU task dispatcher for the scene.
//
// 	See PxCpuDispatcher, PxScene::getCpuDispatcher
// 	*/
    public PxCpuDispatcher CpuDispatcher;

//
// 	/**
// 	\brief The CUDA context manager for the scene.
//
// 	<b>Platform specific:</b> Applies to PC GPU only.
//
// 	See PxCudaContextManager, PxScene::getCudaContextManager
// 	*/
    public IntPtr CudaContextManager;

//
// 	/**
// 	\brief Defines the structure used to store static objects.
//
// 	\note Only PxPruningStructureType::eSTATIC_AABB_TREE and PxPruningStructureType::eDYNAMIC_AABB_TREE are allowed here.
// 	*/
    public PxPruningStructureType StaticStructure;

//
// 	/**
// 	\brief Defines the structure used to store dynamic objects.
// 	*/
    public PxPruningStructureType DynamicStructure;

//
// 	/**
// 	\brief Hint for how much work should be done per simulation frame to rebuild the pruning structure.
//
// 	This parameter gives a hint on the distribution of the workload for rebuilding the dynamic AABB tree
// 	pruning structure #PxPruningStructureType::eDYNAMIC_AABB_TREE. It specifies the desired number of simulation frames
// 	the rebuild process should take. Higher values will decrease the workload per frame but the pruning
// 	structure will get more and more outdated the longer the rebuild takes (which can make
// 	scene queries less efficient).
//
// 	\note Only used for #PxPruningStructureType::eDYNAMIC_AABB_TREE pruning structure.
//
// 	\note This parameter gives only a hint. The rebuild process might still take more or less time depending on the
// 	number of objects involved.
//
// 	<b>Range:</b> [4, PX_MAX_U32)<br>
// 	<b>Default:</b> 100
// 	*/
    public uint DynamicTreeRebuildRateHint;

//
// 	/**
// 	\brief Defines the scene query update mode.
// 	<b>Default:</b> PxSceneQueryUpdateMode::eBUILD_ENABLED_COMMIT_ENABLED
// 	*/
    public PxSceneQueryUpdateMode SceneQueryUpdateMode;

//
// 	/**
// 	\brief Will be copied to PxScene::userData.
//
// 	<b>Default:</b> NULL
// 	*/
    public IntPtr UserData;

//
// 	/**
// 	\brief Defines the number of actors required to spawn a separate rigid body solver island task chain.
//
// 	This parameter defines the minimum number of actors required to spawn a separate rigid body solver task chain. Setting a low value 
// 	will potentially cause more task chains to be generated. This may result in the overhead of spawning tasks can become a limiting performance factor. 
// 	Setting a high value will potentially cause fewer islands to be generated. This may reduce thread scaling (fewer task chains spawned) and may 
// 	detrimentally affect performance if some bodies in the scene have large solver iteration counts because all constraints in a given island are solved by the 
// 	maximum number of solver iterations requested by any body in the island.
//
// 	Note that a rigid body solver task chain is spawned as soon as either a sufficient number of rigid bodies or articulations are batched together.
//
// 	<b>Default:</b> 128
//
// 	@see PxScene.setSolverBatchSize() PxScene.getSolverBatchSize()
// 	*/
    public uint SolverBatchSize;

//
// 	/**
// 	\brief Defines the number of articulations required to spawn a separate rigid body solver island task chain.
//
// 	This parameter defines the minimum number of articulations required to spawn a separate rigid body solver task chain. Setting a low value
// 	will potentially cause more task chains to be generated. This may result in the overhead of spawning tasks can become a limiting performance factor.
// 	Setting a high value will potentially cause fewer islands to be generated. This may reduce thread scaling (fewer task chains spawned) and may
// 	detrimentally affect performance if some bodies in the scene have large solver iteration counts because all constraints in a given island are solved by the
// 	maximum number of solver iterations requested by any body in the island.
//
// 	Note that a rigid body solver task chain is spawned as soon as either a sufficient number of rigid bodies or articulations are batched together. 
//
// 	<b>Default:</b> 128
//
// 	@see PxScene.setSolverArticulationBatchSize() PxScene.getSolverArticulationBatchSize()
// 	*/
    public uint SolverArticulationBatchSize;

//
// 	/**
// 	\brief Setting to define the number of 16K blocks that will be initially reserved to store contact, friction, and contact cache data.
// 	This is the number of 16K memory blocks that will be automatically allocated from the user allocator when the scene is instantiated. Further 16k
// 	memory blocks may be allocated during the simulation up to maxNbContactDataBlocks.
//
// 	\note This value cannot be larger than maxNbContactDataBlocks because that defines the maximum number of 16k blocks that can be allocated by the SDK.
//
// 	<b>Default:</b> 0
//
// 	<b>Range:</b> [0, PX_MAX_U32]<br>
//
// 	@see PxPhysics::createScene PxScene::setNbContactDataBlocks 
// 	*/
    public uint NbContactDataBlocks;

//
// 	/**
// 	\brief Setting to define the maximum number of 16K blocks that can be allocated to store contact, friction, and contact cache data.
// 	As the complexity of a scene increases, the SDK may require to allocate new 16k blocks in addition to the blocks it has already 
// 	allocated. This variable controls the maximum number of blocks that the SDK can allocate.
//
// 	In the case that the scene is sufficiently complex that all the permitted 16K blocks are used, contacts will be dropped and 
// 	a warning passed to the error stream.
//
// 	If a warning is reported to the error stream to indicate the number of 16K blocks is insufficient for the scene complexity 
// 	then the choices are either (i) re-tune the number of 16K data blocks until a number is found that is sufficient for the scene complexity,
// 	(ii) to simplify the scene or (iii) to opt to not increase the memory requirements of physx and accept some dropped contacts.
// 	
// 	<b>Default:</b> 65536
//
// 	<b>Range:</b> [0, PX_MAX_U32]<br>
//
// 	@see nbContactDataBlocks PxScene::setNbContactDataBlocks 
// 	*/
    public uint MaxNbContactDataBlocks;

//
// 	/**
// 	\brief The maximum bias coefficient used in the constraint solver
//
// 	When geometric errors are found in the constraint solver, either as a result of shapes penetrating
// 	or joints becoming separated or violating limits, a bias is introduced in the solver position iterations
// 	to correct these errors. This bias is proportional to 1/dt, meaning that the bias becomes increasingly 
// 	strong as the time-step passed to PxScene::simulate(...) becomes smaller. This coefficient allows the
// 	application to restrict how large the bias coefficient is, to reduce how violent error corrections are.
// 	This can improve simulation quality in cases where either variable time-steps or extremely small time-steps
// 	are used.
// 	
// 	<b>Default:</b> PX_MAX_F32
//
// 	<b> Range</b> [0, PX_MAX_F32] <br>
//
// 	*/
    public float MaxBiasCoefficient;

//
// 	/**
// 	\brief Size of the contact report stream (in bytes).
// 	
// 	The contact report stream buffer is used during the simulation to store all the contact reports. 
// 	If the size is not sufficient, the buffer will grow by a factor of two.
// 	It is possible to disable the buffer growth by setting the flag PxSceneFlag::eDISABLE_CONTACT_REPORT_BUFFER_RESIZE.
// 	In that case the buffer will not grow but contact reports not stored in the buffer will not get sent in the contact report callbacks.
//
// 	<b>Default:</b> 8192
//
// 	<b>Range:</b> (0, PX_MAX_U32]<br>
// 	
// 	*/
    public uint ContactReportStreamBufferSize;

//
// 	/**
// 	\brief Maximum number of CCD passes 
//
// 	The CCD performs multiple passes, where each pass every object advances to its time of first impact. This value defines how many passes the CCD system should perform.
// 	
// 	\note The CCD system is a multi-pass best-effort conservative advancement approach. After the defined number of passes has been completed, any remaining time is dropped.
// 	\note This defines the maximum number of passes the CCD can perform. It may perform fewer if additional passes are not necessary.
//
// 	<b>Default:</b> 1
// 	<b>Range:</b> [1, PX_MAX_U32]<br>
// 	*/
    public uint CcdMaxPasses;

//
// 	/**
// 	\brief CCD threshold
//
// 	CCD performs sweeps against shapes if and only if the relative motion of the shapes is fast-enough that a collision would be missed
// 	by the discrete contact generation. However, in some circumstances, e.g. when the environment is constructed from large convex shapes, this 
// 	approach may produce undesired simulation artefacts. This parameter defines the minimum relative motion that would be required to force CCD between shapes.
// 	The smaller of this value and the sum of the thresholds calculated for the shapes involved will be used.
//
// 	\note It is not advisable to set this to a very small value as this may lead to CCD "jamming" and detrimentally effect performance. This value should be at least larger than the translation caused by a single frame's gravitational effect
//
// 	<b>Default:</b> PX_MAX_F32
// 	<b>Range:</b> [Eps, PX_MAX_F32]<br>
// 	*/
//
    public float CcdThreshold;

//
// 	/**
// 	\brief The wake counter reset value
//
// 	Calling wakeUp() on objects which support sleeping will set their wake counter value to the specified reset value.
//
// 	<b>Range:</b> (0, PX_MAX_F32)<br>
// 	<b>Default:</b> 0.4 (which corresponds to 20 frames for a time step of 0.02)
//
// 	@see PxRigidDynamic::wakeUp() PxArticulationBase::wakeUp()
// 	*/
    public float WakeCounterResetValue;

//
// 	/**
// 	\brief The bounds used to sanity check user-set positions of actors and articulation links
//
// 	These bounds are used to check the position values of rigid actors inserted into the scene, and positions set for rigid actors
// 	already within the scene.
//
// 	<b>Range:</b> any valid PxBounds3 <br> 
// 	<b>Default:</b> (-PX_MAX_BOUNDS_EXTENTS, PX_MAX_BOUNDS_EXTENTS) on each axis
// 	*/
    public PxBounds3 SanityBounds;

//
// 	/**
// 	\brief The pre-allocations performed in the GPU dynamics pipeline.
// 	*/
    public PxgDynamicsMemoryConfig GpuDynamicsConfig;

//
// 	/**
// 	\brief Limitation for the partitions in the GPU dynamics pipeline.
// 	This variable must be power of 2.
// 	A value greater than 32 is currently not supported.
// 	<b>Range:</b> (1, 32)<br>
// 	*/
    public uint GpuMaxNumPartitions;

//
// 	/**
// 	\brief Defines which compute version the GPU dynamics should target. DO NOT MODIFY
// 	*/
    public uint GpuComputeVersion;

//
// private:
// 	/**
// 	\cond
// 	*/
// 	// For internal use only
    public readonly PxTolerancesScale TolerancesScale;

// 	/**
// 	\endcond
// 	*/
//
//
// public:
// 	/**
// 	\brief constructor sets to default.
//
// 	\param[in] scale scale values for the tolerances in the scene, these must be the same values passed into
// 	PxCreatePhysics(). The affected tolerances are bounceThresholdVelocity and frictionOffsetThreshold.
//
// 	@see PxCreatePhysics() PxTolerancesScale bounceThresholdVelocity frictionOffsetThreshold
// 	*/	
// 	PX_INLINE PxSceneDesc(const PxTolerancesScale& scale);
//
// 	/**
// 	\brief (re)sets the structure to the default.
//
// 	\param[in] scale scale values for the tolerances in the scene, these must be the same values passed into
// 	PxCreatePhysics(). The affected tolerances are bounceThresholdVelocity and frictionOffsetThreshold.
//
// 	@see PxCreatePhysics() PxTolerancesScale bounceThresholdVelocity frictionOffsetThreshold
// 	*/
// 	PX_INLINE void setToDefault(const PxTolerancesScale& scale);
//
// 	/**
// 	\brief Returns true if the descriptor is valid.
// 	\return true if the current settings are valid.
// 	*/
// 	PX_INLINE bool isValid() const;
//
// 	/**
// 	\cond
// 	*/
// 	// For internal use only
// 	PX_INLINE const PxTolerancesScale& getTolerancesScale() const { return tolerancesScale; }
// 	/**
// 	\endcond
// 	*/
// };
//*
    public PxSceneDesc(PxTolerancesScale scale)
    {
        Gravity = Vector3.Zero;
        SimulationEventCallback = null;
        ContactModifyCallback = IntPtr.Zero;
        CcdContactModifyCallback = IntPtr.Zero;

        FilterShaderData = IntPtr.Zero;
        FilterShaderDataSize = 0;
        FilterShader = null;
        FilterCallback = null;

        KineKineFilteringMode = PxPairFilteringMode.Default;
        StaticKineFilteringMode = PxPairFilteringMode.Default;

        BroadPhaseType = PxBroadPhaseType.Abp;
        BroadPhaseCallback = IntPtr.Zero;

        FrictionType = PxFrictionType.Patch;
        SolverType = PxSolverType.Pgs;
        BounceThresholdVelocity = 0.2f * scale.Speed;
        FrictionOffsetThreshold = 0.04f * scale.Length;
        CcdMaxSeparation = 0.04f * scale.Length;
        SolverOffsetSlop = 0.0f;

        Flags = PxSceneFlag.EnablePcm;

        CpuDispatcher = null;
        CudaContextManager = IntPtr.Zero;

        StaticStructure = PxPruningStructureType.DynamicAabbTree;
        DynamicStructure = PxPruningStructureType.DynamicAabbTree;
        DynamicTreeRebuildRateHint = 100;
        SceneQueryUpdateMode = PxSceneQueryUpdateMode.BuildEnabledCommitEnabled;

        UserData = IntPtr.Zero;

        SolverBatchSize = 128;
        SolverArticulationBatchSize = 16;

        NbContactDataBlocks = 0;
        MaxNbContactDataBlocks = 1 << 16;
        MaxBiasCoefficient = float.MaxValue;
        ContactReportStreamBufferSize = 8192;
        CcdMaxPasses = 1;
        CcdThreshold = float.MaxValue;
        WakeCounterResetValue = 20.0f * 0.02f;
        SanityBounds = PxBounds3.Default;
        GpuMaxNumPartitions = 8;
        GpuComputeVersion = 0;
        TolerancesScale = scale;

        Limits = new PxSceneLimits();
        GpuDynamicsConfig = new PxgDynamicsMemoryConfig();
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct PxSceneLimits
{
    /// <summary>Expected maximum number of actors.</summary>
    public uint MaxNbActors;

    /// <summary>Expected maximum number of dynamic rigid bodies.</summary>
    public uint MaxNbBodies;

    /// <summary>Expected maximum number of static shapes.</summary>
    public uint MaxNbStaticShapes;

    /// <summary>Expected maximum number of dynamic shapes.</summary>
    public uint MaxNbDynamicShapes;

    /// <summary>Expected maximum number of aggregates.</summary>
    public uint MaxNbAggregates;

    /// <summary>Expected maximum number of constraint shaders.</summary>
    public uint MaxNbConstraints;

    /// <summary>Expected maximum number of broad-phase regions.</summary>
    public uint MaxNbRegions;

    /// <summary>Expected maximum number of broad-phase overlaps.</summary>
    public uint MaxNbBroadPhaseOverlaps;
}

/// <summary>Enum for selecting the friction algorithm used for simulation.</summary>
/// <remarks>
/// <para>#PxFrictionType::ePATCH selects the patch friction model which typically leads to the most stable results at
/// low solver iteration counts and is also quite inexpensive, as it uses only up to four scalar solver constraints per
/// pair of touching objects.  The patch friction model is the same basic strong friction algorithm as PhysX 3.2 and
/// before.</para>
/// <para>#PxFrictionType::eONE_DIRECTIONAL is a simplification of the Coulomb friction model, in which the friction for
/// a given point of contact is applied in the alternating tangent directions of the contact's normal.  This
/// simplification allows us to reduce the number of iterations required for convergence but is not as accurate as the
/// two directional model.</para>
/// <para>#PxFrictionType::eTWO_DIRECTIONAL is identical to the one directional model, but it applies friction in both
/// tangent directions simultaneously.  This hurts convergence a bit so it requires more solver iterations, but is more
/// accurate.  Like the one directional model, it is applied at every contact point, which makes it potentially more
/// expensive than patch friction for scenarios with many contact points.</para>
/// <para>#PxFrictionType::eFRICTION_COUNT is the total numer of friction models supported by the SDK.</para>
/// </remarks>
public enum PxFrictionType
{
    /// <summary>Select default patch-friction model.</summary>
    Patch,

    /// <summary>Select one directional per-contact friction model.</summary>
    OneDirectional,

    /// <summary>Select two directional per-contact friction model.</summary>
    TwoDirectional,

    /// <summary>The total number of friction models supported by the SDK.</summary>
    FrictionCount
}

/// <summary>Enum for selecting the type of solver used for the simulation.</summary>
/// <remarks>
/// <para>#PxSolverType::ePGS selects the default iterative sequential impulse solver. This is the same kind of solver used in PhysX 3.4 and earlier releases.</para>
/// <para>
/// #PxSolverType::eTGS selects a non linear iterative solver. This kind of solver can lead to improved convergence and
/// handle large mass ratios, long chains and jointed systems better. It is slightly more expensive than the default
/// solver and can introduce more energy to correct joint and contact errors.
/// </para>
/// </remarks>
public enum PxSolverType
{
    /// <summary>Default Projected Gauss-Seidel iterative solver.</summary>
    Pgs,

    /// <summary>Temporal Gauss-Seidel solver.</summary>
    Tgs
}

[Flags]
public enum PxSceneFlag
{
    /**
		\brief Enable Active Actors Notification.

		This flag enables the Active Actor Notification feature for a scene.  This
		feature defaults to disabled.  When disabled, the function
		PxScene::getActiveActors() will always return a NULL list.

		\note There may be a performance penalty for enabling the Active Actor Notification, hence this flag should
		only be enabled if the application intends to use the feature.

		<b>Default:</b> False
		*/
    EnableActiveActors = (1 << 0),

    /**
		\brief Enables a second broad phase check after integration that makes it possible to prevent objects from tunneling through eachother.

		PxPairFlag::eDETECT_CCD_CONTACT requires this flag to be specified.

		\note For this feature to be effective for bodies that can move at a significant velocity, the user should raise the flag PxRigidBodyFlag::eENABLE_CCD for them.
		\note This flag is not mutable, and must be set in PxSceneDesc at scene creation.

		<b>Default:</b> False

		@see PxRigidBodyFlag::eENABLE_CCD, PxPairFlag::eDETECT_CCD_CONTACT, eDISABLE_CCD_RESWEEP
		*/
    EnableCcd = (1 << 1),

    /**
		\brief Enables a simplified swept integration strategy, which sacrifices some accuracy for improved performance.

		This simplified swept integration approach makes certain assumptions about the motion of objects that are not made when using a full swept integration. 
		These assumptions usually hold but there are cases where they could result in incorrect behavior between a set of fast-moving rigid bodies. A key issue is that
		fast-moving dynamic objects may tunnel through each-other after a rebound. This will not happen if this mode is disabled. However, this approach will be potentially 
		faster than a full swept integration because it will perform significantly fewer sweeps in non-trivial scenes involving many fast-moving objects. This approach 
		should successfully resist objects passing through the static environment.

		PxPairFlag::eDETECT_CCD_CONTACT requires this flag to be specified.

		\note This scene flag requires eENABLE_CCD to be enabled as well. If it is not, this scene flag will do nothing.
		\note For this feature to be effective for bodies that can move at a significant velocity, the user should raise the flag PxRigidBodyFlag::eENABLE_CCD for them.
		\note This flag is not mutable, and must be set in PxSceneDesc at scene creation.

		<b>Default:</b> False

		@see PxRigidBodyFlag::eENABLE_CCD, PxPairFlag::eDETECT_CCD_CONTACT, eENABLE_CCD
		*/
    DisableCcdResweep = (1 << 2),

    /**
		\brief Enable adaptive forces to accelerate convergence of the solver. 
		
		\note This flag is not mutable, and must be set in PxSceneDesc at scene creation.

		<b>Default:</b> false
		*/
    AdaptiveForce = (1 << 3),

    /**
		\brief Enable GJK-based distance collision detection system.
		
		\note This flag is not mutable, and must be set in PxSceneDesc at scene creation.

		<b>Default:</b> true
		*/
    EnablePcm = (1 << 6),

    /**
		\brief Disable contact report buffer resize. Once the contact buffer is full, the rest of the contact reports will 
		not be buffered and sent.

		\note This flag is not mutable, and must be set in PxSceneDesc at scene creation.
		
		<b>Default:</b> false
		*/
    DisableContactReportBufferResize = (1 << 7),

    /**
		\brief Disable contact cache.
		
		Contact caches are used internally to provide faster contact generation. You can disable all contact caches
		if memory usage for this feature becomes too high.

		\note This flag is not mutable, and must be set in PxSceneDesc at scene creation.

		<b>Default:</b> false
		*/
    DisableContactCache = (1 << 8),

    /**
		\brief Require scene-level locking

		When set to true this requires that threads accessing the PxScene use the
		multi-threaded lock methods.
		
		\note This flag is not mutable, and must be set in PxSceneDesc at scene creation.

		@see PxScene::lockRead
		@see PxScene::unlockRead
		@see PxScene::lockWrite
		@see PxScene::unlockWrite
		
		<b>Default:</b> false
		*/
    RequireRwLock = (1 << 9),

    /**
		\brief Enables additional stabilization pass in solver

		When set to true, this enables additional stabilization processing to improve that stability of complex interactions between large numbers of bodies.

		Note that this flag is not mutable and must be set in PxSceneDesc at scene creation. Also, this is an experimental feature which does result in some loss of momentum.
		*/
    EnableStabilization = (1 << 10),

    /**
		\brief Enables average points in contact manifolds

		When set to true, this enables additional contacts to be generated per manifold to represent the average point in a manifold. This can stabilize stacking when only a small
		number of solver iterations is used.

		Note that this flag is not mutable and must be set in PxSceneDesc at scene creation.
		*/
    EnableAveragePoint = (1 << 11),

    /**
		\brief Do not report kinematics in list of active actors.

		Since the target pose for kinematics is set by the user, an application can track the activity state directly and use
		this flag to avoid that kinematics get added to the list of active actors.

		\note This flag has only an effect in combination with eENABLE_ACTIVE_ACTORS.

		@see eENABLE_ACTIVE_ACTORS

		<b>Default:</b> false
		*/
    ExcludeKinematicsFromActiveActors = (1 << 12),

    /*\brief Enables the GPU dynamics pipeline

    When set to true, a CUDA ARCH 3.0 or above-enabled NVIDIA GPU is present and the CUDA context manager has been configured, this will run the GPU dynamics pipelin instead of the CPU dynamics pipeline.

    Note that this flag is not mutable and must be set in PxSceneDesc at scene creation.
    */
    EnableGpuDynamics = (1 << 13),

    /**
		\brief Provides improved determinism at the expense of performance. 

		By default, PhysX provides limited determinism guarantees. Specifically, PhysX guarantees that the exact scene (same actors created in the same order) and simulated using the same 
		time-stepping scheme should provide the exact same behaviour.

		However, if additional actors are added to the simulation, this can affect the behaviour of the existing actors in the simulation, even if the set of new actors do not interact with 
		the existing actors.

		This flag provides an additional level of determinism that guarantees that the simulation will not change if additional actors are added to the simulation, provided those actors do not interfere
		with the existing actors in the scene. Determinism is only guaranteed if the actors are inserted in a consistent order each run in a newly-created scene and simulated using a consistent time-stepping
		scheme.

		Note that this flag is not mutable and must be set at scene creation.

		Note that enabling this flag can have a negative impact on performance.

		Note that this feature is not currently supported on GPU.

		<b>Default</b> false
		*/
    EnableEnhancedDeterminism = (1 << 14),

    /**
		\brief Controls processing friction in all solver iterations

		By default, PhysX processes friction only in the final 3 position iterations, and all velocity
		iterations. This flag enables friction processing in all position and velocity iterations.

		The default behaviour provides a good trade-off between performance and stability and is aimed
		primarily at game development.

		When simulating more complex frictional behaviour, such as grasping of complex geometries with
		a robotic manipulator, better results can be achieved by enabling friction in all solver iterations.

		\note This flag only has effect with the default solver. The TGS solver always performs friction per-iteration.
		*/
    EnableFrictionEveryIteration = (1 << 15),

    MutableFlags = EnableActiveActors | ExcludeKinematicsFromActiveActors
}

/**
\brief Pruning structure used to accelerate scene queries.

eNONE uses a simple data structure that consumes less memory than the alternatives,
but generally has slower query performance.

eDYNAMIC_AABB_TREE usually provides the fastest queries. However there is a
constant per-frame management cost associated with this structure. How much work should
be done per frame can be tuned via the #PxSceneDesc::dynamicTreeRebuildRateHint
parameter.

eSTATIC_AABB_TREE is typically used for static objects. It is the same as the
dynamic AABB tree, without the per-frame overhead. This can be a good choice for static
objects, if no static objects are added, moved or removed after the scene has been
created. If there is no such guarantee (e.g. when streaming parts of the world in and out),
then the dynamic version is a better choice even for static objects.

*/
public enum PxPruningStructureType
{
    /// <summary>Using a simple data structure</summary>
    None,

    /// <summary>Using a dynamic AABB tree</summary>
    DynamicAabbTree,

    /// <summary>Using a static AABB tree</summary>	
    StaticAabbTree,
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct PxBounds3
{
    public const float MaxBoundsExtends = float.MaxValue * 0.25f;

    public readonly Vector3 Min;
    public readonly Vector3 Max;

    public PxBounds3(Vector3 min, Vector3 max)
    {
        Min = min;
        Max = max;
    }

    public static PxBounds3 Default => new(
        new Vector3(-MaxBoundsExtends, -MaxBoundsExtends, -MaxBoundsExtends),
        new Vector3(MaxBoundsExtends, MaxBoundsExtends, MaxBoundsExtends)
    );
}

[StructLayout(LayoutKind.Sequential)]
public struct PxgDynamicsMemoryConfig
{
    /// <summary>Capacity of constraint buffer allocated in GPU global memory.</summary>
    public uint ConstraintBufferCapacity;

    /// <summary>Capacity of contact buffer allocated in GPU global memory.</summary>	
    public uint ContactBufferCapacity;

    /// <summary>Capacity of temp buffer allocated in pinned host memory.</summary>
    public uint TempBufferCapacity;

    /// <summary>Size of contact stream buffer allocated in pinned host memory. This is double-buffered so total allocation size = 2* contactStreamCapacity * sizeof(PxContact).</summary>
    public uint ContactStreamSize;

    /// <summary>Size of the contact patch stream buffer allocated in pinned host memory. This is double-buffered so total allocation size = 2 * patchStreamCapacity * sizeof(PxContactPatch).</summary>
    public uint PatchStreamSize;

    /// <summary>Capacity of force buffer allocated in pinned host memory.</summary>
    public uint ForceStreamCapacity;

    /// <summary>Initial capacity of the GPU and pinned host memory heaps. Additional memory will be allocated if more memory is required.</summary>
    public uint HeapCapacity;

    /// <summary>Capacity of found and lost buffers allocated in GPU global memory. This is used for the found/lost pair reports in the BP.</summary>	
    public uint FoundLostPairsCapacity;

    public PxgDynamicsMemoryConfig()
    {
        ConstraintBufferCapacity = 32 * 1024 * 1024;
        ContactBufferCapacity = 24 * 1024 * 1024;
        TempBufferCapacity = 16 * 1024 * 1024;
        ContactStreamSize = 1024 * 512;
        PatchStreamSize = 1024 * 80;
        ForceStreamCapacity = 1 * 1024 * 1024;
        HeapCapacity = 64 * 1024 * 1024;
        FoundLostPairsCapacity = 256 * 1024;
    }
}

/**
\brief Scene query update mode

When PxScene::fetchResults is called it does scene query related work, with this enum it is possible to 
set what work is done during the fetchResults. 

FetchResults will sync changed bounds during simulation and update the scene query bounds in pruners, this work is mandatory.

eCOMMIT_ENABLED_BUILD_ENABLED does allow to execute the new AABB tree build step during fetchResults, additionally the pruner commit is
called where any changes are applied. During commit PhysX refits the dynamic scene query tree and if a new tree was built and 
the build finished the tree is swapped with current AABB tree. 

eCOMMIT_DISABLED_BUILD_ENABLED does allow to execute the new AABB tree build step during fetchResults. Pruner commit is not called,
this means that refit will then occur during the first scene query following fetchResults, or may be forced by the method PxScene::flushSceneQueryUpdates().

eCOMMIT_DISABLED_BUILD_DISABLED no further scene query work is executed. The scene queries update needs to be called manually, see PxScene::sceneQueriesUpdate.
It is recommended to call PxScene::sceneQueriesUpdate right after fetchResults as the pruning structures are not updated. 

*/
public enum PxSceneQueryUpdateMode
{
    /// <summary>Both scene query build and commit are executed.</summary>
    BuildEnabledCommitEnabled,

    /// <summary>Scene query build only is executed.</summary>
    BuildEnabledCommitDisabled,

    /// <summary>No work is done, no update of scene queries.</summary>
    BuildDisabledCommitDisabled
}

public delegate PxFilterFlag PxFilterShaderCallback(
    PxFilterObjectFlag attributes0, ref PxFilterData filterData0,
    PxFilterObjectFlag attributes1, ref PxFilterData filterData1,
    out PxPairFlag pairFlags
);
