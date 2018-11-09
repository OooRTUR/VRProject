using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = Vector3.Cross(transform.position,Vector3.up);
	}

    Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        // Find vectors corresponding to two of the sides of the triangle.
        Vector3 side1 = b - a;
        Vector3 side2 = c - a;

        // Cross the vectors to get a perpendicular vector, then normalize it.
        return Vector3.Cross(side1, side2).normalized;
    }
}
