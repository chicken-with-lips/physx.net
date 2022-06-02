#include "PxPhysics.h"
#include "PxScene.h"
#include "PxSimulationEventCallback.h"
#include <iostream>

using namespace physx;

typedef bool(*onStatusChange)(PxU32 &pairID, PxU16 &pairFlags, PxU16 &filterFlags);

class ProxySimulationFilterCallback : PxSimulationFilterCallback {
private:
    ::onStatusChange m_onStatusChange;

public:
    ProxySimulationFilterCallback(::onStatusChange onStatusChange) {
        m_onStatusChange = onStatusChange;
    }

private:
    PxFilterFlags
    pairFound(PxU32 pairID, PxFilterObjectAttributes attributes0, PxFilterData filterData0, const PxActor *a0,
              const PxShape *s0, PxFilterObjectAttributes attributes1, PxFilterData filterData1, const PxActor *a1,
              const PxShape *s1, PxPairFlags &pairFlags) override {
        std::cout << "pairFound";
        return physx::PxFilterFlags();
    }

    void pairLost(PxU32 pairID, PxFilterObjectAttributes attributes0, PxFilterData filterData0,
                  PxFilterObjectAttributes attributes1, PxFilterData filterData1, bool objectRemoved) override {
        std::cout << "pairLost";
    }

    bool statusChange(PxU32 &pairID, PxPairFlags &pairFlags, PxFilterFlags &filterFlags) override {
        PxU16 tmpPairFlags = pairFlags.operator uint16_t();
        PxU16 tmpFilterFlags = filterFlags.operator uint16_t();

        auto ret = (*m_onStatusChange)(pairID, tmpPairFlags, tmpFilterFlags);

        pairFlags = (PxPairFlags) tmpPairFlags;
        filterFlags = (PxFilterFlags) tmpFilterFlags;

        return ret;
    }
};

extern "C" void *physx_PxSimulationFilterCallback_New(::onStatusChange onStatusChange) {
    return new ProxySimulationFilterCallback(onStatusChange);
}
