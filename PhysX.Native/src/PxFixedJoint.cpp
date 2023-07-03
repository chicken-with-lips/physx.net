#include "PxNative.h"

#include <extensions/PxFixedJoint.h>

using namespace physx;

PHYSX_CAPI void* physx_PxFixedJoint_Create(PxPhysics* physics, PxRigidActor* actor0, PxTransform& localFrame0,
                                           PxRigidActor* actor1, PxTransform& localFrame1)
{
    return PxFixedJointCreate(*physics, actor0, localFrame0, actor1, localFrame1);
}
