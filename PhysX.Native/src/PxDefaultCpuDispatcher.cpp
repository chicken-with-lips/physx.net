#include "PxNative.h"
#include "extensions/PxDefaultCpuDispatcher.h"

using namespace physx;

PHYSX_CAPI void *physx_PxDefaultCpuDispatcher_New(PxU32 numThreads) {
    return (void *) PxDefaultCpuDispatcherCreate(numThreads);
}
