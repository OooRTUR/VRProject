using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Animal {
	public string name;
	public int count;
	public GameObject prefab;
}
public class SpawnController : MonoBehaviour {

	public Animal[] animals;
	public Transform[] spawnPoints;

	void Start () {
		for (int i = 0; i < animals.Length; i++) {
			for (int a = 0; a < animals [i].count; a++) {
				int spawnerIndex = Random.Range (0, spawnPoints.Length - 1);
				Instantiate (animals [i].prefab, spawnPoints [spawnerIndex].position, Quaternion.identity);
			}
		}
	}
}
