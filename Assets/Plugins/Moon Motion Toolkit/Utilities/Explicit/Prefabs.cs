﻿#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Prefabs:
// • provides properties and methods for handling prefabs
// #assets
public static class Prefabs
{
	#region filtering all prefabs
	
	public static HashSet<GameObject> filteredBy(string filterString)
		=>	Asset.pathsForPrefabAssetsFilteredBy(filterString)
				.pickUnique(assetPath =>
					Asset.atPathOfType<GameObject>(assetPath));
	
	public static HashSet<GameObject> all => filteredBy("");

	public static HashSet<GameObject> withAnyLocalOrDescendant<ComponentT>() where ComponentT : Component
		=> all.uniquesWhere(prefab => prefab.hasAnyLocalOrDescendant<ComponentT>());

	public static HashSet<GameObject> withAnyLocalOrDescendantMonoBehaviour => withAnyLocalOrDescendant<MonoBehaviour>();

	public static HashSet<GameObject> withAnyLocalOrDescendantI<ComponentI>() where ComponentI : class
		=> all.uniquesWhere(prefab => prefab.hasAnyLocalOrDescendantI<ComponentI>());
	#endregion filtering all prefabs
}
#endif