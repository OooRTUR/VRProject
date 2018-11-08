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
			StartCoroutine(DisableInSeconds(2));
		}
	}

	IEnumerator DisableInSeconds (int seconds) {
		while(true) {
			yield return new WaitForSeconds(seconds);
			gameObject.SetActive(false);
		}
	}
}
