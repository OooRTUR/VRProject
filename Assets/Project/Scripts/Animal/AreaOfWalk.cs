using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfWalk : MonoBehaviour {

	public float walkRadius;
	[HideInInspector]public Transform areaCenter;

	AnimalAI animalAI;

	void Awake () {
		animalAI = GetComponent<AnimalAI> ();
	}

	void Start () {
		FindAreaCenter ();
	}

	void FindAreaCenter () {
		foreach (Transform trans in animalAI.saveZones) {
			if (areaCenter == null || animalAI.DistanceTo (trans.position) < animalAI.DistanceTo (areaCenter.position))
				areaCenter = trans;
		}
	}

	public Vector3 GetWalkPoint () {
		float x = Random.Range (-walkRadius, walkRadius);
		float z = Random.Range(-walkRadius, walkRadius);
		Vector3 waypoint = new Vector3(x + areaCenter.position.x,areaCenter.transform.position.y, z + areaCenter.position.z);

		return waypoint;
	}
}
