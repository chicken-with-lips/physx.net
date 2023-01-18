#include "PxNative.h"
#include "PxPhysics.h"
#include "pvd/PxPvdTransport.h"

using namespace physx;

PHYSX_CAPI void *
physx_PxPvdTransport_NewSocketTransferTransport(const char *host, int port, unsigned int timeoutInMilliseconds) {
    return (void *) PxDefaultPvdSocketTransportCreate(host, port, timeoutInMilliseconds);
}
