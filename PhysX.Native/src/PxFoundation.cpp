#include "PxNative.h"
#include "PxPhysics.h"
#include "foundation/PxFoundation.h"

#include <iostream>

#include "extensions/PxDefaultAllocator.h"
#include "extensions/PxDefaultErrorCallback.h"
#include "common/PxTolerancesScale.h"

using namespace physx;

static PxDefaultAllocator gAllocator;
static PxDefaultErrorCallback gErrorCallback;
static PxTolerancesScale gTolerancesScale;

struct TestStruct
{
    int64_t test;
};

PHYSX_CAPI void *physx_PxFoundation_New(PxU32 version) {
    return PxCreateFoundation(version, gAllocator, gErrorCallback);
}

PHYSX_CAPI void *physx_PxFoundation_CreatePhysics(PxFoundation &foundation, PxU32 version, PxTolerancesScale &scale,
                                                  bool trackOutstandingAllocations, PxPvd *pvd) {
    return PxCreatePhysics(version, foundation, gTolerancesScale, trackOutstandingAllocations, pvd);
}
