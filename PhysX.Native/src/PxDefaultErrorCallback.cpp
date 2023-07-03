#include "PxNative.h"
#include "extensions/PxDefaultErrorCallback.h"

using namespace physx;

PHYSX_CAPI void *physx_PxDefaultErrorCallback_New() {
    return new PxDefaultErrorCallback();
}
