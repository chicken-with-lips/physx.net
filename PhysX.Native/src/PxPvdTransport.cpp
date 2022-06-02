#include "PxPhysics.h"
#include "pvd/PxPvdTransport.h"

using namespace physx;

extern "C" void *
physx_PxPvdTransport_NewSocketTransferTransport(const char *host, int port, unsigned int timeoutInMilliseconds) {
    return (void *) PxDefaultPvdSocketTransportCreate(host, port, timeoutInMilliseconds);
}
