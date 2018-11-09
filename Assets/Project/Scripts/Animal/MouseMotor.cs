using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseMotor : MonoBehaviour {

	public float walkSpeed = 3f;
	public float runSpeed = 7f;

	NavMeshAgent agent;
	AreaOfWalk walkArea;
	public enum Condition {Walk, Run}
	public Condition cond;

	void Awake () {
		agent = GetComponent<NavMeshAgent>();
		walkArea = GetComponent<AreaOfWalk>();
	}

	void Start () {
		StartCoroutine("Walking");
	}

	void Move (Vector3 waypoint, float speed) {
		agent.speed = cond == Condition.Walk ? walkSpeed : runSpeed;
		agent.SetDestination(waypoint);
	}

	IEnumerator Walking () {
		while(cond == Condition.Walk) {
			Move(walkArea.GetWalkPoint(), runSpeed);
			//while (agent.pathStatus == NavMeshPathStatus.PathComplete) yield return null;
			float randomSec = Random.Range(3,6);
			yield return new WaitForSeconds(randomSec);
			Debug.Log("Path Complete!");
		}
	}
}
