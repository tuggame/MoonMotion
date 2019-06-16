﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Float Extensions: provides extension methods for handling floats //
public static class FloatExtensions
{
	// methods for: comparison //

	public static bool equals(this float float_, float otherFloat)
		=> (float_ == otherFloat);

	public static bool lesserThan(this float float_, float otherFloat)
		=> (float_ < otherFloat);

	public static bool lesserThanOrEqualTo(this float float_, float otherFloat)
		=> (float_ <= otherFloat);

	public static bool greaterThan(this float float_, float otherFloat)
		=> (float_ > otherFloat);

	public static bool greaterThanOrEqualTo(this float float_, float otherFloat)
		=> (float_ >= otherFloat);


	// methods for: parity determination //

	// method: return whether this given float is even //
	public static bool even(this float float_)
		=> ((float_ % 2f) == 0f);

	// method: return whether this given float is odd //
	public static bool odd(this float float_)
		=> ((float_ % 2f) == 1f);


	// methods for: range determination //

	// method: return whether this given float is within (in inclusive range of) the given lower and upper bound values //
	public static bool within(this float float_, float lower, float upper)
		=> (lower <= float_) && (float_ <= upper);

	// method: return whether this given float is within (in inclusive range of) the given range //
	public static bool within(this float float_, Vector2 range)
		=> float_.within(range.x, range.y);

	// method: return whether this given float is within (in inclusive range of) any of the given ranges //
	public static bool withinAnyOf(this float float_, Vector2[] ranges)
		=> ranges.Any(range => float_.within(range));

	// method: return whether this given float is within (in inclusive range of) all of the given ranges //
	public static bool withinAllOf(this float float_, Vector2[] ranges)
		=> ranges.All(range => float_.within(range));

	// method: return whether this given float is between (in exclusive range of) the given lower and upper bound values //
	public static bool between(this float float_, float lower, float upper)
		=> (lower < float_) && (float_ < upper);

	// method: return whether this given float is between (in exclusive range of) the given range //
	public static bool between(this float float_, Vector2 range)
		=> float_.between(range.x, range.y);

	// method: return whether this given float is between (in exclusive range of) any of the given ranges //
	public static bool betweenAnyOf(this float float_, Vector2[] ranges)
		=> ranges.Any(range => float_.between(range));

	// method: return whether this given float is between (in exclusive range of) all of the given ranges //
	public static bool betweenAllOf(this float float_, Vector2[] ranges)
		=> ranges.All(range => float_.between(range));


	// methods for: validity determination //

	// method: return whether this given float is valid //
	public static bool valid(this float float_)
		=> float_.within(float.MinValue, float.MaxValue);


	// methods for: sign determination //

	// method: return whether this given float is unsigned //
	public static bool unsigned(this float float_)
		=> (float_ == 0f);

	// method: return whether this given float is signed //
	public static bool signed(this float float_)
		=> (float_ != 0f);

	// method: return whether this given float is positive //
	public static bool positive(this float float_)
		=> (float_ > 0f);

	// method: return whether this given float is nonpositive //
	public static bool nonnegative(this float float_)
		=> (float_ >= 0f);

	// method: return whether this given float is positive //
	public static bool negative(this float float_)
		=> (float_ < 0f);

	// method: return whether this given float is nonpositive //
	public static bool nonpositive(this float float_)
		=> (float_ <= 0f);


	// methods for: clamping //

	public static float atLeast(this float float_, float otherFloat, bool boolean = true)
	{
		if (boolean)
		{
			return Mathf.Max(float_, otherFloat);
		}
		return float_;
	}

	public static float atMost(this float float_, float otherFloat, bool boolean = true)
	{
		if (boolean)
		{
			return Mathf.Min(float_, otherFloat);
		}
		return float_;
	}

	public static float clampedRatio(this float float_, bool boolean = true)
		=> float_.atLeast(0f, boolean).atMost(1f, boolean);

	public static float clampedValid(this float float_)
		=> float_.atLeast(float.MinValue).atMost(float.MaxValue);

	public static float clampedValidAndNonnegative(this float float_)
		=> float_.atLeast(0f).atMost(float.MaxValue);

	public static float clampedNonnegative(this float float_)
		=> float_.atLeast(0f);

	public static float clampedNonpositive(this float float_)
		=> float_.atMost(0f);


	// methods for: distance //

	public static float distanceFrom(this float float_, float otherFloat)
		=> (float_ - otherFloat).absoluteValue();


	// methods for: sign manipulation //

	public static float absoluteValue(this float float_)
		=> Mathf.Abs(float_);

	public static float withSign(this float float_, bool booleanForSign)
		=> (float_.absoluteValue() * booleanForSign.asSign());

	public static float timesSign(this float float_, bool booleanForSign)
		=> (float_ * booleanForSign.asSign());


	// methods for: math operations //

	public static float halved(this float float_)
		=> (float_ / 2f);

	public static float doubled(this float float_)
		=> (float_ * 2f);

	public static float toThePowerOf(this float float_, float power)
		=> Mathf.Pow(float_, power);

	public static float squared(this float float_)
		=> float_.toThePowerOf(2f);

	public static float timesPi(this float float_)
		=> (float_ * Mathf.PI);


	// methods for: interpolation //

	// method: return this given ratio float "lerped" (linearly interpolated) along the range from the given start float to the given end float - without clamping //
	public static float lerpedUnclampingly(this float ratio, float start, float end)
		=> start + ((end - start) * ratio);

	// method: return this given ratio float "lerped" (linearly interpolated) along the range from the given start float to the given end float - with clamping //
	public static float lerpedClampingly(this float ratio, float start, float end)
		=> ratio.clampedRatio().lerpedUnclampingly(start, end);

	// method: return this given ratio float "lerped" (linearly interpolated) along the range from the given start float to the given end float - with clamping according to the given 'clamp' boolean //
	public static float lerped(this float ratio, float start, float end, bool clamp)
		=> clamp ? ratio.lerpedClampingly(start, end) : ratio.lerpedUnclampingly(start, end);


	// methods for: conversion //

	// method: return the integer for this given float //
	public static int asInteger(this float float_)
		=> (int) float_;

	// method: return the sign integer for this given float //
	public static int asSign(this float float_)
		=> float_.asInteger().asSign();

	// method: return the double for this given float //
	public static double asDouble(this float float_)
		=> float_;

	// method: return a vector for this given float (as each coordinate) //
	public static Vector3 asVector(this float float_)
		=> new Vector3(float_, float_, float_);

	// method: return the doubles array for this given floats array //
	public static double[] asDoublesArray(this float[] floats)
	{
		double[] doubles = new double[floats.Length];
		for (int index = 0; index < floats.Length; index++)
		{
			doubles[index] = floats[index];
		}
		return doubles;
	}
}