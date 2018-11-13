using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMouse : MyAnimal {

    

    private void Start()
    {
        Cond = Conditions.Walk;
        Debug.Log(transform.name + " | "+ Cond);
        Name = "Mousy!";
        DebugName();
    }

    protected override void DebugFunc()
    {
        Debug.Log("this is mouse");
    }
}
