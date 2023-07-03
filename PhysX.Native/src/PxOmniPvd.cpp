#include "PxNative.h"
#include "PxPhysics.h"
#include "omnipvd/PxOmniPvd.h"

using namespace physx;

PHYSX_CAPI void * physx_PxOmniPvd_New(PxFoundation *foundation) {
    return PxCreateOmniPvd(*foundation);
}

// PHYSX_CAPI bool physx_PxPvd_Connect(PxOmniPvd *pvd, PxPvdTransport *transport, PxU8 flags) {
    // return pvd->connect(*transport, (PxPvdInstrumentationFlags)  flags);
// }
