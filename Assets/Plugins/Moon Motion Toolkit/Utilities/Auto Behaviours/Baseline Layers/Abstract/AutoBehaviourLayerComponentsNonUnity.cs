﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if NAV_MESH_COMPONENTS
using UnityEngine.AI;
#endif
#if STEAM_VIRTUALITY
using Valve.VR.InteractionSystem;
#endif

// Auto Behaviour Layer Components Moon Motion:
// #auto #tracking
// • provides this behaviour with automatically-connected properties to its game object's nonUnity components and typical state of (and so on) those components
public abstract class	AutoBehaviourLayerComponentsNonUnity<AutoBehaviourT> :
					AutoBehaviourLayerComponentsUnity<AutoBehaviourT>
						where AutoBehaviourT : AutoBehaviour<AutoBehaviourT>
{
	#region Moon Motion Toolkit
	#if MOON_MOTION_TOOLKIT
	public TerrainResponse terrainResponse => cache<TerrainResponse>();
	public Powerup powerup => cache<Powerup>();
	public PowerupCollider powerupCollider => cache<PowerupCollider>();
	public PowerupObjectsToggler powerupObjectsToggler => cache<PowerupObjectsToggler>();
	public PowerupInstanceMethodsCaller powerupInstanceMethodsCaller => cache<PowerupInstanceMethodsCaller>();
	public StretchScalable stretchScalable => cache<StretchScalable>();
	#endif
	#endregion Moon Motion Toolkit








	#region Utilities




	#region Auto Behaviours


	public DefaultAutoBehaviour defaultAutoBehaviour => cache<DefaultAutoBehaviour>();
	public DefaultAutoBehaviour ensuredDefaultAutoBehaviour => cache<DefaultAutoBehaviour>(true);
	public Kinematizer kinematizer => cache<Kinematizer>();
	public Slower slower => cache<Slower>();
	public FaceMainCamera faceMainCamera => cache<FaceMainCamera>();
	public Hovering hovering => cache<Hovering>();
	public BasicSpinning basicSpinning => cache<BasicSpinning>();


	#region Trackings

	#region miscellaneous
	public TrackTimeOfAwake trackTimeOfAwake => cache<TrackTimeOfAwake>();
	public TrackLightIntensityAtAwake trackLightIntensityAtAwake => cache<TrackLightIntensityAtAwake>();
	public TrackAwake trackAwake => cache<TrackAwake>();
	public TrackColorAtAwake trackColorAtAwake => cache<TrackColorAtAwake>();
	public TrackStart trackStart => cache<TrackStart>();
	#endregion miscellaneous

	#region Collideds
	public TrackCollideds trackCollideds => cache<TrackCollideds>();
	#endregion Collideds

	#region Components
	public TrackCapsuleCollidersAtAwake trackCapsuleCollidersAtAwake => cache<TrackCapsuleCollidersAtAwake>();
	#endregion Components

	#region Transformations
	public TrackLocalScaleAtAwake trackLocalScaleAtAwake => cache<TrackLocalScaleAtAwake>();
	public TrackLocalScaleXAtAwake trackLocalScaleXAtAwake => cache<TrackLocalScaleXAtAwake>();
	public TrackLocalScaleYAtAwake trackLocalScaleYAtAwake => cache<TrackLocalScaleYAtAwake>();
	public TrackLocalScaleZAtAwake trackLocalScaleZAtAwake => cache<TrackLocalScaleZAtAwake>();
	public TrackPositionAtAwake trackPositionAtAwake => cache<TrackPositionAtAwake>();
	public TrackPositionXAtAwake trackPositionXAtAwake => cache<TrackPositionXAtAwake>();
	public TrackPositionYAtAwake trackPositionYAtAwake => cache<TrackPositionYAtAwake>();
	public TrackPositionZAtAwake trackPositionZAtAwake => cache<TrackPositionZAtAwake>();
	public TrackRotationAtAwake trackRotationAtAwake => cache<TrackRotationAtAwake>();
	public TrackLocalPositionZAtAwake trackLocalPositionZAtAwake => cache<TrackLocalPositionZAtAwake>();
	public TrackLocalPositionYAtAwake trackLocalPositionYAtAwake => cache<TrackLocalPositionYAtAwake>();
	public TrackEulerAngleYAtAwake trackEulerAngleYAtAwake => cache<TrackEulerAngleYAtAwake>();
	public TrackLocalPositionXAtAwake trackLocalPositionXAtAwake => cache<TrackLocalPositionXAtAwake>();
	public TrackLocalPositionAtAwake trackLocalPositionAtAwake => cache<TrackLocalPositionAtAwake>();
	public TrackLocalEulerAngleZAtAwake trackLocalEulerAngleZAtAwake => cache<TrackLocalEulerAngleZAtAwake>();
	public TrackLocalEulerAngleYAtAwake trackLocalEulerAngleYAtAwake => cache<TrackLocalEulerAngleYAtAwake>();
	public TrackLocalEulerAngleXAtAwake trackLocalEulerAngleXAtAwake => cache<TrackLocalEulerAngleXAtAwake>();
	public TrackLocalEulerAnglesAtAwake trackLocalEulerAnglesAtAwake => cache<TrackLocalEulerAnglesAtAwake>();
	public TrackEulerAngleZAtAwake trackEulerAngleZAtAwake => cache<TrackEulerAngleZAtAwake>();
	public TrackEulerAngleXAtAwake trackEulerAngleXAtAwake => cache<TrackEulerAngleXAtAwake>();
	public TrackEulerAnglesAtAwake trackEulerAnglesAtAwake => cache<TrackEulerAnglesAtAwake>();
	public TrackLocalRotationAtAwake trackLocalRotationAtAwake => cache<TrackLocalRotationAtAwake>();
	#endregion Transformations
	#endregion Trackings
	#endregion Auto Behaviours
	#endregion Utilities









	#region Navmesh Components
	#if NAV_MESH_COMPONENTS
	public NavMeshAgent navmeshAgent => cache<NavMeshAgent>();
	public Bipedation bipedation => cache<Bipedation>();
	#endif
	#endregion Navmesh Components









	#region Unitology
	#if UNITOLOGY
	public Unit unit => cache<Unit>();
	#endif
	#endregion Unitology









	#region Steam Virtuality
	#if STEAM_VIRTUALITY
	public Player player => cache<Player>();
	public Hand hand => cache<Hand>();
	public Interactable interactable => cache<Interactable>();
	public Throwable throwable => cache<Throwable>();
	public VelocityEstimator velocityEstimator => cache<VelocityEstimator>();
	public ComplexThrowable complexThrowable => cache<ComplexThrowable>();
	public Arrow arrow => cache<Arrow>();
	public ArrowHand arrowHand => cache<ArrowHand>();
	public Balloon balloon => cache<Balloon>();
	public Longbow longbow => cache<Longbow>();
	public ItemPackage itemPackage => cache<ItemPackage>();
	#endif
	#endregion Steam Virtuality
}