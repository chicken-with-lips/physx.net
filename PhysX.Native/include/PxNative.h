#pragma once

#include "PxConfig.h"

#if defined(_MSC_VER)
#    define _PHYSX_EXPORT __declspec(dllexport)
#elif defined(__GNUC__)
#    define _PHYSX_EXPORT __attribute__((visibility("default")))
#else
#    define _PHYSX_EXPORT
#    pragma warning "Unknown dynamic link import/export semantics."
#endif

#ifdef __cplusplus
#    define _PHYSX_EXTERN extern "C"
#else
#    define _PHYSX_EXTERN extern
#endif

#define PHYSX_CAPI _PHYSX_EXTERN _PHYSX_EXPORT
