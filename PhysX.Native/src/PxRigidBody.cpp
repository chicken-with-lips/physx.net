#include "PxPhysics.h"
#include "PxRigidBody.h"

using namespace physx;

extern "C" void physx_PxRigidBody_AddForce(PxRigidBody *actor, PxVec3& force, PxForceMode::Enum mode, bool autowake) {
    actor->addForce(force, mode, autowake);
}

extern "C" void physx_PxRigidBody_AddTorque(PxRigidBody *actor, PxVec3& torque, PxForceMode::Enum mode, bool autowake) {
    actor->addTorque(torque, mode, autowake);
}

extern "C" void physx_PxRigidBody_SetLinearDamping(PxRigidBody *actor, float value) {
    actor->setLinearDamping(value);
}

extern "C" float physx_PxRigidBody_GetLinearDamping(PxRigidBody *actor) {
    return actor->getLinearDamping();
}

extern "C" void physx_PxRigidBody_SetAngularDamping(PxRigidBody *actor, float value) {
    actor->setAngularDamping(value);
}

extern "C" float physx_PxRigidBody_GetAngularDamping(PxRigidBody *actor) {
    return actor->getAngularDamping();
}
