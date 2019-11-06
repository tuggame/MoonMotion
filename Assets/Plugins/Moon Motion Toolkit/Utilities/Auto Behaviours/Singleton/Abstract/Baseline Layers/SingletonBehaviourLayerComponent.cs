﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton Behaviour Layer Component:
// #auto #component
// • provides this singleton behaviour with static access to its auto behaviour's component layer
public abstract class	SingletonBehaviourLayerComponent<SingletonBehaviourT> :
					SingletonBehaviourLayerTransform<SingletonBehaviourT>
						where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
{
	#region destruction

	#region of this component
	public static new void destroyThisBehaviour(bool boolean = true)
		=> autoBehaviour.destroyThisBehaviour(boolean);
	#endregion of this component
	
	#region of the other given component
	public static new void destroy(Component otherComponent, bool boolean = true)
		=> autoBehaviour.destroy(otherComponent, boolean);
	#endregion of the other given component
	#endregion destruction


	#region inspector

	// method: hide this component in the inspector, then return this component //
	public static new AutoBehaviour<SingletonBehaviourT> hideInInspector()
		=> autoBehaviour.hideInInspector();

	// method: show this component in the inspector, then return this component //
	public static new AutoBehaviour<SingletonBehaviourT> unhideInInspector()
		=> autoBehaviour.unhideInInspector();
	#endregion inspector


	#region requirement via RequireComponent

	public static new bool requires_ViaReflection<ComponentTPotentiallyRequired>(bool considerInheritedRequireComponents = true) where ComponentTPotentiallyRequired : Component
		=> autoBehaviour.requires_ViaReflection<ComponentTPotentiallyRequired>(considerInheritedRequireComponents);

	public static new bool required_ViaReflection(bool considerInheritedRequireComponents = true)
		=> autoBehaviour.required_ViaReflection(considerInheritedRequireComponents);
	#endregion requirement via RequireComponent


	#region adding components

	// method: add a new component of the specified class to this component's game object, then return the new component //
	public static new ComponentT addGet<ComponentT>() where ComponentT : Component
		=> autoBehaviour.addGet<ComponentT>();

	// method: add a new component of the specified class to this component's game object, then return this component's game object //
	public static new GameObject add<ComponentT>() where ComponentT : Component
		=> autoBehaviour.add<ComponentT>();
	
	public static new ComponentT ensured<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.ensured<ComponentT>(includeInactiveComponents);
	#endregion adding components


	#region determining local components

	public static new bool hasAny<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.hasAny<ComponentT>(includeInactiveComponents);

	public static new bool hasAny<ComponentT>(Func<ComponentT, bool> function, bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.hasAny(function, includeInactiveComponents);

	public static new bool hasNo<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.hasNo<ComponentT>(includeInactiveComponents);

	public static new bool hasAnyComponentOtherThan(Component component, bool includeInactiveComponents = Default.inclusionOfInactiveComponents)
		=> autoBehaviour.hasAnyComponentOtherThan(component, includeInactiveComponents);

	public static new bool hasAnyOtherComponent(bool includeInactiveComponents = Default.inclusionOfInactiveComponents)
		=> autoBehaviour.hasAnyOtherComponent(includeInactiveComponents);

	public static new bool hasAnyComponentExcept(Component component, Func<Component, bool> function, bool includeInactiveComponents = Default.inclusionOfInactiveComponents)
		=> autoBehaviour.hasAnyComponentExcept(component, function, includeInactiveComponents);

	public static new bool hasAnyOtherComponent(Func<Component, bool> function, bool includeInactiveComponents = Default.inclusionOfInactiveComponents)
		=> autoBehaviour.hasAnyOtherComponent(function, includeInactiveComponents);

	public static new bool hasAnyAutoBehaviours(bool includeInactiveComponents = Default.inclusionOfInactiveComponents)
		=> autoBehaviour.hasAnyAutoBehaviours(includeInactiveComponents);

	public static new bool hasAnyOtherAutoBehaviours(bool includeInactiveComponents = Default.inclusionOfInactiveComponents)
		=> autoBehaviour.hasAnyOtherAutoBehaviours(includeInactiveComponents);
	#endregion determining local components


	#region accessing local components

	// method: return this component's game object's first component of the specified class (null if none found), optionally including inactive components according to the given boolean //
	public static new ComponentT first<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.first<ComponentT>(includeInactiveComponents);

	// method: return a list of the specified class of components, optionally including inactive components according to the given boolean, on this component's game object //
	public static new List<ComponentT> pick<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.pick<ComponentT>(includeInactiveComponents);

	// method: return a list of all components on this component's game object, optionally including inactive components according to the given boolean //
	public static new List<Component> components(bool includeInactiveComponents = Default.inclusionOfInactiveComponents)
		=> autoBehaviour.components();

	// method: return a list of all auto behaviours on this component's game object, optionally including inactive components according to the given boolean //
	public static new List<IAutoBehaviour> autoBehaviours(bool includeInactiveComponents = Default.inclusionOfInactiveComponents)
		=> autoBehaviour.autoBehaviours(includeInactiveComponents);
	#endregion accessing local components


	#region iterating local components

	public static new AutoBehaviour<SingletonBehaviourT> forEach<ComponentT>(Action<ComponentT> action, bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.forEach(action, includeInactiveComponents);
	#endregion iterating local components


	#region picking upon local components

	public static new TResult pickUponFirstIfAny<ComponentT, TResult>(Func<ComponentT, TResult> function, Func<TResult> fallbackfunction) where ComponentT : Component
		=> autoBehaviour.pickUponFirstIfAny(function, fallbackfunction);
	#endregion picking upon local components


	#region determining descendant components
	
	public static new bool anyDescendant<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.anyDescendant<ComponentT>(includeInactiveComponents);
	#endregion determining descendant components


	#region accessing descendant components
	
	public static new ComponentT firstDescendant<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.firstDescendant<ComponentT>(includeInactiveComponents);
	
	public static new ComponentT lastDescendant<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.lastDescendant<ComponentT>(includeInactiveComponents);
	
	public static new IEnumerable<ComponentT> descendants<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.descendants<ComponentT>(includeInactiveComponents);
	#endregion accessing descendant components


	#region determining local or descendant components
	
	public static new bool anyLocalOrDescendant<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.anyLocalOrDescendant<ComponentT>(includeInactiveComponents);
	#endregion determining local or descendant components


	#region accessing local or descendant components
	
	public static new ComponentT firstLocalOrDescendant<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.firstLocalOrDescendant<ComponentT>(includeInactiveComponents);
	
	public static new ComponentT[] localAndDescendant<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.localAndDescendant<ComponentT>(includeInactiveComponents);
	
	public static new ComponentI[] localAndDescendantI<ComponentI>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentI : class
		=> autoBehaviour.localAndDescendantI<ComponentI>(includeInactiveComponents);

	public static new HashSet<GameObject> localAndDescendantObjectsWithI<ComponentI>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentI : class
		=> autoBehaviour.localAndDescendantObjectsWithI<ComponentI>(includeInactiveComponents);
	#endregion accessing local or descendant components


	#region accessing parent components

	// method: return this component's parent's first component of the specified class (null if none found), optionally including inactive components according to the given boolean //
	public static new ComponentT firstParent<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.firstParent<ComponentT>(includeInactiveComponents);

	// method: return a list of this component's parent's components of the specified class, optionally including inactive components according to the given boolean //
	public static new List<ComponentT> parental<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.parental<ComponentT>(includeInactiveComponents);
	#endregion accessing parent components


	#region accessing ancestral components
	
	public static new ComponentT firstAncestor<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.firstAncestor<ComponentT>(includeInactiveComponents);
	
	public static new ComponentT[] ancestral<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.ancestral<ComponentT>(includeInactiveComponents);
	#endregion accessing ancestral components


	#region determining ancestral components
	
	public static new bool hasAnyAncestral<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.hasAnyAncestral<ComponentT>(includeInactiveComponents);
	#endregion determining ancestral components


	#region accessing local or ancestral components

	// method: return this component's first local or parent component of the specified class (null if none found), optionally including inactive components according to the given boolean //
	public static new ComponentT firstLocalOrAncestor<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.firstLocalOrAncestor<ComponentT>(includeInactiveComponents);

	// method: return an array of this component's local and parent components of the specified class, optionally including inactive components according to the given boolean //
	public static new ComponentT[] localAndAncestral<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.localAndAncestral<ComponentT>(includeInactiveComponents);
	#endregion accessing local or ancestral components


	#region searching for self or ancestor based on comparison

	// method: return the first game object out of the game object for this component and its parent game objects (searching upward) to have a component of the given type (null if none found), optionally including inactive components according to the given boolean //
	public static new GameObject firstSelfOrAncestorObjectWith<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.firstSelfOrAncestorObjectWith<ComponentT>(includeInactiveComponents);

	// method: return the transform of the first game object out of this component and its parent game objects (searching upward) to have a component of the given type (null if none found), optionally including inactive components according to the given boolean //
	public static new Transform firstSelfOrAncestorTransformWith<ComponentT>(bool includeInactiveComponents = Default.inclusionOfInactiveComponents) where ComponentT : Component
		=> autoBehaviour.firstSelfOrAncestorTransformWith<ComponentT>(includeInactiveComponents);
	#endregion searching for self or ancestor based on comparison
}