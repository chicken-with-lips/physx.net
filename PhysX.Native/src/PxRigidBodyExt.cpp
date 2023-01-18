#include "PxNative.h"
#include "extensions/PxRigidBodyExt.h"

using namespace physx;

PHYSX_CAPI bool physx_PxRigidBodyExt_UpdateMassAndInertia(PxRigidBody *body, float density, PxVec3 *massLocalPose, bool includeNonSimShapes) {
    return PxRigidBodyExt::updateMassAndInertia(*body, density, massLocalPose, includeNonSimShapes);
}