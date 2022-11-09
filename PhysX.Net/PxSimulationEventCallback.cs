using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX;

/// <summary>
/// An interface class that the user can implement in order to receive simulation events.
/// </summary>
/// <remarks>
/// <para>With the exception of <see cref="OnAdvance"/>, the events get sent during the call to either
/// <see cref="PxScene.FetchResults"/> or #PxScene::flushSimulation() with sendPendingReports=true. onAdvance() gets
/// called while the simulation is running (that is between PxScene::simulate(), onAdvance()
/// and PxScene::fetchResults()).</para>
/// <para>SDK state should not be modified from within the callbacks. In particular objects should not be created or
/// destroyed. If state modification is needed then the changes should be stored to a buffer and performed after the
/// simulation step.</para>
/// <para>Threading: With the exception of onAdvance(), it is not necessary to make these callbacks thread safe as they
/// will only be called in the context of the user thread.</para>
/// </remarks>
public abstract class PxSimulationEventCallback : PxBase<PxSimulationEventCallback>
{
    private Native.PxSimulationEventCallback.OnContact _onContact;

    public PxSimulationEventCallback() : base(IntPtr.Zero)
    {
        _onContact = OnContactInternal;

        NativePtr = Native.PxSimulationEventCallback.Create(
            _onContact
        );

        ManuallyRegisterCache(NativePtr, this);
    }

    private void OnContactInternal(PxContactPairHeaderTransfer pairHeader, PxContactPairTransfer[] pairs, uint pairCount)
    {
	    var tmpPairHeader = new PxContactPairHeader(
            pairHeader
        );

        var tmpPairs = new PxContactPair[pairCount];

        for (var i = 0; i < pairCount; i++) {
            tmpPairs[i] = new PxContactPair(pairs[i]);
        }

        OnContact(tmpPairHeader, tmpPairs);
    }

    protected abstract void OnContact(PxContactPairHeader pairHeader, PxContactPair[] pairs);
}

/// <summary>
/// An Instance of this class is passed to PxSimulationEventCallback.onContact().
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct PxContactPairHeaderTransfer
{
    /**
	\brief The two actors of the notification shape pairs.

	\note The actor pointers might reference deleted actors. This will be the case if PxPairFlag::eNOTIFY_TOUCH_LOST
		  or PxPairFlag::eNOTIFY_THRESHOLD_FORCE_LOST events were requested for the pair and one of the involved actors 
		  gets deleted or removed from the scene. Check the #flags member to see whether that is the case.
		  Do not dereference a pointer to a deleted actor. The pointer to a deleted actor is only provided 
		  such that user data structures which might depend on the pointer value can be updated.

	@see PxActor
	*/
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public readonly IntPtr[] Actors;

    /**
	\brief Stream containing extra data as requested in the PxPairFlag flags of the simulation filter.

	This pointer is only valid if any kind of extra data information has been requested for the contact report pair (see #PxPairFlag::ePOST_SOLVER_VELOCITY etc.),
	else it will be NULL.
	
	@see PxPairFlag
	*/
    public readonly IntPtr ExtraDataStream;

    /**
	\brief Size of the extra data stream [bytes] 
	*/
    public readonly ushort ExtraDataStreamSize;

    /**
	\brief Additional information on the contact report pair.

	@see PxContactPairHeaderFlag
	*/
    public readonly PxContactPairHeaderFlag Flags;

    /**
	\brief pointer to the contact pairs
	*/
    public readonly IntPtr Pairs;

    /**
	\brief number of contact pairs
	*/
    public readonly uint NbPairs;
}

