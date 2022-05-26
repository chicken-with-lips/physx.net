#include "extensions/PxDefaultCpuDispatcher.h"

using namespace physx;

extern "C" void *physx_PxDefaultCpuDispatcher_New(PxU32 numThreads) {
    return (void *) PxDefaultCpuDispatcherCreate(numThreads);
}
