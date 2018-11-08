using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellController : MonoBehaviour {

	ParticleSystem ps;

	void Awake () {
		ps = GetComponent<ParticleSystem>();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("Feel Smell!");
		ps.Stop();
	}
}
