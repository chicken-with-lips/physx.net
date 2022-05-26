#include "PxPhysics.h"
#include "PxRigidDynamic.h"

using namespace physx;

extern "C" void
physx_PxRigidDynamic_SetRigidDynamicFlag(PxRigidDynamic *actor, PxRigidBodyFlag::Enum flag, bool value) {
    actor->setRigidBodyFlag(flag, value);
}

extern "C" void physx_PxRigidDynamic_SetRigidDynamicLockFlags(PxRigidDynamic *actor, PxU8 flags) {
    actor->setRigidDynamicLockFlags((PxRigidDynamicLockFlags) flags);
}

extern "C" PxRigidDynamicLockFlags physx_PxRigidDynamic_GetRigidDynamicLockFlags(PxRigidDynamic *actor) {
    return actor->getRigidDynamicLockFlags();
}
