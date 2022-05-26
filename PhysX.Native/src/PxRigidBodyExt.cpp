#include "extensions/PxRigidBodyExt.h"

using namespace physx;

extern "C" bool physx_PxRigidBodyExt_UpdateMassAndInertia(PxRigidBody *body, float density, PxVec3 *massLocalPose, bool includeNonSimShapes) {
    return PxRigidBodyExt::updateMassAndInertia(*body, density, massLocalPose, includeNonSimShapes);
}