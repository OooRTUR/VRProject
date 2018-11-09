using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellController : MonoBehaviour {
	public Transform rabbit;
	ParticleSystem ps;

	void Awake () {
		ps = GetComponent<ParticleSystem>();
	}

	void Update () {
		ChangePosAndDir(transform.position);
	}

	void OnTriggerEnter(Collider other) {
		if(ps.isPlaying) {
			ps.Stop();
		}
	}

	public void ChangePosAndDir (Vector3 position) {
		transform.position = position;
		Vector3 dir = (rabbit.position - transform.position).normalized;
		transform.rotation = Quaternion.LookRotation(dir);
		ps.Play();
	}
}
