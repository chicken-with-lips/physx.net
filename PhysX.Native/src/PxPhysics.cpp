#include "PxPhysics.h"
#include "PxScene.h"
#include "PxSimulationEventCallback.h"
#include "extensions/PxExtensionsAPI.h"
#include <iostream>
#include <map>

using namespace physx;
using namespace std;

typedef PxU16 (*WrapperSimulationFilterShader)
        (PxU32 attributes0, PxFilterData& filterData0,
         PxFilterObjectAttributes attributes1, PxFilterData& filterData1,
         PxU16 *pairFlags);

class WrapperShaderEntry {
public:
    WrapperSimulationFilterShader Shader;
};

map<PxScene *, WrapperShaderEntry *> wrapperSimulationFilterCallbackList;

PxFilterFlags ProxyFilterShader(
        PxFilterObjectAttributes attributes0, PxFilterData filterData0,
        PxFilterObjectAttributes attributes1, PxFilterData filterData1,
        PxPairFlags &pairFlags, const void *constantBlock, PxU32 constantBlockSize) {
    const auto *wrapperEntry = reinterpret_cast<const WrapperShaderEntry *>(constantBlock);

    PxU16 wrapperPairFlags = 0;
    WrapperSimulationFilterShader shader = wrapperEntry->Shader;

    auto wrapperReturn = (*shader)(
        attributes0, filterData0,
        attributes1, filterData1,
        &wrapperPairFlags
    );

    pairFlags = (PxPairFlags &) wrapperPairFlags;

    return (PxFilterFlags &) wrapperReturn;
}

class PxSceneDescTransfer {
public:
    PxVec3 gravity;
    PxSimulationEventCallback *simulationEventCallback;
    PxContactModifyCallback *contactModifyCallback;
    PxCCDContactModifyCallback *ccdContactModifyCallback;
    const void *filterShaderData;
    PxU32 filterShaderDataSize;
    WrapperSimulationFilterShader filterShader;
    PxSimulationFilterCallback *filterCallback;
    PxPairFilteringMode::Enum kineKineFilteringMode;
    PxPairFilteringMode::Enum staticKineFilteringMode;
    PxBroadPhaseType::Enum broadPhaseType;
    PxBroadPhaseCallback *broadPhaseCallback;
    PxSceneLimits limits;
    PxFrictionType::Enum frictionType;
    PxSolverType::Enum solverType;
    PxReal bounceThresholdVelocity;
    PxReal frictionOffsetThreshold;
    PxReal frictionCorrelationDistance;
    PxU32 flags;
    PxCpuDispatcher *cpuDispatcher;
    PxCudaContextManager *cudaContextManager;
    void *userData;
    PxU32 solverBatchSize;
    PxU32 solverArticulationBatchSize;
    PxU32 nbContactDataBlocks;
    PxU32 maxNbContactDataBlocks;
    PxReal maxBiasCoefficient;
    PxU32 contactReportStreamBufferSize;
    PxU32 ccdMaxPasses;
    PxReal ccdThreshold;
    PxReal ccdMaxSeparation;
    PxReal wakeCounterResetValue;
    PxBounds3 sanityBounds;
    PxgDynamicsMemoryConfig gpuDynamicsConfig;
    PxU32 gpuMaxNumPartitions;
    PxU32 gpuMaxNumStaticPartitions;
    PxU32 gpuComputeVersion;
    PxU32 contactPairSlabSize;

private:
    PxTolerancesScale tolerancesScale;

public:
    PX_INLINE const PxTolerancesScale &getTolerancesScale() const { return tolerancesScale; }

