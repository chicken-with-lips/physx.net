#include "PxPhysics.h"
#include "PxRigidActor.h"

using namespace physx;

extern "C" void physx_PxActor_SetActorFlag(PxActor *actor, PxActorFlag::Enum flag, bool value) {
    actor->setActorFlag(flag, value);
}
