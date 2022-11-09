cmake_minimum_required(VERSION 3.22)

project(PhysX.Native VERSION 1.0 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 14)
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++1y -Wall -Wextra -Wno-catch-value -Wno-unused-parameter -Wunused-but-set-variable -fPIC")
set(PHYSX_ROOT_DIR "${CMAKE_SOURCE_DIR}/../PhysX")

if (CMAKE_CONFIGURATION_TYPES)
    set(CMAKE_CONFIGURATION_TYPES debug release profile checked)
    set(CMAKE_CONFIGURATION_TYPES "${CMAKE_CONFIGURATION_TYPES}" CACHE STRING "Reset the configurations to what we need" FORCE)
    set(CMAKE_CXX_FLAGS_PROFILE "${CMAKE_CXX_FLAGS_RELEASE}")
    set(CMAKE_CXX_FLAGS_CHECKED "${CMAKE_CXX_FLAGS_RELEASE}")
endif ()

link_directories("${PHYSX_ROOT_DIR}/physx/bin/linux.clang/${CMAKE_BUILD_TYPE}")

add_library(${PROJECT_NAME} SHARED
    src/PxActor.cpp
    src/PxCpuDispatcher.cpp
    src/PxDefaultAllocator.cpp
    src/PxDefaultCpuDispatcher.cpp
    src/PxDefaultErrorCallback.cpp
    src/PxFoundation.cpp
    src/PxRigidActor.cpp
    src/PxRigidBody.cpp
    src/PxRigidBodyExt.cpp
    src/PxRigidDynamic.cpp
    src/PxRigidStatic.cpp
    src/PxScene.cpp
    src/PxSimulationEventCallback.cpp
    src/PxSimulationFilterCallback.cpp
    src/PxPhysics.cpp
    src/PxPvd.cpp
    src/PxPvdTransport.cpp
    src/PxVersion.cpp
)

add_definitions(-DNDEBUG)

include_directories(${PROJECT_NAME}
    PRIVATE "${PHYSX_ROOT_DIR}/physx/include"
)

target_link_libraries(${PROJECT_NAME}
    PhysX_static_64
    PhysXPvdSDK_static_64
    PhysXVehicle_static_64
    PhysXVehicle2_static_64
    PhysXCharacterKinematic_static_64
    PhysXExtensions_static_64
    PhysXCooking_static_64
    PhysXCommon_static_64
    PhysXFoundation_static_64
)

add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:${PROJECT_NAME}> /home/jolly_samurai/Projects/chicken-with-lips/duck/Build/Debug/net6.0/${PROJECT_NAME}.so)