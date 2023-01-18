#include "PxNative.h"
#include "PxPhysics.h"
#include "PxRigidActor.h"

using namespace physx;

PHYSX_CAPI bool physx_PxRigidActor_AttachShape(PxRigidActor *actor, PxShape *shape) {
    return actor->attachShape(*shape);
}

PHYSX_CAPI PxTransform physx_PxRigidActor_GetGlobalPose(PxRigidActor *actor) {
    return actor->getGlobalPose();
}

PHYSX_CAPI void physx_PxRigidActor_SetGlobalPose(PxRigidActor *actor, PxTransform& pose, bool autowake) {
    actor->setGlobalPose(pose, autowake);
}