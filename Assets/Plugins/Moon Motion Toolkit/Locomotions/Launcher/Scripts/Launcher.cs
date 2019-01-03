﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Launcher
// • classifies this locomotion as the "launching" locomotion; classifies this object as a "launcher"
// • launches (applies a launching force to) rigidbodies entering its collider
//   · only launches objects of one of the set recognized layers
//   · a setting determines the launching force amount
//   · for consistency, before launching, zeroes the rigidbody's velocity and angular velocity
//   · plays the attached launching audio
// • player launching time determination:
//   · tracks and provides the last time the player was launched
//   · provides a method for determining whether the player was launched within a given amount of time ago
public class Launcher : Locomotion
{
	// variables //

	
	// variables for: launching //
	public string[] recognizedLayers = new string[] {"Player", "Default"};		// setting: the recognized layers that objects must have one of to be launched
	public float launchingForce = 30f;		// setting: the force amount by which to launch objects

	// variables for: playing launching audio //
	private AudioSource audioSource;		// connection - automatic: the attached launching audio source
	private new AudioClip audio;        // connection - automatic: the attached launching audio

	// variables for: player launching time determination //
	public static float lastTimePlayerLaunched = -1f;		// tracking: the last time that the player was launched – initialized to -1 as a flag that the player has never been launched
	
	
	
	
	// methods //

	
	// methods for: player launching time determination //

	
	// method: determine whether the player was launched within the given amount of time ago //
	public static bool playerLastLaunchedWithin(float timeAgo)
	{
		return ((Time.time - lastTimePlayerLaunched) <= timeAgo);
	}
	
	
	
	
	// updating //

	
	// before the start: //
	private void Awake()
	{
		// connect to the attached launching audio source and audio //
		audioSource = GetComponent<AudioSource>();
		audio = audioSource.clip;
	}

	// upon trigger entry: //
	private void OnTriggerEnter(Collider collider)
	{
		Rigidbody collidedRigidbody = Hierarchy.selfOrAnyLevelParentWithRigidbody(collider).GetComponent<Rigidbody>();
		
		if (collidedRigidbody)
		{
			string rigidbodyLayer = LayerMask.LayerToName(collidedRigidbody.gameObject.layer);
			bool layerRecognized = false;
			foreach (string recognizedLayer in recognizedLayers)
			{
				if (rigidbodyLayer.Equals(recognizedLayer))
				{
					layerRecognized = true;
				}
			}
			
			if (layerRecognized)
			{
				collidedRigidbody.velocity = Vector3.zero;
				collidedRigidbody.angularVelocity = Vector3.zero;

				Vector3 forceDirection = Rotation.vectorFor(Rotation.Direction.up, transform);
				Vector3 newVelocity = (launchingForce * forceDirection);
				collidedRigidbody.velocity = newVelocity;

				audioSource.PlayOneShot(audio);

				if (rigidbodyLayer.Equals("Player"))
				{
					lastTimePlayerLaunched = Time.time;
				}
			}
		}
	}
}