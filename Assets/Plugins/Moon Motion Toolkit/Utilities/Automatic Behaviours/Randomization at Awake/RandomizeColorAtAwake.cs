﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

// Randomize Color At Awake
public class RandomizeColorAtAwake : AutomaticBehaviour<RandomizeColorAtAwake>
{
	// updating //

	
	// before the start: //
	private void Awake()
		=> randomizeColor();
}