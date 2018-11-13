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

	public void SawPredator (Vector3 runTo) {
		cond = Condition.Run;
		StopCoroutine("Walking");
		Move(runTo);
		Debug.Log("Бегу к норе");
	}

	public void SawPredator () {
		cond = Condition.Run;
		StopCoroutine("Walking");
		Debug.Log("Не знаю куда бежать");
	}

	void Move (Vector3 waypoint) {
		agent.speed = cond == Condition.Walk ? walkSpeed : runSpeed;
		agent.SetDestination(waypoint);
	}

	IEnumerator Walking () {
		while(cond == Condition.Walk) {
			Move(walkArea.GetWalkPoint());
			//while (agent.pathStatus == NavMeshPathStatus.PathComplete) yield return null;
			float randomSec = Random.Range(3,6);
			yield return new WaitForSeconds(randomSec);
			Debug.Log("Path Complete!");
		}
	}
}
