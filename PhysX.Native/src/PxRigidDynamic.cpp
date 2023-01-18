#include "PxNative.h"
#include "PxPhysics.h"
#include "PxRigidDynamic.h"

using namespace physx;

PHYSX_CAPI void
physx_PxRigidDynamic_SetRigidDynamicFlag(PxRigidDynamic *actor, PxRigidBodyFlag::Enum flag, bool value) {
    actor->setRigidBodyFlag(flag, value);
}

PHYSX_CAPI void physx_PxRigidDynamic_SetRigidDynamicLockFlags(PxRigidDynamic *actor, PxU8 flags) {
    actor->setRigidDynamicLockFlags((PxRigidDynamicLockFlags) flags);
}

PHYSX_CAPI PxRigidDynamicLockFlags physx_PxRigidDynamic_GetRigidDynamicLockFlags(PxRigidDynamic *actor) {
    return actor->getRigidDynamicLockFlags();
}
