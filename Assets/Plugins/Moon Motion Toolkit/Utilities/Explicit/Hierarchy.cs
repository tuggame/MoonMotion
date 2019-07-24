﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Hierarchy: provides functionality for handling the hierarchy //
public static class Hierarchy
{
	// state //

	
	public static readonly string playerName = "Player";
	
	public static IEnumerable<Scene> selectScenes => Select.forCount(SceneManager.sceneCount, sceneIndex =>
																	SceneManager.GetSceneAt(sceneIndex));

	public static IEnumerable<GameObject> selectPrimogenitorObjects => selectScenes.selectNested(scene => scene.GetRootGameObjects());

	public static IEnumerable<Transform> selectPrimogenitorTransforms => selectPrimogenitorObjects.selectTransforms();

	public static int primogenitorCount => selectPrimogenitorObjects.count();

	public static int lastPrimogenitorIndex => primogenitorCount - 1;



	
	// methods //

	
	// method: create a fresh game object with the default name, make it hidden, then return the created game object //
	public static GameObject createTemporaryObject()
		=> new GameObject()
			.makeTemporary();
}