using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxScene
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_Simulate")]
    public static extern void Simulate(IntPtr scene, float elapsedTime);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_FetchResults")]
    public static extern bool FetchResults(IntPtr scene, bool block, ref uint errorState);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_AddActor")]
    public static extern void AddActor(IntPtr scene, IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_RemoveActor")]
    public static extern void RemoveActor(IntPtr scene, IntPtr actor, bool wakeOnLostTouch = true);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_GetGravity")]
    public static extern Vector3 GetGravity(IntPtr scene);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_SetGravity")]
    public static extern void SetGravity(IntPtr scene, Vector3 vector);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_Overlap")]
    public static extern bool Overlap(IntPtr scene, ref PxBoxGeometry geometry, ref PxTransform pose, PxQueryFlag filterData);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_Overlap")]
    public static extern bool Overlap(IntPtr scene, ref PxSphereGeometry geometry, ref PxTransform pose, PxQueryFlag filterData);
}

[StructLayout(LayoutKind.Sequential)]
internal struct PxSceneDescTransfer
{
    public Vector3 Gravity;
    public IntPtr SimulationEventCallback;
    public IntPtr ContactModifyCallback;
    public IntPtr CcdContactModifyCallback;
    public IntPtr FilterShaderData;
    public uint FilterShaderDataSize;
    public PxFilterShaderCallback FilterShader;
    public IntPtr FilterCallback;
    public PxPairFilteringMode KineKineFilteringMode;
    public PxPairFilteringMode StaticKineFilteringMode;
    public PxBroadPhaseType BroadPhaseType;
    public IntPtr BroadPhaseCallback;
    public PxSceneLimits Limits;
    public PxFrictionType FrictionType;
    public PxSolverType SolverType;
    public float BounceThresholdVelocity;
    public float FrictionOffsetThreshold;
    public float FrictionCorrelationDistance;
    public PxSceneFlag Flags;
    public IntPtr CpuDispatcher;
    public IntPtr CudaContextManager;
    public IntPtr UserData;
    public uint SolverBatchSize;
    public uint SolverArticulationBatchSize;
    public uint NbContactDataBlocks;
    public uint MaxNbContactDataBlocks;
    public float MaxBiasCoefficient;
    public uint ContactReportStreamBufferSize;
    public uint CcdMaxPasses;
    public float CcdThreshold;
    public float CcdMaxSeparation;
    public float WakeCounterResetValue;
    public PxBounds3 SanityBounds;
    public PxgDynamicsMemoryConfig GpuDynamicsConfig;
    public uint GpuMaxNumPartitions;
    public uint GpuMaxNumStaticPartitions;
    public uint GpuComputeVersion;
    public uint ContactPairSlabSize;
    public readonly PxTolerancesScale TolerancesScale;

    public PxSceneDescTransfer(PxSceneDesc desc)
    {
        Gravity = desc.Gravity;
        SimulationEventCallback = desc.SimulationEventCallback?.NativePtr ?? IntPtr.Zero;
        ContactModifyCallback = desc.ContactModifyCallback;
        CcdContactModifyCallback = desc.CcdContactModifyCallback;

        FilterShaderData = desc.FilterShaderData;
        FilterShaderDataSize = desc.FilterShaderDataSize;
        FilterShader = desc.FilterShader;
        FilterCallback = desc.FilterCallback.NativePtr;

        KineKineFilteringMode = desc.KineKineFilteringMode;
        StaticKineFilteringMode = desc.StaticKineFilteringMode;

        BroadPhaseType = desc.BroadPhaseType;
        BroadPhaseCallback = desc.BroadPhaseCallback;

        FrictionType = desc.FrictionType;
        SolverType = desc.SolverType;
        BounceThresholdVelocity = desc.BounceThresholdVelocity;
        FrictionOffsetThreshold = desc.FrictionOffsetThreshold;
        FrictionCorrelationDistance = desc.FrictionCorrelationDistance;
        CcdMaxSeparation = desc.CcdMaxSeparation;

        Flags = desc.Flags;

        CpuDispatcher = desc.CpuDispatcher.NativePtr;
        CudaContextManager = desc.CudaContextManager;

        UserData = desc.UserData;

        SolverBatchSize = desc.SolverBatchSize;
        SolverArticulationBatchSize = desc.SolverArticulationBatchSize;

        NbContactDataBlocks = desc.NbContactDataBlocks;
        MaxNbContactDataBlocks = desc.MaxNbContactDataBlocks;
        MaxBiasCoefficient = desc.MaxBiasCoefficient;
        ContactReportStreamBufferSize = desc.ContactReportStreamBufferSize;
        CcdMaxPasses = desc.CcdMaxPasses;
        CcdThreshold = desc.CcdThreshold;
        WakeCounterResetValue = desc.WakeCounterResetValue;
        SanityBounds = desc.SanityBounds;
        GpuMaxNumPartitions = desc.GpuMaxNumPartitions;
        GpuMaxNumStaticPartitions = desc.GpuMaxNumStaticPartitions;
        GpuComputeVersion = desc.GpuComputeVersion;
        TolerancesScale = desc.TolerancesScale;
        ContactPairSlabSize = desc.ContactPairSlabSize;

        Limits = desc.Limits;
        GpuDynamicsConfig = desc.GpuDynamicsConfig;
    }
}
