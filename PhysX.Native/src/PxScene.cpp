#include "PxPhysics.h"
#include "PxScene.h"

using namespace physx;

extern "C" void physx_PxScene_Simulate(PxScene *scene, float elapsedTime) {
    scene->simulate(elapsedTime);
}

extern "C" bool physx_PxScene_FetchResults(PxScene *scene, bool block, uint *errorState) {
    return scene->fetchResults(block, errorState);
}

extern "C" void physx_PxScene_AddActor(PxScene *scene, PxActor *actor) {
    return scene->addActor(*actor);
}

extern "C" void physx_PxScene_SetGravity(PxScene *scene, PxVec3 &vec) {
    scene->setGravity(vec);
}

extern "C" PxVec3 physx_PxScene_GetGravity(PxScene *scene) {
    return scene->getGravity();
}
