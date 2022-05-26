using System.Numerics;
using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Net.Native;

internal static class PxScene
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_Simulate")]
    public static extern void Simulate(IntPtr scene, float elapsedTime);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_FetchResults")]
    public static extern bool FetchResults(IntPtr scene, bool block, ref uint errorState);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_AddActor")]
    public static extern void AddActor(IntPtr scene, IntPtr actor);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_GetGravity")]
    public static extern Vector3 GetGravity(IntPtr scene);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxScene_SetGravity")]
    public static extern void SetGravity(IntPtr scene, Vector3 vector);
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
    public IntPtr FilterShader;
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
    public float CcdMaxSeparation;
    public float SolverOffsetSlop;
    public PxSceneFlag Flags;
    public IntPtr CpuDispatcher;
    public IntPtr CudaContextManager;
    public PxPruningStructureType StaticStructure;
    public PxPruningStructureType DynamicStructure;
    public uint DynamicTreeRebuildRateHint;
    public PxSceneQueryUpdateMode SceneQueryUpdateMode;
    public IntPtr UserData;
    public uint SolverBatchSize;
    public uint SolverArticulationBatchSize;
    public uint NbContactDataBlocks;
    public uint MaxNbContactDataBlocks;
    public float MaxBiasCoefficient;
    public uint ContactReportStreamBufferSize;
    public uint CcdMaxPasses;
    public float CcdThreshold;
    public float WakeCounterResetValue;
    public PxBounds3 SanityBounds;
    public PxgDynamicsMemoryConfig GpuDynamicsConfig;
    public uint GpuMaxNumPartitions;
    public uint GpuComputeVersion;
    public readonly PxTolerancesScale TolerancesScale;

    public PxSceneDescTransfer(PxSceneDesc desc)
    {
        Gravity = desc.Gravity;
        SimulationEventCallback = desc.SimulationEventCallback;
        ContactModifyCallback = desc.ContactModifyCallback;
        CcdContactModifyCallback = desc.CcdContactModifyCallback;

        FilterShaderData = desc.FilterShaderData;
        FilterShaderDataSize = desc.FilterShaderDataSize;
        FilterShader = desc.FilterShader;
        FilterCallback = desc.FilterCallback;

        KineKineFilteringMode = desc.KineKineFilteringMode;
        StaticKineFilteringMode = desc.StaticKineFilteringMode;

        BroadPhaseType = desc.BroadPhaseType;
        BroadPhaseCallback = desc.BroadPhaseCallback;

        FrictionType = desc.FrictionType;
        SolverType = desc.SolverType;
        BounceThresholdVelocity = desc.BounceThresholdVelocity;
        FrictionOffsetThreshold = desc.FrictionOffsetThreshold;
        CcdMaxSeparation = desc.CcdMaxSeparation;
        SolverOffsetSlop = desc.SolverOffsetSlop;

        Flags = desc.Flags;

        CpuDispatcher = desc.CpuDispatcher.NativePtr;
        CudaContextManager = desc.CudaContextManager;

        StaticStructure = desc.StaticStructure;
        DynamicStructure = desc.DynamicStructure;
        DynamicTreeRebuildRateHint = desc.DynamicTreeRebuildRateHint;
        SceneQueryUpdateMode = desc.SceneQueryUpdateMode;

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
        GpuComputeVersion = desc.GpuComputeVersion;
        TolerancesScale = desc.TolerancesScale;

        Limits = desc.Limits;
        GpuDynamicsConfig = desc.GpuDynamicsConfig;
    }
}
