#include "PxPhysics.h"
#include "PxRigidActor.h"
#include <iostream>

using namespace physx;

extern "C" bool physx_PxRigidActor_AttachShape(PxRigidActor *actor, PxShape *shape) {
    return actor->attachShape(*shape);
}

extern "C" PxTransform physx_PxRigidActor_GetGlobalPose(PxRigidActor *actor) {
    return actor->getGlobalPose();
}

extern "C" void physx_PxRigidActor_SetGlobalPose(PxRigidActor *actor, PxTransform& pose, bool autowake) {
    actor->setGlobalPose(pose, autowake);
}