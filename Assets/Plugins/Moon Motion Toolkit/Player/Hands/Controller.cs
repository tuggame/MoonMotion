﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;

// Controller
// • provides connections to the left and right instances of Controller
// • enumerates controller handednesses, inputs, inputtednesses
// • tracks the handedness of this controller (whether this controller is for the left hand (versus the right))
// • provides methods for determining input status of this controller, to:
//   · determine status for given inputs andor inputtednesses andor beingnesses (altogether, an "operation" as defined by Controller Operation)
//   · track touchpad touch positions and motion, including methods for determining touchpad touch motions and controlling their tracking
//   · determine whether a given array of inputs has any actual (non-none) inputs set
// • provides methods for vibrating this controller
// • provides methods for determining whether given operations are currently operated and those controllers operated
// ∗: example usage of touchpad travelling input to flip pages of a book
[RequireComponent(typeof(Hand))]
public class Controller : MonoBehaviour
{
	// enumerations //
	
	
	// enumeration of: controller handedness //
	public enum Handedness
	{
		neither,
		either,
		one,
		left,
		right,
		both,
		infinite
	}

	// enumeration of: controller inputs //
	public enum Input
	{
		none,
		trigger,
		touchpad,
		menuButton,
		grip
	}

	// enumeration of: controller inputtedness //
	public enum Inputtedness
	{
		touch,
		shallow,
		deep,
		press
	}
















	// variables //


	// variables for: connection with this hand and the other controller //
	[HideInInspector] public Hand hand;		// connection - automatic: this controller's hand
    [HideInInspector] public Controller otherController;		// connection - automatic: the other controller
	
	// variables for: instancing //
	public static Controller left, right;		// connections - automatic: the left and right controller instances, respectively
	[HideInInspector] public bool leftInstance = true;		// tracking: this controller's handedness

	// variables for: touchpad input handling //
	private float touchpadTouchdownTime = -1f;		// tracking: the time of last touchdown for the touchpad
	private float touchpadTouchdownX = 0f, touchpadTouchdownY = 0f;		// tracking: the coordinates (x and y, respectively) of the last touchdown for the touchpad
	private float previousTouchpadX = 0f, previousTouchpadY = 0f;       // tracking: the touchpad's previously touched x and y coordinates
	private float touchpadXDerivative = 0f, touchpadYDerivative = 0f;		// tracking: the touchpad's change in touched coordinate (for x and y, respectively) from the last frame
	private float previousTouchpadXDerivative = 0f, previousTouchpadYDerivative = 0f;		// tracking: the previous derivative for each coordinate (x or y, respectively) – used to determine the derivative derivative (which in turn implies a change in touchpad movement direction)
	private bool touchpadXDerivativeDerivativeNonzero = false, touchpadYDerivativeDerivativeNonzero = false;        // tracking: whether the touchpad's change in change in touched coordinate (for x and y, respectively) from the last frame is nonzero (implying a change in touchpad touch movement direction) versus zero
















	// methods //








	// detecting of particular inputs, inputtedness, and beingness //


	// detection - trigger //

	public bool triggerTouching()
	{
		return false;
	}
	public bool triggerTouched()
	{
		return false;
	}
	public bool triggerUntouching()
	{
		return false;
	}

	public bool triggerShallowing()
	{
		if (triggerPressed())
		{
			return (triggerPressing() || triggerUndeeping());
		}
		return false;
	}
	public bool triggerShallowed()
	{
		return (triggerPressed() && !triggerDeeped());
	}
	public bool triggerUnshallowing()
	{
		return (triggerUnpressing() || triggerDeeping());
	}

