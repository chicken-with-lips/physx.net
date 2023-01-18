#include "PxNative.h"
#include "foundation/PxFoundation.h"
#include "foundation/PxPhysicsVersion.h"

using namespace physx;

PHYSX_CAPI PxU32 physx_PxVersion_VersionConst() {
    return PX_PHYSICS_VERSION;
}