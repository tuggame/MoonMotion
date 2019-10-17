﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Auto Behaviour Layer Transform:
// #auto #transform
// • provides this behaviour with methods and automatically-connected properties for its transform
public abstract class	AutoBehaviourLayerTransform<AutoBehaviourT> :
					AutoBehaviourLayerTransformations<AutoBehaviourT>
						where AutoBehaviourT : AutoBehaviour<AutoBehaviourT>
{
	#region transform

	public new Transform transform => inheritorHasAttribute<CacheTransformAttribute>() ? cache<Transform>() : component.transform;     // the transform property is only worth caching (since that takes up memory) when accessed a lot, like every frame in Update, so that property only uses caching according to the respective attribute; see https://forum.unity.com/threads/cache-transform-really-needed.356875/ for discussion of this //
	#endregion transform


	#region sibling indexing

	public int siblingIndex => transform.siblingIndex();
	#endregion sibling indexing


	#region accessing siblings

	public IEnumerable<Transform> selectSiblingAndSelfTransforms => transform.selectSiblingAndSelfTransforms();
	public Transform[] siblingAndSelfTransforms => transform.siblingAndSelfTransforms();

	public IEnumerable<GameObject> selectSiblingAndSelfObjects => transform.selectSiblingAndSelfObjects();
	public GameObject[] siblingAndSelfObjects => transform.siblingAndSelfObjects();

	public IEnumerable<Transform> selectSiblingTransforms => transform.selectSiblingTransforms();
	public IEnumerable<Transform> siblingTransforms => transform.siblingTransforms();

	public IEnumerable<GameObject> selectSiblingObjects => transform.selectSiblingObjects();
	public IEnumerable<GameObject> siblingObjects => transform.siblingObjects();
	#endregion siblings


	#region descendants

	public bool isLocalToOrDescendantOf(Transform otherTransform)
		=> transform.isLocalToOrDescendantOf(otherTransform);
	
	public bool isLocalToOrDescendantOf(GameObject otherGameObject)
		=> transform.isLocalToOrDescendantOf(otherGameObject);
	
	public bool isLocalToOrDescendantOf(Component otherComponent)
		=> transform.isLocalToOrDescendantOf(otherComponent);
	
	public bool isLocalToOrDescendantOf<SingletonBehaviourT>() where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
		=> transform.isLocalToOrDescendantOf<SingletonBehaviourT>();
	#endregion descendants


	#region parent

	public bool parentless => transform.parentless();
	public Transform parent => transform.parent;
	public Transform setParentTo(Transform parentTransform, bool boolean = true)
		=> transform.setParentTo(parentTransform, boolean);
	public Transform setParentTo(GameObject parentObject, bool boolean = true)
		=> transform.setParentTo(parentObject, boolean);
	public Transform setParentTo(Component parentComponent, bool boolean = true)
		=> transform.setParentTo(parentComponent, boolean);
	public Transform setParentTo<ParentSingletonBehaviourT>(bool boolean = true) where ParentSingletonBehaviourT : SingletonBehaviour<ParentSingletonBehaviourT>
		=> transform.setParentTo<ParentSingletonBehaviourT>(boolean);
	public Transform unparent(bool boolean = true)
		=> transform.unparent(boolean);
	#endregion parent


	#region pibling indexing
	
	public int piblingIndex => transform.piblingIndex();
	#endregion pibling indexing


	#region accessing piblings

	public Transform piblingTransform(int piblingIndex)
		=> transform.piblingTransform(piblingIndex);

	public Transform correspondingPiblingTransform => transform.correspondingPiblingTransform();

	public GameObject piblingObject(int piblingIndex)
		=> transform.piblingObject(piblingIndex);

	public Transform firstPiblingTransform => transform.firstPiblingTransform();

	public GameObject firstPiblingObject => transform.firstPiblingObject();

	public Transform lastPiblingTransform => transform.lastPiblingTransform();

	public GameObject lastPiblingObject => transform.lastPiblingObject();

	public IEnumerable<Transform> selectPiblingTransforms => transform.selectPiblingTransforms();
	public Transform[] piblingTransforms => transform.piblingTransforms();

	public IEnumerable<GameObject> selectPiblingObjects => transform.selectPiblingObjects();
	public GameObject[] piblingObjects => transform.piblingObjects();
	#endregion accessing piblings


	#region counting children

	public bool childless => component.childless();

	public bool anyChildren()
		=> component.hasAnyChildren();

	public bool pluralChildren => component.pluralChildren();
	#endregion counting children


	#region accessing children

	public Transform childTransform(int childIndex)
		=> transform.childTransform(childIndex);

	public GameObject childObject(int childIndex)
		=> transform.childObject(childIndex);

	public Transform firstChildTransform => transform.firstChildTransform();

	public GameObject firstChildObject => transform.firstChildObject();

	public Transform lastChildTransform => transform.lastChildTransform();

	public GameObject lastChildObject => transform.lastChildObject();

	public IEnumerable<Transform> selectChildTransforms => transform.selectChildTransforms();
	public Transform[] childTransforms => transform.childTransforms();

	public IEnumerable<GameObject> selectChildObjects => transform.selectChildObjects();
	public GameObject[] childObjects => transform.childObjects();
	#endregion accessing children


	#region destroying children

	public AutoBehaviourT destroyChild(int childIndex)
		=> selfAfter(()=> transform.destroyChild(childIndex));
	
	public AutoBehaviourT destroyChildIfItExists(int childIndex)
		=> selfAfter(()=> transform.destroyChildIfItExists(childIndex));
	
	public AutoBehaviourT destroyFirstChild()
		=> selfAfter(()=> transform.destroyFirstChild());
	
	public AutoBehaviourT destroyFirstChildIfItExists()
		=> selfAfter(()=> transform.destroyFirstChildIfItExists());
	
	public AutoBehaviourT destroyLastChild()
		=> selfAfter(()=> transform.destroyLastChild());
	
	public AutoBehaviourT destroyLastChildIfItExists()
		=> selfAfter(()=> transform.destroyLastChildIfItExists());

	public AutoBehaviourT destroyChildren()
		=> selfAfter(()=> transform.destroyChildren());
	#endregion destroying children


	#region child iteration
	public AutoBehaviourT forEachChildTransform(Action<Transform> action)
		=> selfAfter(()=> transform.forEachChildTransform(action));
	public AutoBehaviourT setActivityOfChildrenTo(bool boolean)
		=> selfAfter(()=> transform.setActivityOfChildrenTo(boolean));
	public AutoBehaviourT activateChildren()
		=> selfAfter(()=> transform.activateChildren());
	public AutoBehaviourT deactivateChildren()
		=> selfAfter(()=> transform.deactivateChildren());
	#endregion child iteration


	#region destroying descendants

	public AutoBehaviourT destroyLastDescendantObjectWithComponentIfItExists<ComponentT>() where ComponentT : Component
		=> selfAfter(()=> gameObject.destroyLastDescendantObjectWithComponentIfItExists<ComponentT>());
	#endregion destroying descendants


	#region euler rotation

	// method: (according to the given boolean:) have this behaviour's transform rotate by the given (x, y, and z) rotation angles, then return this behaviour's transform //
	public AutoBehaviourT rotate(Vector3 rotationAngles, bool boolean = true)
		=> selfAfter(()=> transform.rotate(rotationAngles, boolean));

	// method: (according to the given boolean:) have this behaviour's transform rotate by the given x, y, and z rotation angles, then return this behaviour's transform //
	public AutoBehaviourT rotate(float x, float y, float z, bool boolean = true)
		=> selfAfter(()=> transform.rotate(x, y, z, boolean));

	// method: (according to the given boolean:) have this behaviour's transform rotate by the given x rotation angle, then return this behaviour's transform //
	public AutoBehaviourT rotateX(float x, bool boolean = true)
		=> selfAfter(()=> transform.rotateZ(x, boolean));

	// method: (according to the given boolean:) have this behaviour's transform rotate by the given y rotation angle, then return this behaviour's transform //
	public AutoBehaviourT rotateY(float y, bool boolean = true)
		=> selfAfter(()=> transform.rotateZ(y, boolean));

	// method: (according to the given boolean:) have this behaviour's transform rotate by the given z rotation angle, then return this behaviour's transform //
	public AutoBehaviourT rotateZ(float z, bool boolean = true)
		=> selfAfter(()=> transform.rotateZ(z, boolean));
	#endregion euler rotation


	#region flipping
	// methods: (according to the given boolean:) have this behaviour's transform rotate by 180° on the respective axis, then return this behaviour's transform //

	public AutoBehaviourT flipX(bool boolean = true)
		=> selfAfter(()=> transform.flipX(boolean));
	
	public AutoBehaviourT flipY(bool boolean = true)
		=> selfAfter(()=> transform.flipY(boolean));
	
	public AutoBehaviourT flipZ(bool boolean = true)
		=> selfAfter(()=> transform.flipZ(boolean));
	#endregion flipping


	#region facing
	
	public AutoBehaviourT face(Vector3 targetPosition, bool withX = true, bool withY = true, bool withZ = true, bool boolean = true, params Vector3[] upDirection_MaxOf1)
		=> selfAfter(()=> transform.face(targetPosition, withX, withY, withZ, boolean, upDirection_MaxOf1));
	
	public AutoBehaviourT face(Transform targetTransform, bool withX = true, bool withY = true, bool withZ = true, bool boolean = true, params Vector3[] upDirection_MaxOf1)
		=> selfAfter(()=> transform.face(targetTransform, withX, withY, withZ, boolean, upDirection_MaxOf1));
	
	public AutoBehaviourT face(GameObject targetObject, bool withX = true, bool withY = true, bool withZ = true, bool boolean = true, params Vector3[] upDirection_MaxOf1)
		=> selfAfter(()=> transform.face(targetObject.transform, withX, withY, withZ, boolean, upDirection_MaxOf1));
	
	public AutoBehaviourT face(Component targetComponent, bool withX = true, bool withY = true, bool withZ = true, bool boolean = true, params Vector3[] upDirection_MaxOf1)
		=> selfAfter(()=> transform.face(targetComponent, withX, withY, withZ, boolean, upDirection_MaxOf1));
	
	public AutoBehaviourT faceCamera(bool withX = true, bool withY = true, bool withZ = true, bool boolean = true, params Vector3[] upDirection_MaxOf1)
		=> selfAfter(()=> transform.faceCamera(withX, withY, withZ, boolean, upDirection_MaxOf1));
	#endregion facing


	#region displacement

	#region from other
	public Vector3 displacementFrom(object otherPosition_PositionProvider)
		=> position.displacementFrom(otherPosition_PositionProvider);
	public Vector3 displacementFrom<SingletonBehaviourT>() where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
		=> position.displacementFrom<SingletonBehaviourT>();
	public Vector3 displacementFromCamera()
		=> position.displacementFromCamera();
	#endregion from other

	#region to other
	public Vector3 displacementTo(object otherPosition_PositionProvider)
		=> position.displacementTo(otherPosition_PositionProvider);
	public Vector3 displacementTo<SingletonBehaviourT>() where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
		=> position.displacementTo<SingletonBehaviourT>();
	public Vector3 displacementToCamera()
		=> position.displacementToCamera();
	#endregion to other

	#region with displacement
	public Vector3 positionWithDisplacement(Vector3 displacement)
		=> position.withDisplacement(displacement);
	#endregion with displacement
	#endregion displacement


	#region basic directions
	
	public Vector3 forwardLocal => transform.forward();
	
	public Vector3 backwardLocal => transform.backward();
	
	public Vector3 rightLocal => transform.right();
	
	public Vector3 leftLocal => transform.left();
	
	public Vector3 upLocal => transform.up();
	
	public Vector3 downLocal => transform.down();
	
	public Vector3 relativeDirectionFor(BasicDirection basicDirection)
		=> transform.relativeDirectionFor(basicDirection);
	#endregion basic directions


	#region direction

	#region from other
	public Vector3 directionFrom(object otherPosition_PositionProvider)
		=> position.directionFrom(otherPosition_PositionProvider);
	public Vector3 directionFrom<SingletonBehaviourT>() where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
		=> position.directionFrom<SingletonBehaviourT>();
	public Vector3 directionFromCamera()
		=> position.directionFromCamera();
	#endregion from other

	#region to other
	public Vector3 directionTo(object otherPosition_PositionProvider)
		=> position.directionTo(otherPosition_PositionProvider);
	public Vector3 directionTo<SingletonBehaviourT>() where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
		=> position.directionTo<SingletonBehaviourT>();
	public Vector3 directionToCamera()
		=> position.directionToCamera();
	#endregion to other

	#region distinctivity conversion
	// method: return the local direction relative to this behaviour's transform for this given global direction //
	public Vector3 localDirectionForGlobalDirection(Vector3 globalDirection)
		=> globalDirection.asGlobalDirectionToLocalDirectionRelativeTo(transform);
	// method: return the global direction for this given local direction relative to this behaviour's transform //
	public Vector3 globalDirectionForLocalDirection(Vector3 localDirection)
		=> localDirection.asLocalDirectionToGlobalDirectionFromRelativityOf(transform);
	#endregion distinctivity conversion
	#endregion direction


	#region directionality

	#region directionality same toward both
	public bool directionalitySameTowardBoth(object firstOtherPosition_PositionProvider, object secondOtherPosition_PositionProvider)
		=> position.directionalitySameTowardBoth(firstOtherPosition_PositionProvider, secondOtherPosition_PositionProvider);
	public bool directionalitySameTowardBoth<SingletonBehaviourT>(object secondOtherPosition_PositionProvider) where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
		=>	position.directionalitySameTowardBoth<SingletonBehaviourT>(secondOtherPosition_PositionProvider);
	#endregion directionality same toward both
	#endregion directionality


	#region distance

	#region distance with
	public float distanceWith(Vector3 otherPosition)
		=> position.distanceWith(otherPosition);
	public float distanceWith(object otherPosition_PositionProvider)
		=> position.distanceWith(otherPosition_PositionProvider);
	public float distanceWith<SingletonBehaviourT>() where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
		=> position.distanceWith<SingletonBehaviourT>();
	public float distanceWithCamera()
		=> position.distanceWithCamera();
	#endregion distance with

	#region nearest of
	public Vector3 nearestOf(IEnumerable<Vector3> positions)
		=> transform.nearestOf(positions);
	public Transform nearestOf(IEnumerable<Transform> transforms)
		=> transform.nearestOf(transforms);
	public GameObject nearestOf(IEnumerable<GameObject> gameObjects)
		=> transform.nearestOf(gameObjects);
	#endregion nearest of

	#region farthest of
	public Vector3 farthestOf(IEnumerable<Vector3> positions)
		=> transform.farthestOf(positions);
	public Transform farthestOf(IEnumerable<Transform> transforms)
		=> transform.farthestOf(transforms);
	public GameObject farthestOf(IEnumerable<GameObject> gameObjects)
		=> transform.farthestOf(gameObjects);
	#endregion farthest of

	#region is within distance of
	public bool isWithinDistanceOf(object position_PositionProvider, float thresholdDistance)
		=> position.isWithinDistanceOf(position_PositionProvider, thresholdDistance);
	public bool isWithinDistanceOf<SingletonBehaviourT>(float thresholdDistance) where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
		=> position.isWithinDistanceOf<SingletonBehaviourT>(thresholdDistance);
	public bool isWithinDistanceOfCamera(float thresholdDistance)
		=> position.isWithinDistanceOfCamera(thresholdDistance);
	public bool isWithinDistanceOfPlayer(float thresholdDistance)
		=> position.isWithinDistanceOfPlayer(thresholdDistance);
	#endregion is within distance of

	#region is more distant than
	public bool isMoreDistantThan(object otherPosition_PositionProvider, object distancePosition_PositionProvider)
		=> position.isMoreDistantThan(otherPosition_PositionProvider, distancePosition_PositionProvider);
	#endregion is more distant than
	
	#region is more distant in same directionality as
	public bool isMoreDistantInSameDirectionalityAs(object otherPosition_PositionProvider, object referencePosition_PositionProvider)
		=> position.isMoreDistantInSameDirectionalityAs(otherPosition_PositionProvider, referencePosition_PositionProvider);
	#endregion is more distant in same directionality as

	#region nearest position to be at distance from target
	public Vector3 nearestPositionToBeAtDistanceFrom(object targetPosition_PositionProvider, float distanceFromTarget)
		=> position.nearestPositionToBeAtDistanceFrom(targetPosition_PositionProvider, distanceFromTarget);
	public Vector3 nearestPositionToBeAtDistanceFrom<SingletonBehaviourT>(float distanceFromTarget) where SingletonBehaviourT : SingletonBehaviour<SingletonBehaviourT>
		=> position.nearestPositionToBeAtDistanceFrom<SingletonBehaviourT>(distanceFromTarget);
	public Vector3 nearestPositionToBeAtDistanceFromCameraOf(float distanceFromCamera)
		=> position.nearestPositionToBeAtDistanceFromCameraOf(distanceFromCamera);
	#endregion nearest position to be at distance from target
	#endregion distance


	#region position

	#region determining another position along the given direction
	public Vector3 positionAlong(Vector3 direction, float distance)
		=> position.positionAlong(direction, distance);
	#endregion determining another position along the given direction
	
	#region determining another position ahead of the provided transform
	public Vector3 positionAheadBy(float distance)
		=> gameObject.positionAheadBy(distance);
	#endregion determining another position ahead of the provided transform
	
	#region determining another position relative to the provided position, along the forward direction of the provided transform
	public Vector3 positionAlongForwardOf(object transform_TransformProvider, float distance)
		=> position.positionAlongForwardOf(transform_TransformProvider, distance);
	#endregion determining another position relative to the provided position, along the forward direction of the provided transform
	#endregion position
}