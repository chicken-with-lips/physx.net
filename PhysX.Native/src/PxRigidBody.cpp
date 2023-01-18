#include "PxNative.h"
#include "PxPhysics.h"
#include "PxRigidBody.h"

using namespace physx;

PHYSX_CAPI void physx_PxRigidBody_AddForce(PxRigidBody *actor, PxVec3& force, PxForceMode::Enum mode, bool autowake) {
    actor->addForce(force, mode, autowake);
}

PHYSX_CAPI void physx_PxRigidBody_AddTorque(PxRigidBody *actor, PxVec3& torque, PxForceMode::Enum mode, bool autowake) {
    actor->addTorque(torque, mode, autowake);
}

PHYSX_CAPI void physx_PxRigidBody_ClearForce(PxRigidBody* actor, PxForceMode::Enum mode) {
    actor->clearForce(mode);
}

PHYSX_CAPI void physx_PxRigidBody_ClearTorque(PxRigidBody* actor, PxForceMode::Enum mode) {
    actor->clearTorque(mode);
}

PHYSX_CAPI void physx_PxRigidBody_SetForceAndTorque(PxRigidBody* actor, PxVec3& force, PxVec3& torque, PxForceMode::Enum mode) {
    actor->setForceAndTorque(force, torque, mode);
}

PHYSX_CAPI void physx_PxRigidBody_SetLinearDamping(PxRigidBody *actor, float value) {
    actor->setLinearDamping(value);
}

PHYSX_CAPI float physx_PxRigidBody_GetLinearDamping(PxRigidBody *actor) {
    return actor->getLinearDamping();
}

PHYSX_CAPI void physx_PxRigidBody_SetAngularDamping(PxRigidBody *actor, float value) {
    actor->setAngularDamping(value);
}

PHYSX_CAPI float physx_PxRigidBody_GetAngularDamping(PxRigidBody *actor) {
    return actor->getAngularDamping();
}

PHYSX_CAPI PxVec3 physx_PxRigidBody_GetLinearVelocity(PxRigidBody* actor) {
    return actor->getLinearVelocity();
}

PHYSX_CAPI PxVec3 physx_PxRigidBody_GetAngularVelocity(PxRigidBody* actor) {
    return actor->getAngularVelocity();
}

PHYSX_CAPI void physx_PxRigidBody_SetMassSpaceInertiaTensor(PxRigidBody* actor, PxVec3& m) {
    actor->setMassSpaceInertiaTensor(m);
}

PHYSX_CAPI PxVec3 physx_PxRigidBody_GetMassSpaceInertiaTensor(PxRigidBody* actor) {
    return actor->getMassSpaceInertiaTensor();
}

PHYSX_CAPI PxVec3 physx_PxRigidBody_GetMassSpaceInvInertiaTensor(PxRigidBody* actor) {
    return actor->getMassSpaceInvInertiaTensor();
}

PHYSX_CAPI void physx_PxRigidBody_SetCMassLocalPose(PxRigidBody* actor, PxTransform& pose) {
    actor->setCMassLocalPose(pose);
}

PHYSX_CAPI PxTransform physx_PxRigidBody_GetCMassLocalPose(PxRigidBody* actor) {
    return actor->getCMassLocalPose();
}