/// <summary>
/// An Instance of this class is passed to PxSimulationEventCallback.onContact().
/// </summary>
public readonly struct PxContactPairHeader
{
    /**
	\brief The two actors of the notification shape pairs.

	\note The actor pointers might reference deleted actors. This will be the case if PxPairFlag::eNOTIFY_TOUCH_LOST
		  or PxPairFlag::eNOTIFY_THRESHOLD_FORCE_LOST events were requested for the pair and one of the involved actors 
		  gets deleted or removed from the scene. Check the #flags member to see whether that is the case.
		  Do not dereference a pointer to a deleted actor. The pointer to a deleted actor is only provided 
		  such that user data structures which might depend on the pointer value can be updated.

	@see PxActor
	*/
    public readonly PxActor[] Actors;

    /**
	\brief Stream containing extra data as requested in the PxPairFlag flags of the simulation filter.

	This pointer is only valid if any kind of extra data information has been requested for the contact report pair (see #PxPairFlag::ePOST_SOLVER_VELOCITY etc.),
	else it will be NULL.
	
	@see PxPairFlag
	*/
    public readonly IntPtr ExtraDataStream;

    /**
	\brief Size of the extra data stream [bytes] 
	*/
    public readonly ushort ExtraDataStreamSize;

    /**
	\brief Additional information on the contact report pair.

	@see PxContactPairHeaderFlag
	*/
    public readonly PxContactPairHeaderFlag Flags;

    /**
	\brief pointer to the contact pairs
	*/
    public readonly PxContactPair[] Pairs;

    internal PxContactPairHeader(PxContactPairHeaderTransfer source)
    {
        Actors = new PxRigidActor[source.Actors.Length];
        Pairs = new PxContactPair[source.NbPairs];

        for (var i = 0; i < Actors.Length; i++) {
            Actors[i] = PxInstanceCache.Instance.Get<PxRigidActor>(source.Actors[i]);
        }

        for (var i = 0; i < Pairs.Length; i++) {
            // Pairs[i] = ;
        }

        ExtraDataStream = source.ExtraDataStream;
        ExtraDataStreamSize = source.ExtraDataStreamSize;
        Flags = source.Flags;
    }
};

/// <summary>
/// Collection of flags providing information on contact report pairs.
/// </summary>
[Flags]
public enum PxContactPairHeaderFlag : ushort
{
    /// <summary>The actor with index 0 has been removed from the scene.</summary>
    RemovedActor0 = (1 << 0),

    /// <summary>The actor with index 1 has been removed from the scene.</summary>
    RemovedActor1 = (1 << 1),
}

[StructLayout(LayoutKind.Sequential)]
internal readonly struct PxContactPairTransfer
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public readonly IntPtr[] Shapes;

    public readonly IntPtr ContactPatches;
    public readonly IntPtr ContactPoints;
    public readonly IntPtr ContactImpulses;
    public readonly uint RequiredBufferSize;
    public readonly byte ContactCount;
    public readonly byte PatchCount;
    public readonly ushort ContactStreamSize;
    public readonly PxContactPairFlag Flags;
    public readonly PxPairFlag Events;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public readonly uint[] InternalData;
}

