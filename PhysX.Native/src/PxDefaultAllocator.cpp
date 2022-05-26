#include "extensions/PxDefaultAllocator.h"

using namespace physx;

extern "C" void *physx_PxDefaultAllocator_New() {
    return (void *) new PxDefaultAllocator();
}