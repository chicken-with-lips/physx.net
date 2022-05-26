#include "PxFoundation.h"
#include "PxPhysicsVersion.h"

using namespace physx;

extern "C" PxU32 physx_PxVersion_VersionConst() {
    return PX_PHYSICS_VERSION;
}