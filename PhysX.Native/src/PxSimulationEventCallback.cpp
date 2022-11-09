#include "PxPhysics.h"
#include "PxScene.h"
#include "PxSimulationEventCallback.h"
#include <iostream>

using namespace physx;

struct PxContactPairHeaderTransfer {
public:
    PxActor *actors[2];
    const PxU8 *extraDataStream;
    PxU16 extraDataStreamSize;
    PxU16 flags;
    const struct PxContactPair *pairs;
    PxU32 nbPairs;

    void CopyFrom(const PxContactPairHeader &source) {
        actors[0] = source.actors[0];
        actors[1] = source.actors[1];
        extraDataStream = source.extraDataStream;
        extraDataStreamSize = source.extraDataStreamSize;
        flags = source.flags;
        pairs = source.pairs;
        nbPairs = source.nbPairs;
    }
};

struct PxContactPairTransfer {
public:
    PxShape *shapes[2];
    const PxU8 *contactPatches;
    const PxU8 *contactPoints;
    const PxReal *contactImpulses;
    PxU32 requiredBufferSize;
    PxU8 contactCount;
    PxU8 patchCount;
    PxU16 contactStreamSize;

    PxU16 flags;
    PxU16 events;

    PxU32 internalData[2];    // For internal use only

    void CopyFrom(const PxContactPair source) {
        shapes[0] = source.shapes[0];
        shapes[1] = source.shapes[1];
        contactPatches = source.contactPatches;
        contactPoints = source.contactPoints;
        contactImpulses = source.contactImpulses;
        requiredBufferSize = source.requiredBufferSize;
        contactCount = source.contactCount;
        patchCount = source.patchCount;
        contactStreamSize = source.contactStreamSize;

        flags = source.flags;
        events = source.events;

        internalData[0] = source.internalData[0];    // For internal use only
        internalData[1] = source.internalData[1];    // For internal use only
    }
};

typedef void *(*onContact)(PxContactPairHeaderTransfer pairHeader, PxContactPairTransfer &pairs, PxU32 nbPairs);

class ProxySimulationEventCallback : PxSimulationEventCallback {
private:
    ::onContact m_onContact;

public:
    ProxySimulationEventCallback(::onContact onContact) {
        m_onContact = onContact;
    }

    void onConstraintBreak(PxConstraintInfo *constraints, PxU32 count) override {
        std::cout << "OnConstraintBreak\n";
    }

    void onWake(PxActor **actors, PxU32 count) override {
        std::cout << "OnWake\n";
    }

    void onSleep(PxActor **actors, PxU32 count) override {
        std::cout << "OnSleep\n";
    }

    void onContact(const PxContactPairHeader &pairHeader, const PxContactPair *pairs, PxU32 nbPairs) override {
        PxContactPairHeaderTransfer pairHeaderTransfer{};
        pairHeaderTransfer.CopyFrom(pairHeader);
        pairHeaderTransfer.flags = PxContactPairHeaderFlag::Enum::eREMOVED_ACTOR_1;

        auto *pairsTransfer = new PxContactPairTransfer[nbPairs];

        for (PxU32 i = 0; i < nbPairs; i++) {
            pairsTransfer[i].CopyFrom(pairs[i]);
        }

        (*m_onContact)(pairHeaderTransfer, *pairsTransfer, nbPairs);

        delete[] pairsTransfer;
    }

    void onTrigger(PxTriggerPair *pairs, PxU32 count) override {

    }

    void onAdvance(const PxRigidBody *const *bodyBuffer, const PxTransform *poseBuffer, const PxU32 count) override {

    }
};

extern "C" void *physx_PxSimulationEventCallback_New(::onContact onContact) {
    return new ProxySimulationEventCallback(onContact);
}
