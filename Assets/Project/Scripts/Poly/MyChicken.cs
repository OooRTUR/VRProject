using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChicken : MyAnimal {

	// Use this for initialization
	void Start () {

        Cond = Conditions.Run;
        Debug.Log(transform.name+" | "+Cond);
        Name = "My Chicken";
        DebugName();
	}
}
