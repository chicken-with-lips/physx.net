cmake_minimum_required(VERSION 3.22)

project(PhysX.Native VERSION 1.0 LANGUAGES CXX)

set(PHYSX_ROOT_DIR "${CMAKE_SOURCE_DIR}/PhysX/physx")

set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)

if(CMAKE_CONFIGURATION_TYPES)
    set(CMAKE_CONFIGURATION_TYPES debug checked profile release)
    set(CMAKE_CONFIGURATION_TYPES "${CMAKE_CONFIGURATION_TYPES}" CACHE STRING "Reset config to what we need" FORCE)
endif()

if(WIN32)    
    set(TARGET_BUILD_PLATFORM "windows" CACHE INTERNAL "")
    set(PLATFORM "Windows")
    set(CMAKE_EXE_LINKER_FLAGS "/SUBSYSTEM:WINDOWS /DEBUG:FASTLINK")
    set(CMAKE_VS_PLATFORM_TOOLSET_HOST_ARCHITECTURE "x64")

    if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "MSVC")
        set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /std:c++17")

        if (USE_STATIC_MSVC_RUNTIME_LIBRARY)
            set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreaded$<$<CONFIG:Debug>:Debug>")
        else()
            set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreaded")
        endif()
    endif()
elseif(LINUX)
    set(TARGET_BUILD_PLATFORM "linux" CACHE INTERNAL "")
    set(PLATFORM "Linux")
    set(CMAKE_LIBRARY_ARCHITECTURE "x86_64-linux-gnu" CACHE INTERNAL "")
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++1y -Wall -Wextra -Wno-catch-value -Wno-unused-parameter -Wunused-but-set-variable -fPIC")
endif()

#link_directories("${PHYSX_ROOT_DIR}/physx/bin/linux.clang/${CMAKE_BUILD_TYPE}")
link_directories("${PHYSX_ROOT_DIR}/bin/win.x86_64.vc142.mt/${CMAKE_BUILD_TYPE}")

add_library(${PROJECT_NAME} SHARED
    src/PxActor.cpp
    src/PxCpuDispatcher.cpp
    src/PxDefaultAllocator.cpp
    src/PxDefaultCpuDispatcher.cpp
    src/PxDefaultErrorCallback.cpp
    src/PxDistanceJoint.cpp
    src/PxFixedJoint.cpp
    src/PxFoundation.cpp
    src/PxRigidActor.cpp
    src/PxRigidBody.cpp
    src/PxRigidBodyExt.cpp
    src/PxRigidDynamic.cpp
    src/PxScene.cpp
    src/PxSimulationEventCallback.cpp
    src/PxSimulationFilterCallback.cpp
    src/PxPvd.cpp
    src/PxOmniPvd.cpp
    src/PxPhysics.cpp
    src/PxPvdTransport.cpp
    src/PxVersion.cpp
)

target_compile_definitions(${PROJECT_NAME} PRIVATE NDEBUG)

include_directories(${PROJECT_NAME}
    PRIVATE include
    PRIVATE Physx/physx/include
)

target_link_libraries(${PROJECT_NAME} PUBLIC
    PhysX_static_64
    PhysXCharacterKinematic_static_64
    PhysXCommon_static_64
    PhysXExtensions_static_64
    PhysXFoundation_static_64
    PhysXCooking_static_64
    PhysXPvdSDK_static_64
    PhysXVehicle_static_64
    PhysXVehicle2_static_64
)
