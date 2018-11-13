using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAI : MonoBehaviour {
	public Transform[] saveZones;

	public List<Transform> variantsZones = new List<Transform>();
	public Transform finalZone;

	MouseMotor motor;

	void Awake () {
		motor = GetComponent<MouseMotor>();
	}

	public void FindSaveZone (Vector3 predatorPos) {
		if(motor.cond == MouseMotor.Condition.Walk) {
			variantsZones.Clear();
			finalZone = null;
			foreach(Transform zone in saveZones) {
				if(Vector3.Angle(-DirectionTo(predatorPos), DirectionTo(zone.position)) < 45)
					variantsZones.Add(zone);
			}
			if(variantsZones.Count > 0) {
				foreach(Transform zone in variantsZones) {
					if(finalZone == null)
						finalZone = zone;
					else
						if(DistanceTo(zone.position) < DistanceTo(finalZone.position))
							finalZone = zone;
				}
				motor.SawPredator(finalZone.position);
			}
			else
				motor.SawPredator();
		}
	}

	Vector3 DirectionTo (Vector3 position) {
		Vector3 direction = (position - transform.position).normalized;
		return direction;
	}

	float DistanceTo (Vector3 position) {
		float distance = Vector3.Distance(transform.position, position);
		return distance;
	}
}
