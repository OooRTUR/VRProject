using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AreaOfWalk))]
public class AreaOfWalkEditor : Editor {

	void OnSceneGUI () {
		AreaOfWalk aow = (AreaOfWalk)target;
		Handles.color = Color.green;
		Handles.DrawWireArc(aow.areaCenter.position,Vector3.up,Vector3.forward, 360, aow.walkRadius);
	}
}
