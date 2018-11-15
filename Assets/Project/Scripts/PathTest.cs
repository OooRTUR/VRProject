using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathTest : MonoBehaviour {
	
	public Transform startTransform;
	public Transform endTransform;
	NavMeshPath path;

	void Start () {
		path = new NavMeshPath ();
	}

	void Update () {
		NavMesh.CalculatePath (startTransform.position, endTransform.position, NavMesh.AllAreas, path);
		for (int i = 0; i < path.corners.Length - 1; i++)
			Debug.DrawLine (path.corners [i], path.corners [i + 1], Color.red);
	}
}
