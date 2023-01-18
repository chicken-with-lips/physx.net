#include "PxNative.h"
#include "PxPhysics.h"
#include "PxScene.h"

using namespace physx;

PHYSX_CAPI void physx_PxScene_Simulate(PxScene *scene, float elapsedTime) {
    scene->simulate(elapsedTime);
}

PHYSX_CAPI bool physx_PxScene_FetchResults(PxScene *scene, bool block, unsigned int *errorState) {
    return scene->fetchResults(block, errorState);
}

PHYSX_CAPI void physx_PxScene_AddActor(PxScene *scene, PxActor *actor) {
    scene->addActor(*actor);
}

PHYSX_CAPI void physx_PxScene_RemoveActor(PxScene *scene, PxActor *actor, bool wakeOnLostTouch) {
    scene->removeActor(*actor, wakeOnLostTouch);
}

PHYSX_CAPI void physx_PxScene_SetGravity(PxScene *scene, PxVec3 &vec) {
    scene->setGravity(vec);
}

PHYSX_CAPI PxVec3 physx_PxScene_GetGravity(PxScene *scene) {
    return scene->getGravity();
}

PHYSX_CAPI bool physx_PxScene_Overlap(PxScene *scene, PxGeometry &geometry, PxTransform &pose/*, PxOverlapBuffer *hitCall*/,
                                      PxU16 filterData) {
    PxOverlapBuffer hitCall;

    bool res = scene->overlap(geometry, pose, hitCall, PxQueryFilterData((PxQueryFlags) filterData));

    return res;
}