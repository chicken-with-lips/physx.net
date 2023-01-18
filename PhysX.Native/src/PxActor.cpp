#include "PxNative.h"
#include "PxPhysics.h"

using namespace physx;

PHYSX_CAPI void physx_PxActor_SetActorFlag(PxActor *actor, PxActorFlag::Enum flag, bool value) {
    actor->setActorFlag(flag, value);
}

PHYSX_CAPI const char* physx_PxActor_GetName(PxActor *actor) {
    return actor->getName();
}
