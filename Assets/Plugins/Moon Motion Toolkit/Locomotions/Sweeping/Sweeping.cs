﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Valve.VR.InteractionSystem;

// Sweeping:
// • provides the player with the sweeping locomotion
public class Sweeping : SingletonBehaviour<Sweeping>, ILocomotion
{
	#region variables


	#region control

	[BoxGroup("Control")]
	[Tooltip("the controller operations by which to sweep")]
	[ReorderableList]
	public ControllerOperation[] operations;
	
	[BoxGroup("Control")]
	[Tooltip("whether sweeping can be canceled via the operation")]
	public bool cancelable = true;
	#endregion control


	#region sweeping
	
	// the potential dashing position of the controller the player is currently sweeping via //
	public static Vector3? potentialControllerSweepingPosition = null;
	// the dashing position of the controller the player is currently sweeping via (assuming the player is sweeping) //
	public static Vector3 controllerSweepingPosition => potentialControllerSweepingPosition.GetValueOrDefault();

	// the (potential) sweep starting position //
	public static Vector3? potentialSweepStartingPosition = null;
	// the sweep starting position the player is currently sweeping from (assuming there is one) //
	public static Vector3 sweepStartingPosition => potentialSweepStartingPosition.GetValueOrDefault();
	
	// the potential target position the player is currently dashing to //
	public static Vector3? potentialCurrentTargetPosition = null;
	// the target position the player is currently dashing to (assuming there is one) //
	public static Vector3 currentTargetPosition => potentialCurrentTargetPosition.GetValueOrDefault();
	
	// whether the player is currently sweeping //
	public static bool currentlySweeping => potentialCurrentTargetPosition.isYull();

	[BoxGroup("Sweeping")]
	[Tooltip("the distance away from the player's body to sweep to")]
	public float sweepDistance = 3.6f;

	[BoxGroup("Sweeping")]
	[Tooltip("the threshold distance for the player's body to be within the target position to end the sweep")]
	public float endingThresholdDistance = Default.thresholdDistance;

	[BoxGroup("Sweeping")]
	[Tooltip("whether to end the sweep if farther from the sweep starting position than the target position is (in the same direction)")]
	public bool endIfOverswept = true;

	[BoxGroup("Sweeping")]
	[Tooltip("a layer mask by which to end sweeps before they reach their target position")]
	public LayerMask sweepEndingLayerMask = Default.layerMask;
	
	[BoxGroup("Sweeping")]
	[Tooltip("the magnitude of the sweeping force")]
	public float forceMagnitude = 28f;
	
	[BoxGroup("Sweeping")]
	[Tooltip("whether to enable skiing upon starting a sweep and to disable skiing upon ending a sweep")]
	public bool togglesSkiing = true;
	#endregion sweeping
	#endregion variables




	#region methods


	// method: stop the current sweep, if any //
	private void stopSweep()
	{
		if (togglesSkiing)
		{
			Skier.disableSkiing();
		}
		
		MoonMotionPlayer.zeroVelocities();

		potentialCurrentTargetPosition = null;
	}
	
	// method: begin a sweep via the given sweeping controller //
	private void beginSweepVia(Controller sweepingController)
	{
		SkiingSettings.singleton.heldVersusToggled = false;

		stopSweep();
		
		potentialControllerSweepingPosition = sweepingController.position();
		potentialSweepStartingPosition = MoonMotionBody.position;
		potentialCurrentTargetPosition = sweepingController.position.positionAlong(sweepingController.forward().withVectralYZeroed(), sweepDistance);

		if (togglesSkiing)
		{
			Skier.enableSkiing();
		}
	}
	#endregion methods




	#region updating


	// at each physics update: //
	private void FixedUpdate()
	{
		if (operations.operated() && !currentlySweeping)
		{
			beginSweepVia(operations.firstOperatedControllerOtherwiseFallback());
		}
		else if
		(
			currentlySweeping &&
			(
				(operations.operated() && cancelable) ||

				MoonMotionBody.isWithinDistanceOf(currentTargetPosition, endingThresholdDistance) ||

				endIfOverswept.and(MoonMotionBody.isMoreDistantInSameDirectionalityAs(currentTargetPosition, sweepStartingPosition)) ||
				
				MoonMotionPlayer.isCollidedWith(sweepEndingLayerMask)
			)
		)
		{
			stopSweep();
		}

		if (currentlySweeping)
		{
			currentTargetPosition.attractAsThoughMistargeting
			(
				controllerSweepingPosition,
				MoonMotionPlayer.ensuredCorrespondingRigidbody,
				forceMagnitude
			);
		}
	}
	#endregion updating
}