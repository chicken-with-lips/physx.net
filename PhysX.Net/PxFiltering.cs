using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX;

public enum PxPairFilteringMode
{
    /// <summary>
    /// Output pair from BP, potentially send to user callbacks, create regular interaction object.
    /// </summary>
    /// <remarks>
    /// <para>Enable contact pair filtering between kinematic/static or kinematic/kinematic rigid bodies.</para>
    /// <para>By default contacts between these are suppressed (see #PxFilterFlag::eSUPPRESS) and don't get reported to
    /// the filter mechanism.</para>
    /// <para>Use this mode if these pairs should go through the filtering pipeline nonetheless.</para>
    /// <para>This mode is not mutable, and must be set in PxSceneDesc at scene creation.</para>
    /// </remarks>
    Keep,

    /// <summary>Output pair from BP, create interaction marker. Can be later switched to regular interaction.</summary>
    Suppress,

    /// <summary>Don't output pair from BP. Cannot be later switched to regular interaction, needs "resetFiltering" call.</summary>
    Kill,

    /// <summary>Default is eSUPPRESS for compatibility with previous PhysX versions.</summary>
    Default = Suppress,
}

/// <summary>
/// Collection of flags describing the filter actions to take for a collision pair.
/// </summary>
public enum PxFilterFlag : ushort
{
    /**
		\brief Ignore the collision pair as long as the bounding volumes of the pair objects overlap.

		Killed pairs will be ignored by the simulation and won't run through the filter again until one
		of the following occurs:

		\li The bounding volumes of the two objects overlap again (after being separated)
		\li The user enforces a re-filtering (see #PxScene::resetFiltering())

		@see PxScene::resetFiltering()
		*/
    Kill = (1 << 0),

    /**
		\brief Ignore the collision pair as long as the bounding volumes of the pair objects overlap or until filtering relevant data changes for one of the collision objects.

		Suppressed pairs will be ignored by the simulation and won't make another filter request until one
		of the following occurs:

		\li Same conditions as for killed pairs (see #eKILL)
		\li The filter data or the filter object attributes change for one of the collision objects

		@see PxFilterData PxFilterObjectAttributes
		*/
    Suppress = (1 << 1),

    /**
		\brief Invoke the filter callback (#PxSimulationFilterCallback::pairFound()) for this collision pair.

		@see PxSimulationFilterCallback
		*/
    Callback = (1 << 2),

    /**
		\brief Track this collision pair with the filter callback mechanism.

		When the bounding volumes of the collision pair lose contact, the filter callback #PxSimulationFilterCallback::pairLost()
		will be invoked. Furthermore, the filter status of the collision pair can be adjusted through #PxSimulationFilterCallback::statusChange()
		once per frame (until a pairLost() notification occurs).

		@see PxSimulationFilterCallback
		*/
    Notify = (1 << 3) | Callback,

    /**
		\brief Provided default to get standard behavior:

		The application configure the pair's collision properties once when bounding volume overlap is found and
		doesn't get asked again about that pair until overlap status or filter properties changes, or re-filtering is requested.

		No notification is provided when bounding volume overlap is lost

		The pair will not be killed or suppressed, so collision detection will be processed
		*/
    Default = 0
}

/// <summary>
/// Collection of flags describing the actions to take for a collision pair.
/// </summary>
[Flags]
public enum PxPairFlag : ushort
{
    /**
		\brief Process the contacts of this collision pair in the dynamics solver.

		\note Only takes effect if the colliding actors are rigid bodies.
		*/
    SolveContact = (1 << 0),

    /**
		\brief Call contact modification callback for this collision pair

		\note Only takes effect if the colliding actors are rigid bodies.

		@see PxContactModifyCallback
		*/
    ModifyContacts = (1 << 1),

    /**
		\brief Call contact report callback or trigger callback when this collision pair starts to be in contact.

		If one of the two collision objects is a trigger shape (see #PxShapeFlag::eTRIGGER_SHAPE) 
		then the trigger callback will get called as soon as the other object enters the trigger volume. 
		If none of the two collision objects is a trigger shape then the contact report callback will get 
		called when the actors of this collision pair start to be in contact.

		\note Only takes effect if the colliding actors are rigid bodies.

		\note Only takes effect if eDETECT_DISCRETE_CONTACT or eDETECT_CCD_CONTACT is raised

		@see PxSimulationEventCallback.onContact() PxSimulationEventCallback.onTrigger()
		*/
    NotifyTouchFound = (1 << 2),

