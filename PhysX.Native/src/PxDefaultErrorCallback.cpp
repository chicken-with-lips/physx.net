#include "extensions/PxDefaultErrorCallback.h"

using namespace physx;

extern "C" void *physx_PxDefaultErrorCallback_New() {
    return (void *) new PxDefaultErrorCallback();
}