using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AnimalAI))]
public class AreaOfWalkEditor : Editor {

	void OnSceneGUI () {
		AnimalAI aow = (AnimalAI)target;
		Handles.color = Color.green;
		if (aow.areaCenter != null)
			Handles.DrawWireCube(aow.areaCenter.position, new Vector3(aow.walkRadius * 2, 0, aow.walkRadius * 2));
	}
}
