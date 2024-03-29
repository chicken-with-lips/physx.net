#include "PxNative.h"
#include "PxPhysics.h"
#include "PxScene.h"
#include "pvd/PxPvd.h"

using namespace physx;

PHYSX_CAPI void * physx_PxPvd_New(PxFoundation *foundation) {
    return PxCreatePvd(*foundation);
}

PHYSX_CAPI bool physx_PxPvd_Connect(PxPvd *pvd, PxPvdTransport *transport, PxU8 flags) {
    return pvd->connect(*transport, static_cast<PxPvdInstrumentationFlags>(flags));
}
