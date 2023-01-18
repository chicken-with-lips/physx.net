#include "PxNative.h"
#include "extensions/PxDefaultCpuDispatcher.h"

using namespace physx;

PHYSX_CAPI PxU32 physx_PxCpuDispatcher_getWorkerCount(PxCpuDispatcher *cpuDispatcher) {
    return cpuDispatcher->getWorkerCount();
}