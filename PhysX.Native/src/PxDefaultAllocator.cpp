#include "PxNative.h"
#include "extensions/PxDefaultAllocator.h"

using namespace physx;

PHYSX_CAPI void *physx_PxDefaultAllocator_New() {
    return (void *) new PxDefaultAllocator();
}