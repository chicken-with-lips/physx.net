#include <foundation/PxMathUtils.h>

#include "PxNative.h"
#include "PxPhysics.h"

using namespace physx;

PHYSX_CAPI PxVec3 physx_PxMathUtils_PxDiagonalize(PxMat33 m,  PxQuat& massFrame) {
    return PxDiagonalize(m, massFrame);
}
