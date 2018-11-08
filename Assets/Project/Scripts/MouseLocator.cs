using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocator : MonoBehaviour {

	Transform mouseTrans;
	AudioSource s_source;

	void Awake () {
		mouseTrans = GameObject.FindGameObjectWithTag("Mouse").GetComponent<Transform>();
		s_source = GetComponent<AudioSource>();
		//s_source.clip = mouseSound;
	}

	void OnEnable () {
		StartCoroutine("HearMouse");
	}

	void OnDisable () {
		StopCoroutine("HearMouse");
	}

	IEnumerator HearMouse () {
		while (true) {
			FindMouse();
			yield return new WaitForSeconds(2);
		}
	}

	void FindMouse () {
		Vector3 dir = mouseTrans.position - transform.position;
		float angle = Vector3.Angle(transform.forward, dir);
		if (angle < 20) s_source.volume = 1;
		else s_source.volume = 0.3f;
		Debug.Log(angle);
		//s_source.Play();
	}


}