	public bool triggerDeeping()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
	}
	public bool triggerDeeped()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger);
	}
	public bool triggerUndeeping()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
	}

	public bool triggerPressing()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger);
	}
	public bool triggerPressed()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetTouch(SteamVR_Controller.ButtonMask.Trigger);
	}
	public bool triggerUnpressing()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger);
	}


	// detection - touchpad //

	public bool touchpadTouching()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad);
	}
	public bool touchpadTouched()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad);
	}
	public bool touchpadUntouching()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad);
	}

	public bool touchpadShallowing()
	{
		return touchpadPressing();
	}
	public bool touchpadShallowed()
	{
		return touchpadPressed();
	}
	public bool touchpadUnshallowing()
	{
		return touchpadUnpressing();
	}

	public bool touchpadDeeping()
	{
		return touchpadPressing();
	}
	public bool touchpadDeeped()
	{
		return touchpadPressed();
	}
	public bool touchpadUndeeping()
	{
		return touchpadUnpressing();
	}

	public bool touchpadPressing()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad);
	}
	public bool touchpadPressed()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
	}
	public bool touchpadUnpressing()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad);
	}

	public float touchpadX()
	{
		if (hand.controller == null)
		{
			return 0f;
		}
		Vector2 coordinates = hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
		return coordinates.x;
	}
	public float touchpadY()
	{
		if (hand.controller == null)
		{
			return 0f;
		}
		Vector2 coordinates = hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
		return coordinates.y;
	}
	public float touchpadDistance()
	{
		if (hand.controller == null)
		{
			return -1f;
		}
		Vector2 coordinates = hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
		return Vector2.Distance(coordinates, (new Vector2(0f, 0f)));
	}
	public void resetTouchpadTouchdown()
	{
		if (touchpadTouched())
		{
			touchpadTouchdownTime = Time.time;
			touchpadTouchdownX = touchpadX();
			touchpadTouchdownY = touchpadY();
		}
	}
	public void trackTouchpadTouching()
	{
		if (touchpadTouching())
		{
			resetTouchpadTouchdown();
		}
		else if (touchpadTouched())
		{
			previousTouchpadXDerivative = touchpadXDerivative;
			previousTouchpadYDerivative = touchpadYDerivative;

			touchpadXDerivative = (touchpadX() - previousTouchpadX);
			touchpadYDerivative = (touchpadY() - previousTouchpadY);

			touchpadXDerivativeDerivativeNonzero = ((touchpadXDerivative > 0f) != (previousTouchpadXDerivative > 0f));
			touchpadYDerivativeDerivativeNonzero = ((touchpadYDerivative > 0f) != (previousTouchpadYDerivative > 0f));
		}
	}
	private float touchpadXTravel(bool derivativeChangeResets)
	{
		if ((hand.controller == null) || (touchpadTouchdownTime == -1f) || !touchpadTouched())
		{
			return 0f;
		}
		else
		{
			if (derivativeChangeResets && touchpadXDerivativeDerivativeNonzero)
			{
				resetTouchpadTouchdown();
			}
			return (touchpadX() - touchpadTouchdownX);
		}
	}
	public float touchpadXTravelDirectional()
	{
		return touchpadXTravel(true);
	}
	public float touchpadXTravelDirectionless()
	{
		return touchpadXTravel(false);
	}
	private float touchpadYTravel(bool derivativeChangeResets)
	{
		if ((hand.controller == null) || (touchpadTouchdownTime == -1f) || !touchpadTouched())
		{
			return 0f;
		}
		else
		{
			if (derivativeChangeResets && touchpadYDerivativeDerivativeNonzero)
			{
				resetTouchpadTouchdown();
			}
			return (touchpadY() - touchpadTouchdownY);
		}
	}
	public float touchpadYTravelDirectional()
	{
		return touchpadYTravel(true);
	}
	public float touchpadYTravelDirectionless()
	{
		return touchpadYTravel(false);
	}


	// detection - menu button //

	public bool menuButtonTouching()
	{
		return false;
	}
	public bool menuButtonTouched()
	{
		return false;
	}
	public bool menuButtonUntouching()
	{
		return false;
	}

	public bool menuButtonShallowing()
	{
		return menuButtonPressing();
	}
	public bool menuButtonShallowed()
	{
		return menuButtonPressed();
	}
	public bool menuButtonUnshallowing()
	{
		return menuButtonUnpressing();
	}

	public bool menuButtonDeeping()
	{
		return menuButtonPressing();
	}
	public bool menuButtonDeeped()
	{
		return menuButtonPressed();
	}
	public bool menuButtonUndeeping()
	{
		return menuButtonUnpressing();
	}

	public bool menuButtonPressing()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu);
	}
	public bool menuButtonPressed()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu);
	}
	public bool menuButtonUnpressing()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu);
	}


	// detection - grip //

	public bool gripTouching()
	{
		return false;
	}
	public bool gripTouched()
	{
		return false;
	}
	public bool gripUntouching()
	{
		return false;
	}

	public bool gripShallowing()
	{
		return gripPressing();
	}
	public bool gripShallowed()
	{
		return gripPressed();
	}
	public bool gripUnshallowing()
	{
		return gripUnpressing();
	}

	public bool gripDeeping()
	{
		return gripPressing();
	}
	public bool gripDeeped()
	{
		return gripPressed();
	}
	public bool gripUndeeping()
	{
		return gripUnpressing();
	}

	public bool gripPressing()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
	}
	public bool gripPressed()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPress(SteamVR_Controller.ButtonMask.Grip);
	}
	public bool gripUnpressing()
	{
		if (hand.controller == null)
		{
			return false;
		}
		return hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
	}
	


	
	// generic handling of inputs with particular inputtedness and beingness //


	public bool inputTouching(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerTouching();
		}
		else if (input == Input.touchpad)
		{
			return touchpadTouching();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonTouching();
		}
		else if (input == Input.grip)
		{
			return gripTouching();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputTouching(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputTouching(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputTouched(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerTouched();
		}
		else if (input == Input.touchpad)
		{
			return touchpadTouched();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonTouched();
		}
		else if (input == Input.grip)
		{
			return gripTouched();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputTouched(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputTouched(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputUntouching(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerUntouching();
		}
		else if (input == Input.touchpad)
		{
			return touchpadUntouching();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonUntouching();
		}
		else if (input == Input.grip)
		{
			return gripUntouching();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputUntouching(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputUntouching(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputShallowing(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerShallowing();
		}
		else if (input == Input.touchpad)
		{
			return touchpadShallowing();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonShallowing();
		}
		else if (input == Input.grip)
		{
			return gripShallowing();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputShallowing(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputShallowing(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputShallowed(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerShallowed();
		}
		else if (input == Input.touchpad)
		{
			return touchpadShallowed();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonShallowed();
		}
		else if (input == Input.grip)
		{
			return gripShallowed();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputShallowed(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputShallowed(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputUnshallowing(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerUnshallowing();
		}
		else if (input == Input.touchpad)
		{
			return touchpadUnshallowing();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonUnshallowing();
		}
		else if (input == Input.grip)
		{
			return gripUnshallowing();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputUnshallowing(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputUnshallowing(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputDeeping(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerDeeping();
		}
		else if (input == Input.touchpad)
		{
			return touchpadDeeping();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonDeeping();
		}
		else if (input == Input.grip)
		{
			return gripDeeping();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputDeeping(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputDeeping(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputDeeped(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerDeeped();
		}
		else if (input == Input.touchpad)
		{
			return touchpadDeeped();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonDeeped();
		}
		else if (input == Input.grip)
		{
			return gripDeeped();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputDeeped(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputDeeped(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputUndeeping(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerUndeeping();
		}
		else if (input == Input.touchpad)
		{
			return touchpadUndeeping();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonUndeeping();
		}
		else if (input == Input.grip)
		{
			return gripUndeeping();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputUndeeping(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputUndeeping(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputPressing(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerPressing();
		}
		else if (input == Input.touchpad)
		{
			return touchpadPressing();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonPressing();
		}
		else if (input == Input.grip)
		{
			return gripPressing();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputPressing(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputPressing(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputPressed(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerPressed();
		}
		else if (input == Input.touchpad)
		{
			return touchpadPressed();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonPressed();
		}
		else if (input == Input.grip)
		{
			return gripPressed();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputPressed(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputPressed(input))
			{
				return true;
			}
		}

		return false;
	}

	public bool inputUnpressing(Input input)
	{
		if (input == Input.none)
		{
			return false;
		}
		else if (input == Input.trigger)
		{
			return triggerUnpressing();
		}
		else if (input == Input.touchpad)
		{
			return touchpadUnpressing();
		}
		else if (input == Input.menuButton)
		{
			return menuButtonUnpressing();
		}
		else if (input == Input.grip)
		{
			return gripUnpressing();
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputUnpressing(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (inputUnpressing(input))
			{
				return true;
			}
		}

		return false;
	}




	// generic handling of inputs and inputtedness with particular beingness //


	public bool inputInputting(Input input, Inputtedness inputtedness)
	{
		if (inputtedness == Inputtedness.touch)
		{
			return inputTouching(input);
		}
		else if (inputtedness == Inputtedness.shallow)
		{
			return inputShallowing(input);
		}
		else if (inputtedness == Inputtedness.deep)
		{
			return inputDeeping(input);
		}
		else if (inputtedness == Inputtedness.press)
		{
			return inputPressing(input);
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputInputting(Input[] inputs, Inputtedness inputtedness)
	{
		foreach (Input input in inputs)
		{
			if (inputInputting(input, inputtedness))
			{
				return true;
			}
		}

		return false;
	}
	public bool inputInputting(Input input, Inputtedness[] inputtednesses)
	{
		foreach (Inputtedness inputtedness in inputtednesses)
		{
			if (inputInputting(input, inputtedness))
			{
				return true;
			}
		}

		return false;
	}
	public bool inputInputting(Input[] inputs, Inputtedness[] inputtednesses)
	{
		foreach (Input input in inputs)
		{
			foreach (Inputtedness inputtedness in inputtednesses)
			{
				if (inputInputting(input, inputtedness))
				{
					return true;
				}
			}
		}

		return false;
	}

	public bool inputInputted(Input input, Inputtedness inputtedness)
	{
		if (inputtedness == Inputtedness.touch)
		{
			return inputTouched(input);
		}
		else if (inputtedness == Inputtedness.shallow)
		{
			return inputShallowed(input);
		}
		else if (inputtedness == Inputtedness.deep)
		{
			return inputDeeped(input);
		}
		else if (inputtedness == Inputtedness.press)
		{
			return inputPressed(input);
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputInputted(Input[] inputs, Inputtedness inputtedness)
	{
		foreach (Input input in inputs)
		{
			if (inputInputted(input, inputtedness))
			{
				return true;
			}
		}

		return false;
	}
	public bool inputInputted(Input input, Inputtedness[] inputtednesses)
	{
		foreach (Inputtedness inputtedness in inputtednesses)
		{
			if (inputInputted(input, inputtedness))
			{
				return true;
			}
		}

		return false;
	}
	public bool inputInputted(Input[] inputs, Inputtedness[] inputtednesses)
	{
		foreach (Input input in inputs)
		{
			foreach (Inputtedness inputtedness in inputtednesses)
			{
				if (inputInputted(input, inputtedness))
				{
					return true;
				}
			}
		}

		return false;
	}

	public bool inputUninputting(Input input, Inputtedness inputtedness)
	{
		if (inputtedness == Inputtedness.touch)
		{
			return inputUntouching(input);
		}
		else if (inputtedness == Inputtedness.shallow)
		{
			return inputUnshallowing(input);
		}
		else if (inputtedness == Inputtedness.deep)
		{
			return inputUndeeping(input);
		}
		else if (inputtedness == Inputtedness.press)
		{
			return inputUnpressing(input);
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputUninputting(Input[] inputs, Inputtedness inputtedness)
	{
		foreach (Input input in inputs)
		{
			if (inputUninputting(input, inputtedness))
			{
				return true;
			}
		}

		return false;
	}
	public bool inputUninputting(Input input, Inputtedness[] inputtednesses)
	{
		foreach (Inputtedness inputtedness in inputtednesses)
		{
			if (inputUninputting(input, inputtedness))
			{
				return true;
			}
		}

		return false;
	}
	public bool inputUninputting(Input[] inputs, Inputtedness[] inputtednesses)
	{
		foreach (Input input in inputs)
		{
			foreach (Inputtedness inputtedness in inputtednesses)
			{
				if (inputUninputting(input, inputtedness))
				{
					return true;
				}
			}
		}

		return false;
	}




	// generic handling of inputs, inputtedness, and beingness //

	
	public bool inputInputtednessBeingnessOperation(Input[] inputs, Inputtedness[] inputtednesses, StatesOfBeing.Beingness beingness)
	{
		if (beingness == StatesOfBeing.Beingness.becoming)
		{
			return inputInputting(inputs, inputtednesses);
		}
		else if (beingness == StatesOfBeing.Beingness.being)
		{
			return inputInputted(inputs, inputtednesses);
		}
		else if (beingness == StatesOfBeing.Beingness.unbecoming)
		{
			return inputUninputting(inputs, inputtednesses);
		}
		else        // (default case)
		{
			return false;
		}
	}
	public bool inputInputtednessBeingnessOperation(Input[] inputs, Inputtedness[] inputtednesses, StatesOfBeing.Beingness[] beingnesses)
	{
		foreach (StatesOfBeing.Beingness beingness in beingnesses)
		{
			if (inputInputtednessBeingnessOperation(inputs, inputtednesses, beingness))
			{
				return true;
			}
		}

		return false;
	}




	// method: determine whether the given array of inputs has any actual (non-none) inputs //
	public static bool anyActualInputs(Input[] inputs)
	{
		foreach (Input input in inputs)
		{
			if (input != Input.none)
			{
				return true;
			}
		}
		return false;
	}
	
	
	
	
	// control for delayed prevention of any vibration at the start; method: vibrate during this frame with the given intensity value; control and methods for extended (multiple frame) vibrating toggling //

	
	private float vibrationPreventionDelay = .01f, vibrationPreventionTimer = 0;
	private bool vibrationAllowed = false;

    public void vibrate(ushort intensity)
    {
		if (hand.controller == null)
		{
			return;
		}
		if (vibrationAllowed)
		{
			hand.controller.TriggerHapticPulse(intensity);
		}
    }

	private bool vibratingToggled = false;
	private float toggledVibrationDuration = .1f;
	private ushort toggledVibrationIntensity = 3000;
	private void toggleVibrationOff()
	{
		vibratingToggled = false;
	}
	public void vibrateExtended()
	{
		vibratingToggled = true;
		Invoke("toggleVibrationOff", toggledVibrationDuration);
	}
	public void vibrateExtended(float duration)
	{
		toggledVibrationDuration = duration;
		vibrateExtended();
	}
	public void vibrateExtended(ushort intensity)
	{
		toggledVibrationIntensity = intensity;
		vibrateExtended();
	}
	public void vibrateExtended(float duration, ushort intensity)
	{
		toggledVibrationDuration = duration;
		toggledVibrationIntensity = intensity;
		vibrateExtended();
	}




	// method: determine whether the given operation (ignoring its handedness) is currently operated by the left-handed controller, ignoring the operation's dependencies //
	private static bool operatedLeft(ControllerOperation operation)
	{
		return left.inputInputtednessBeingnessOperation(operation.inputs, operation.inputtednesses, operation.beingnesses);
	}
	// method: determine whether the given operation (ignoring its handedness) is currently operated by the right-handed controller, ignoring the operation's dependencies //
	private static bool operatedRight(ControllerOperation operation)
	{
		return right.inputInputtednessBeingnessOperation(operation.inputs, operation.inputtednesses, operation.beingnesses);
	}
	// method: determine whether the given operation is currently operated, ignoring its dependencies //
	private static bool operatedIgnoringDependencies(ControllerOperation operation)
	{
		if (operation.handedness == Handedness.neither)
		{
			return false;
		}
		else if (operation.handedness == Handedness.either)
		{
			return operatedLeft(operation) || operatedRight(operation);
		}
		else if (operation.handedness == Handedness.one)
		{
			return operatedLeft(operation) ^ operatedRight(operation);
		}
		else if (operation.handedness == Handedness.left)
		{
			return operatedLeft(operation);
		}
		else if (operation.handedness == Handedness.right)
		{
			return operatedRight(operation);
		}
		else if (operation.handedness == Handedness.both)
		{
			return operatedLeft(operation) && operatedRight(operation);
		}
		else if (operation.handedness == Handedness.infinite)
		{
			return true;
		}
		else        // (default case)
		{
			return false;
		}
	}
	// method: determine whether the given operation is currently operated //
	public static bool operated(ControllerOperation operation)
	{
		if (!operation.dependenciesMet())
		{
			return false;
		}
		return operatedIgnoringDependencies(operation);
	}
	// method: determine whether any of the given operations are currently operated //
	public static bool operated(ControllerOperation.SetOfControllerOperations operations)
	{
		foreach (ControllerOperation operation in operations.array)
		{
			if (operated(operation))
			{
				return true;
			}
		}
		return false;
	}
	// method: determine the set of controllers for which the given operation is currently operated, ignoring its dependencies //
	private static HashSet<Controller> operatedControllersIgnoringDependencies(ControllerOperation operation)
	{
		if (operation.handedness == Handedness.neither)
		{
			return new HashSet<Controller>();
		}
		else if (operation.handedness == Handedness.one)
		{
			if (operatedLeft(operation) && !operatedRight(operation))
			{
				return new HashSet<Controller>() {left};
			}
			else if (!operatedLeft(operation) && operatedRight(operation))
			{
				return new HashSet<Controller>() {right};
			}
			return new HashSet<Controller>();
		}
		else if (operation.handedness == Handedness.left)
		{
			if (operatedLeft(operation))
			{
				return new HashSet<Controller>() {left};
			}
			return new HashSet<Controller>();
		}
		else if (operation.handedness == Handedness.right)
		{
			if (operatedRight(operation))
			{
				return new HashSet<Controller>() {right};
			}
			return new HashSet<Controller>();
		}
		else if (operation.handedness == Handedness.both)
		{
			if (operatedLeft(operation) && operatedRight(operation))
			{
				return new HashSet<Controller>() {left, right};
			}
			return new HashSet<Controller>();
		}
		else if ((operation.handedness == Handedness.either) || (operation.handedness == Handedness.infinite))
		{
			HashSet<Controller> setOfOperatedControllers = new HashSet<Controller>();
			if (operatedLeft(operation))
			{
				setOfOperatedControllers.Add(left);
			}
			if (operatedRight(operation))
			{
				setOfOperatedControllers.Add(right);
			}
			return setOfOperatedControllers;
		}
		else        // (default case)
		{
			return new HashSet<Controller>();
		}
	}
	// method: determine the set of controllers for which the given operation is currently operated //
	public static HashSet<Controller> operatedControllers(ControllerOperation operation)
	{
		if (!operation.dependenciesMet())
		{
			return new HashSet<Controller>();
		}
		return operatedControllersIgnoringDependencies(operation);
	}
	// method: determine the set of controllers for which any of the given operations are currently operated //
	public static HashSet<Controller> operatedControllers(ControllerOperation.SetOfControllerOperations operations)
	{
		HashSet<Controller> setOfOperatedControllers = new HashSet<Controller>();
		foreach (ControllerOperation operation in operations.array)
		{
			HashSet<Controller> setOfOperatedControllersForOperation = operatedControllers(operation);
			foreach (Controller operatedController in setOfOperatedControllersForOperation)
			{
				setOfOperatedControllers.Add(operatedController);
			}
		}
		return setOfOperatedControllers;
	}
















	// updating //


	// before the start: //
	private void Awake()
    {
		// connect to: this controller's hand, the other controller //
		hand = GetComponent<Hand>();
		otherController = hand.otherHand.GetComponent<Controller>();
		
		// track whether this controller is for the left hand //
		leftInstance = (hand.startingHandType == Hand.HandType.Left);
    }

	// upon being enabled: //
	private void OnEnable()
	{
		// connect the corresponding instance of this class //
		if (leftInstance)
		{
			left = this;
		}
		else
		{
			right = this;
		}
	}

	// at each update: //
	private void Update()
	{
		// touchpad touching tracking //
		trackTouchpadTouching();

		// delayed prevention of any vibrating at the start //
		if (!vibrationAllowed)
		{
			if (vibrationPreventionTimer < vibrationPreventionDelay)
			{
				vibrationPreventionTimer += Time.deltaTime;
				if (vibrationPreventionTimer > vibrationPreventionDelay)
					vibrationPreventionTimer = vibrationPreventionDelay;
				if (vibrationPreventionTimer == vibrationPreventionDelay)
					vibrationAllowed = true;
			}
		}

		// extended (multiple frame) vibrating //
		if (vibratingToggled)
		{
			vibrate(toggledVibrationIntensity);
		}
	}
}

// ∗: example usage of touchpad travelling input to flip pages of a book
/*
private void Update()
{
	// interaction: page flipping //
	float touchpadXTravelDistance = controller.touchpadXTravelDirectional();
	if (Mathf.Abs(touchpadXTravelDistance) > pageFlippingTravelDistanceThreshold)
	{
		controller.resetTouchpadTouchdown();
		if ((touchpadXTravelDistance > 0f))
		{
			controller.vibrate(1000);
			PageFlipper.flipToNextPage();
		}
		else if ((touchpadXTravelDistance < 0f))
		{
			controller.vibrate(1000);
			PageFlipper.flipToPreviousPage();
		}
	}
}
*/