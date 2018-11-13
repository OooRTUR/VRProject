using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]public List<Transform> visibleTargets = new List<Transform>();
	MouseAI animalAI;


	void Awake () {
		animalAI = GetComponent<MouseAI>();
		StartCoroutine(FindTargetsWithDelay(0.8f));
	}

	void Update () {
		if(visibleTargets.Count > 0) animalAI.FindSaveZone(visibleTargets[0].position);
	}

	IEnumerator FindTargetsWithDelay (float delay) {
		while(true) {
			FindVisibleTargets();
			yield return new WaitForSeconds(delay);
		}
	}

	void FindVisibleTargets () {
		//visibleTargets.Clear();
		Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		if (targetsInView.Length == 0) visibleTargets.Clear();
		for (int i = 0; i < targetsInView.Length; i++) {
			Transform target = targetsInView[i].transform;
			Vector3 dir = (target.position - transform.position).normalized;
			CharacterController c_controller = targetsInView[i].GetComponent<CharacterController>();
			float dist = Vector3.Distance(transform.position,target.position);
			if (Vector3.Angle(transform.forward, dir) < viewAngle / 2 || c_controller.velocity.magnitude > 6f) {
				if (!Physics.Raycast(transform.position,dir,dist,obstacleMask))
					visibleTargets.Add(target);
				else
					visibleTargets.Remove(target);
			}
		}
	}

	public Vector3 DirFromAngle (float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) angleInDegrees += transform.eulerAngles.y;
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
