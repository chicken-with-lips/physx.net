#include "PxNative.h"

#include <extensions/PxDistanceJoint.h>

using namespace physx;

PHYSX_CAPI void* physx_PxDistanceJoint_Create(PxPhysics* physics, PxRigidActor* actor0, PxTransform& localFrame0,
                                           PxRigidActor* actor1, PxTransform& localFrame1)
{
    return PxDistanceJointCreate(*physics, actor0, localFrame0, actor1, localFrame1);
}

PHYSX_CAPI float physx_PxDistanceJoint_GetDistance(PxDistanceJoint *joint)
{
    return joint->getDistance();
}

PHYSX_CAPI void physx_PxDistanceJoint_SetMinDistance(PxDistanceJoint *joint, float distance)
{
    joint->setMinDistance(distance);
}


PHYSX_CAPI float physx_PxDistanceJoint_GetMinDistance(PxDistanceJoint *joint)
{
    return joint->getMinDistance();
}

PHYSX_CAPI void physx_PxDistanceJoint_SetMaxDistance(PxDistanceJoint *joint, float distance)
{
    joint->setMaxDistance(distance);
}

PHYSX_CAPI float physx_PxDistanceJoint_GetMaxDistance(PxDistanceJoint *joint)
{
    return joint->getMaxDistance();
}

PHYSX_CAPI void physx_PxDistanceJoint_SetDistanceJointFlag(PxDistanceJoint *joint, PxU16 flag, bool value)
{
    joint->setDistanceJointFlag(static_cast<PxDistanceJointFlag::Enum>(flag), value);
}
