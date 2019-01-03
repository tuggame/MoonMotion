﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

// Hand Locomotion
// • classifies this locomotion as a hand locomotion (a locomotion that functions individually for each hand, instead of functioning for the whole player at once)
//   · a hand locomotion is a particular kind of locomotive tool controlled by a hand (a child object of the hand that provides some method of locomotion to that hand's controller)
// • handles general functionality (such as automatic connections and tracking for either hand) for any kind of locomotive tool, be it a booster, skier, etc.
// • provides a dependencies combination setting by which to condition whether this hand locomotion is currently allowed
public abstract class HandLocomotion : Locomotion
{
	// variables //
	
	
	// variables for: general functionality //
	protected Controller controller;		// connection - automatic: the parent Controller
	protected Controller otherController;		// connection - automatic: the other controller
	protected Hand hand;		// connection - automatic: the parent Hand
	[HideInInspector] public bool leftHand;     // tracking: whether this locomotion is for the left hand (versus the right)

	// variables for: dependencies //
	[Header("Dependencies")]
	[Tooltip("the dependencies by which to condition whether this hand locomotion is allowed (perhaps just whether its input is allowed, or for more than that – such is up to the discretion of the particular hand locomotion)")]
	public Dependencies.DependenciesCombination locomotionDependencies;		// setting: the dependencies by which to condition whether this hand locomotion is allowed (perhaps just whether its input is allowed, or for more than that – such is up to the discretion of the particular hand locomotion)




	// updating //


	// before the start: //
	protected virtual void Awake()
	{
		// connect to the controller //
		controller = transform.parent.GetComponent<Controller>();

		// connect to the hand //
		hand = controller.GetComponent<Hand>();

		// track whether this locomotion is for the left hand //
		leftHand = (hand.startingHandType == Hand.HandType.Left);
	}

	// at the start: //
	protected virtual void Start()
	{
		// connect to the other controller //
		otherController = controller.otherController;
	}
}