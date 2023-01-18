#include "PxNative.h"
#include "PxPhysics.h"
#include "foundation/PxMathUtils.h"

using namespace physx;

PHYSX_CAPI PxVec3 physx_PxUtil_Diagonalize(const PxMat33& m, PxQuat& massFrame) {
    return PxDiagonalize(m, massFrame);
}
