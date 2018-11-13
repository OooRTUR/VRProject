using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfWalk : MonoBehaviour {

	public Transform areaCenter;
	public float walkRadius;

	public Vector3 GetWalkPoint () {
		float x = Random.Range (-walkRadius, walkRadius);
		float z = Random.Range(-walkRadius, walkRadius);
		Vector3 waypoint = new Vector3(x + areaCenter.position.x,transform.position.y, z + areaCenter.position.z);

		return waypoint;
	}
}