    /**
		\brief Call contact report callback while this collision pair is in contact

		If none of the two collision objects is a trigger shape then the contact report callback will get 
		called while the actors of this collision pair are in contact.

		\note Triggers do not support this event. Persistent trigger contacts need to be tracked separately by observing eNOTIFY_TOUCH_FOUND/eNOTIFY_TOUCH_LOST events.

		\note Only takes effect if the colliding actors are rigid bodies.

		\note No report will get sent if the objects in contact are sleeping.

		\note Only takes effect if eDETECT_DISCRETE_CONTACT or eDETECT_CCD_CONTACT is raised

		\note If this flag gets enabled while a pair is in touch already, there will be no eNOTIFY_TOUCH_PERSISTS events until the pair loses and regains touch.

		@see PxSimulationEventCallback.onContact() PxSimulationEventCallback.onTrigger()
		*/
    NotifyTouchPersists = (1 << 3),

    /**
		\brief Call contact report callback or trigger callback when this collision pair stops to be in contact

		If one of the two collision objects is a trigger shape (see #PxShapeFlag::eTRIGGER_SHAPE) 
		then the trigger callback will get called as soon as the other object leaves the trigger volume. 
		If none of the two collision objects is a trigger shape then the contact report callback will get 
		called when the actors of this collision pair stop to be in contact.

		\note Only takes effect if the colliding actors are rigid bodies.

		\note This event will also get triggered if one of the colliding objects gets deleted.

		\note Only takes effect if eDETECT_DISCRETE_CONTACT or eDETECT_CCD_CONTACT is raised

		@see PxSimulationEventCallback.onContact() PxSimulationEventCallback.onTrigger()
		*/
    NotifyTouchLost = (1 << 4),

    /**
		\brief Call contact report callback when this collision pair is in contact during CCD passes.

		If CCD with multiple passes is enabled, then a fast moving object might bounce on and off the same
		object multiple times. Hence, the same pair might be in contact multiple times during a simulation step.
		This flag will make sure that all the detected collision during CCD will get reported. For performance
		reasons, the system can not always tell whether the contact pair lost touch in one of the previous CCD 
		passes and thus can also not always tell whether the contact is new or has persisted. eNOTIFY_TOUCH_CCD
		just reports when the two collision objects were detected as being in contact during a CCD pass.

		\note Only takes effect if the colliding actors are rigid bodies.

		\note Trigger shapes are not supported.

		\note Only takes effect if eDETECT_CCD_CONTACT is raised

		@see PxSimulationEventCallback.onContact() PxSimulationEventCallback.onTrigger()
		*/
    NotifyTouchCcd = (1 << 5),

    /**
		\brief Call contact report callback when the contact force between the actors of this collision pair exceeds one of the actor-defined force thresholds.

		\note Only takes effect if the colliding actors are rigid bodies.

		\note Only takes effect if eDETECT_DISCRETE_CONTACT or eDETECT_CCD_CONTACT is raised

		@see PxSimulationEventCallback.onContact()
		*/
    NotifyThresholdForceFound = (1 << 6),

    /**
		\brief Call contact report callback when the contact force between the actors of this collision pair continues to exceed one of the actor-defined force thresholds.

		\note Only takes effect if the colliding actors are rigid bodies.

		\note If a pair gets re-filtered and this flag has previously been disabled, then the report will not get fired in the same frame even if the force threshold has been reached in the
		previous one (unless #eNOTIFY_THRESHOLD_FORCE_FOUND has been set in the previous frame).

		\note Only takes effect if eDETECT_DISCRETE_CONTACT or eDETECT_CCD_CONTACT is raised

		@see PxSimulationEventCallback.onContact()
		*/
    NotifyThresholdForcePersists = (1 << 7),

    /**
		\brief Call contact report callback when the contact force between the actors of this collision pair falls below one of the actor-defined force thresholds (includes the case where this collision pair stops being in contact).

		\note Only takes effect if the colliding actors are rigid bodies.

		\note If a pair gets re-filtered and this flag has previously been disabled, then the report will not get fired in the same frame even if the force threshold has been reached in the
		previous one (unless #eNOTIFY_THRESHOLD_FORCE_FOUND or #eNOTIFY_THRESHOLD_FORCE_PERSISTS has been set in the previous frame).

		\note Only takes effect if eDETECT_DISCRETE_CONTACT or eDETECT_CCD_CONTACT is raised

		@see PxSimulationEventCallback.onContact()
		*/
    NotifyThresholdForceLost = (1 << 8),

