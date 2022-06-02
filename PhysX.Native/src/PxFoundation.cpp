#include "PxFoundation.h"
#include "extensions/PxDefaultAllocator.h"
#include "extensions/PxDefaultErrorCallback.h"
#include "common/PxTolerancesScale.h"
#include "PxPhysics.h"

using namespace physx;

static PxDefaultAllocator gAllocator;
static PxDefaultErrorCallback gErrorCallback;
static PxTolerancesScale gTolerancesScale;

extern "C" void *physx_PxFoundation_New(PxU32 version) {
    return (void *) PxCreateFoundation(version, gAllocator, gErrorCallback);
}

extern "C" void *physx_PxFoundation_CreatePhysics(PxFoundation &foundation, PxU32 version, bool trackOutstandingAllocations, PxPvd *pvd) {
    return (void *) PxCreatePhysics(version, foundation, gTolerancesScale, trackOutstandingAllocations, pvd);
}