/**
\brief Contact report pair information.

Instances of this class are passed to PxSimulationEventCallback.onContact(). If contact reports have been requested for a pair of shapes (see #PxPairFlag),
then the corresponding contact information will be provided through this structure.

@see PxSimulationEventCallback.onContact()
*/
public readonly struct PxContactPair
{
    /**
	\brief The two shapes that make up the pair.

	\note The shape pointers might reference deleted shapes. This will be the case if #PxPairFlag::eNOTIFY_TOUCH_LOST
		  or #PxPairFlag::eNOTIFY_THRESHOLD_FORCE_LOST events were requested for the pair and one of the involved shapes 
		  gets deleted. Check the #flags member to see whether that is the case. Do not dereference a pointer to a 
		  deleted shape. The pointer to a deleted shape is only provided such that user data structures which might 
		  depend on the pointer value can be updated.

	@see PxShape
	*/
    public readonly PxShape[] Shapes;

    /**
	\brief Pointer to first patch header in contact stream containing contact patch data

	This pointer is only valid if contact point information has been requested for the contact report pair (see #PxPairFlag::eNOTIFY_CONTACT_POINTS).
	Use #extractContacts() as a reference for the data layout of the stream.
	*/
    public readonly IntPtr ContactPatches;

    /**
	\brief Pointer to first contact point in contact stream containing contact data

	This pointer is only valid if contact point information has been requested for the contact report pair (see #PxPairFlag::eNOTIFY_CONTACT_POINTS).
	Use #extractContacts() as a reference for the data layout of the stream.
	*/
    public readonly IntPtr ContactPoints;

    /**
	\brief Buffer containing applied impulse data.

	This pointer is only valid if contact point information has been requested for the contact report pair (see #PxPairFlag::eNOTIFY_CONTACT_POINTS).
	Use #extractContacts() as a reference for the data layout of the stream.
	*/
    public readonly IntPtr ContactImpulses;

    /**
	\brief Size of the contact stream [bytes] including force buffer
	*/
    public readonly uint RequiredBufferSize;

    /**
	\brief Number of contact points stored in the contact stream
	*/
    public readonly byte ContactCount;

    /**
	\brief Number of contact patches stored in the contact stream
	*/
    public readonly byte PatchCount;

    /**
	\brief Size of the contact stream [bytes] not including force buffer
	*/
    public readonly ushort ContactStreamSize;

    /**
	\brief Additional information on the contact report pair.

	@see PxContactPairFlag
	*/
    public readonly PxContactPairFlag Flags;

    /**
	\brief Flags raised due to the contact.

	The events field is a combination of:

	<ul>
	<li>PxPairFlag::eNOTIFY_TOUCH_FOUND,</li>
	<li>PxPairFlag::eNOTIFY_TOUCH_PERSISTS,</li>
	<li>PxPairFlag::eNOTIFY_TOUCH_LOST,</li>
	<li>PxPairFlag::eNOTIFY_TOUCH_CCD,</li>
	<li>PxPairFlag::eNOTIFY_THRESHOLD_FORCE_FOUND,</li>
	<li>PxPairFlag::eNOTIFY_THRESHOLD_FORCE_PERSISTS,</li>
	<li>PxPairFlag::eNOTIFY_THRESHOLD_FORCE_LOST</li>
	</ul>

	See the documentation of #PxPairFlag for an explanation of each.

	\note eNOTIFY_TOUCH_CCD can get raised even if the pair did not request this event. However, in such a case it will only get
	raised in combination with one of the other flags to point out that the other event occured during a CCD pass.

	@see PxPairFlag
	*/
    public readonly PxPairFlag Events;

    public readonly uint[] InternalData; // For internal use only

    internal PxContactPair(PxContactPairTransfer source)
    {
        Shapes = new PxShape[source.Shapes.Length];

        for (var i = 0; i < Shapes.Length; i++) {
            Shapes[i] = PxInstanceCache.Instance.Get<PxShape>(source.Shapes[i]);
        }

        ContactPatches = source.ContactPatches;
        ContactPoints = source.ContactPoints;
        ContactImpulses = source.ContactImpulses;
        RequiredBufferSize = source.RequiredBufferSize;
        ContactCount = source.ContactCount;
        PatchCount = source.PatchCount;
        ContactStreamSize = source.ContactStreamSize;
        Flags = source.Flags;
        Events = source.Events;
        InternalData = source.InternalData;
    }
};

/// <summary>
/// Collection of flags providing information on contact report pairs.
/// </summary>
[Flags]
public enum PxContactPairFlag : ushort
{
    /**
		\brief The shape with index 0 has been removed from the actor/scene.
		*/
    RemovedShape0 = (1 << 0),

    /**
		\brief The shape with index 1 has been removed from the actor/scene.
		*/
    RemovedShape1 = (1 << 1),

    /**
		\brief First actor pair contact.

		The provided shape pair marks the first contact between the two actors, no other shape pair has been touching prior to the current simulation frame.

		\note: This info is only available if #PxPairFlag::eNOTIFY_TOUCH_FOUND has been declared for the pair.
		*/
    ActorPairHasFirstTouch = (1 << 2),

    /**
		\brief All contact between the actor pair was lost.

		All contact between the two actors has been lost, no shape pairs remain touching after the current simulation frame.
		*/
    ActorPairLostTouch = (1 << 3),

    /**
		\brief Internal flag, used by #PxContactPair.extractContacts()

		The applied contact impulses are provided for every contact point. 
		This is the case if #PxPairFlag::eSOLVE_CONTACT has been set for the pair.
		*/
    InternalHasImpulses = (1 << 4),

    /**
		\brief Internal flag, used by #PxContactPair.extractContacts()

		The provided contact point information is flipped with regards to the shapes of the contact pair. This mainly concerns the order of the internal triangle indices.
		*/
    InternalContactsAreFlipped = (1 << 5)
}
