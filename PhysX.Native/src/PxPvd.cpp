#include "PxPhysics.h"
#include "PxScene.h"
#include "extensions/PxDefaultSimulationFilterShader.h"
#include "pvd/PxPvd.h"

using namespace physx;

extern "C" void * physx_PxPvd_New(PxFoundation *foundation) {
    return (void*) PxCreatePvd(*foundation);
}

extern "C" bool physx_PxPvd_Connect(PxPvd *pvd, PxPvdTransport *transport, PxU8 flags) {
    return pvd->connect(*transport, (PxPvdInstrumentationFlags)  flags);
}
