using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AreaOfWalk))]
public class AreaOfWalkEditor : Editor {

	void OnSceneGUI () {
		AreaOfWalk aow = (AreaOfWalk)target;
		Handles.color = Color.green;
		if (aow.areaCenter != null)
			Handles.DrawWireCube(aow.areaCenter.position, new Vector3(aow.walkRadius * 2, 0, aow.walkRadius * 2));
	}
}
