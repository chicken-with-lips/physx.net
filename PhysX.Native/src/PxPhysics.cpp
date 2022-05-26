#include "PxPhysics.h"
#include "PxScene.h"
#include "extensions/PxDefaultSimulationFilterShader.h"
#include "extensions/PxExtensionsAPI.h"
#include <iostream>

using namespace physx;

extern "C" void *physx_PxPhysics_GetTolerancesScale(PxPhysics *physics) {
    return (void *) &physics->getTolerancesScale();
}

extern "C" void *physx_PxPhysics_New(PxPhysics *physics, PxSceneDesc &sceneDesc) {
    sceneDesc.filterShader = PxDefaultSimulationFilterShader;

    return (void *) physics->createScene(sceneDesc);
}

extern "C" void *physx_PxPhysics_CreateScene(PxPhysics *physics, PxSceneDesc &sceneDesc) {
    sceneDesc.filterShader = PxDefaultSimulationFilterShader;

    return (void *) physics->createScene(sceneDesc);
}

extern "C" void *
physx_PxPhysics_CreateMaterial(PxPhysics *physics, float staticFriction, float dynamicFriction, float restitution) {
    return (void *) physics->createMaterial(staticFriction, dynamicFriction,
                                            restitution);
}

extern "C" void *
physx_PxPhysics_CreateShape(PxPhysics *physics, PxGeometry *geometry, PxMaterial *material, bool isExclusive,
                            PxU8 shapeFlags) {
    return (void *) physics->createShape(*geometry, *material, isExclusive, (PxShapeFlags) shapeFlags);
}

extern "C" void *
physx_PxPhysics_CreateRigidDynamic(PxPhysics *physics, PxTransform &transform) {
    return (void *) physics->createRigidDynamic(transform);
}

extern "C" void *
physx_PxPhysics_CreateRigidStatic(PxPhysics *physics, PxTransform &transform) {
    return (void *) physics->createRigidStatic(transform);
}

extern "C" bool
physx_PxPhysics_InitExtensions(PxPhysics *physics) {
    return PxInitExtensions(*physics, nullptr);
}