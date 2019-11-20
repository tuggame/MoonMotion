﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Components:
// • provides methods for handling components
// #component #assets
public static class Components
{
	public static HashSet<ComponentT> prefabbedOfType<ComponentT>() where ComponentT : Component
		=>	Prefabs.withAnyLodal<ComponentT>()
				.pickUnique(prefab =>
					prefab.firstLodal<ComponentT>());
	public static List<ComponentT> manifestedlyPrefabbedOfType<ComponentT>() where ComponentT : Component
		=> prefabbedOfType<ComponentT>().manifested();
	
	public static HashSet<ComponentI> prefabbedOfTypeI<ComponentI>() where ComponentI : class
		=>	Prefabs.withAnyLodalI<ComponentI>()
				.pickUnique(prefab =>
					prefab.firstLodalI<ComponentI>());
	public static List<ComponentI> manifestedlyPrefabbedOfTypeI<ComponentI>() where ComponentI : class
		=> prefabbedOfTypeI<ComponentI>().manifested();
}