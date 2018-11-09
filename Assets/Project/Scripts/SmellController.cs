using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellController : MonoBehaviour {

	ParticleSystem ps;

	void Awake () {
		ps = GetComponent<ParticleSystem>();
	}

	void OnTriggerEnter(Collider other) {
		if(ps.isPlaying) {
			ps.Stop();
			Debug.Log("Feel Smell!");
			StartCoroutine(DisableInSeconds(5));
		}
	}

	IEnumerator DisableInSeconds (int delay) {
		while(true) {
			yield return new WaitForSeconds(delay);
			gameObject.SetActive(false);
		}
	}
}