    /**
		\brief Provide contact points in contact reports for this collision pair.

		\note Only takes effect if the colliding actors are rigid bodies and if used in combination with the flags eNOTIFY_TOUCH_... or eNOTIFY_THRESHOLD_FORCE_...

		\note Only takes effect if eDETECT_DISCRETE_CONTACT or eDETECT_CCD_CONTACT is raised

		@see PxSimulationEventCallback.onContact() PxContactPair PxContactPair.extractContacts()
		*/
    NotifyContactPoints = (1 << 9),

    /**
		\brief This flag is used to indicate whether this pair generates discrete collision detection contacts. 

		\note Contacts are only responded to if eSOLVE_CONTACT is enabled.
		*/
    DetectDiscreteContact = (1 << 10),

    /**
		\brief This flag is used to indicate whether this pair generates CCD contacts. 

		\note The contacts will only be responded to if eSOLVE_CONTACT is enabled on this pair.
		\note The scene must have PxSceneFlag::eENABLE_CCD enabled to use this feature.
		\note Non-static bodies of the pair should have PxRigidBodyFlag::eENABLE_CCD specified for this feature to work correctly.
		\note This flag is not supported with trigger shapes. However, CCD trigger events can be emulated using non-trigger shapes 
		and requesting eNOTIFY_TOUCH_FOUND and eNOTIFY_TOUCH_LOST and not raising eSOLVE_CONTACT on the pair.

		@see PxRigidBodyFlag::eENABLE_CCD
		@see PxSceneFlag::eENABLE_CCD
		*/
    DetectCcdContact = (1 << 11),

    /**
		\brief Provide pre solver velocities in contact reports for this collision pair.
		
		If the collision pair has contact reports enabled, the velocities of the rigid bodies before contacts have been solved
		will be provided in the contact report callback unless the pair lost touch in which case no data will be provided.
		
		\note Usually it is not necessary to request these velocities as they will be available by querying the velocity from the provided
		PxRigidActor object directly. However, it might be the case that the velocity of a rigid body gets set while the simulation is running
		in which case the PxRigidActor would return this new velocity in the contact report callback and not the velocity the simulation used.
		
		@see PxSimulationEventCallback.onContact(), PxContactPairVelocity, PxContactPairHeader.extraDataStream
		*/
    PreSolverVelocity = (1 << 12),

    /**
		\brief Provide post solver velocities in contact reports for this collision pair.
		
		If the collision pair has contact reports enabled, the velocities of the rigid bodies after contacts have been solved
		will be provided in the contact report callback unless the pair lost touch in which case no data will be provided.
		
		@see PxSimulationEventCallback.onContact(), PxContactPairVelocity, PxContactPairHeader.extraDataStream
		*/
    PostSolverVelocity = (1 << 13),

    /**
		\brief Provide rigid body poses in contact reports for this collision pair.
		
		If the collision pair has contact reports enabled, the rigid body poses at the contact event will be provided 
		in the contact report callback unless the pair lost touch in which case no data will be provided.
		
		\note Usually it is not necessary to request these poses as they will be available by querying the pose from the provided
		PxRigidActor object directly. However, it might be the case that the pose of a rigid body gets set while the simulation is running
		in which case the PxRigidActor would return this new pose in the contact report callback and not the pose the simulation used.
		Another use case is related to CCD with multiple passes enabled, A fast moving object might bounce on and off the same 
		object multiple times. This flag can be used to request the rigid body poses at the time of impact for each such collision event.
		
		@see PxSimulationEventCallback.onContact(), PxContactPairPose, PxContactPairHeader.extraDataStream
		*/
    ContactEventPose = (1 << 14),

    NextFree = (1 << 15), //!< For internal use only.

    /**
		\brief Provided default flag to do simple contact processing for this collision pair.
		*/
    ContactDefault = SolveContact | DetectDiscreteContact,

    /**
		\brief Provided default flag to get commonly used trigger behavior for this collision pair.
		*/
    TriggerDefault = NotifyTouchFound | NotifyTouchLost | DetectDiscreteContact
}

/// <summary>
/// PxFilterData is user-definable data which gets passed into the collision filtering shader and/or callback.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct PxFilterData
{
    public uint Word0;
    public uint Word1;
    public uint Word2;
    public uint Word3;
}

[Flags]
public enum PxFilterObjectFlag
{
    Kinematic = (1 << 4),
    Trigger = (1 << 5)
}

public static class PxFilterObject
{
    public static bool IsTrigger(PxFilterObjectFlag attr)
    {
        return (attr & PxFilterObjectFlag.Trigger) != 0;
    }

    public static bool IsKinematic(PxFilterObjectFlag attr)
    {
        return (attr & PxFilterObjectFlag.Kinematic) != 0;
    }
}