    void CopyTo(PxSceneDesc &desc) const {
        desc.gravity = gravity;
        desc.simulationEventCallback = simulationEventCallback;
        desc.contactModifyCallback = contactModifyCallback;
        desc.ccdContactModifyCallback = ccdContactModifyCallback;
        desc.filterShaderData = filterShaderData;
        desc.filterShaderDataSize = filterShaderDataSize;
        desc.filterShader = ProxyFilterShader;
        desc.filterCallback = filterCallback;
        desc.kineKineFilteringMode = kineKineFilteringMode;
        desc.staticKineFilteringMode = staticKineFilteringMode;
        desc.broadPhaseType = broadPhaseType;
        desc.broadPhaseCallback = broadPhaseCallback;
        desc.limits = limits;
        desc.frictionType = frictionType;
        desc.solverType = solverType;
        desc.bounceThresholdVelocity = bounceThresholdVelocity;
        desc.frictionOffsetThreshold = frictionOffsetThreshold;
        desc.frictionCorrelationDistance = frictionCorrelationDistance;
        desc.ccdMaxSeparation = ccdMaxSeparation;
        desc.flags = (PxSceneFlags) flags;
        desc.cpuDispatcher = cpuDispatcher;
        desc.cudaContextManager = cudaContextManager;
        desc.userData = userData;
        desc.solverBatchSize = solverBatchSize;
        desc.solverArticulationBatchSize = solverArticulationBatchSize;
        desc.nbContactDataBlocks = nbContactDataBlocks;
        desc.maxNbContactDataBlocks = maxNbContactDataBlocks;
        desc.maxBiasCoefficient = maxBiasCoefficient;
        desc.contactReportStreamBufferSize = contactReportStreamBufferSize;
        desc.ccdMaxPasses = ccdMaxPasses;
        desc.ccdThreshold = ccdThreshold;
        desc.wakeCounterResetValue = wakeCounterResetValue;
        desc.sanityBounds = sanityBounds;
        desc.gpuDynamicsConfig = gpuDynamicsConfig;
        desc.gpuMaxNumPartitions = gpuMaxNumPartitions;
        desc.gpuMaxNumStaticPartitions = gpuMaxNumStaticPartitions;
        desc.gpuComputeVersion = gpuComputeVersion;
        desc.contactPairSlabSize = contactPairSlabSize;
    }
};

extern "C" void *physx_PxPhysics_GetTolerancesScale(PxPhysics *physics) {
    return (void *) &physics->getTolerancesScale();
}

extern "C" void *physx_PxPhysics_CreateScene(PxPhysics *physics, PxSceneDescTransfer &tmpDesc) {
    PxSceneDesc sceneDesc(tmpDesc.getTolerancesScale());
    tmpDesc.CopyTo(sceneDesc);

    auto wrapperEntry = new WrapperShaderEntry();
    wrapperEntry->Shader = tmpDesc.filterShader;

    sceneDesc.filterShaderData = wrapperEntry;
    sceneDesc.filterShaderDataSize = sizeof(WrapperShaderEntry);
    sceneDesc.filterShader = ProxyFilterShader;

    auto scene = physics->createScene(sceneDesc);

    wrapperSimulationFilterCallbackList.insert(pair<PxScene *, WrapperShaderEntry *>(scene, wrapperEntry));

    return scene;
}

extern "C" void *
physx_PxPhysics_CreateMaterial(PxPhysics *physics, float staticFriction, float dynamicFriction, float restitution) {
    return physics->createMaterial(staticFriction, dynamicFriction,
                                   restitution);
}

extern "C" void *
physx_PxPhysics_CreateShape(PxPhysics *physics, PxGeometry& geometry, PxMaterial *material, bool isExclusive,
                            PxU8 shapeFlags) {
    return physics->createShape(geometry, *material, isExclusive, (PxShapeFlags) shapeFlags);
}

extern "C" void *
physx_PxPhysics_CreateRigidDynamic(PxPhysics *physics, PxTransform &transform) {
    return physics->createRigidDynamic(transform);
}

extern "C" void *
physx_PxPhysics_CreateRigidStatic(PxPhysics *physics, PxTransform &transform) {
    return physics->createRigidStatic(transform);
}

extern "C" bool
physx_PxPhysics_InitExtensions(PxPhysics *physics, PxPvd *pvd) {
    return PxInitExtensions(*physics, pvd);
}