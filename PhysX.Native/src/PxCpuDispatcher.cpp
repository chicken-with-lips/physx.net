#include "extensions/PxDefaultCpuDispatcher.h"

using namespace physx;

extern "C" PxU32 physx_PxCpuDispatcher_getWorkerCount(PxCpuDispatcher *cpuDispatcher) {
    return cpuDispatcher->getWorkerCount();
}