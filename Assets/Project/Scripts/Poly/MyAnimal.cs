using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAnimal : MonoBehaviour {

    protected enum Conditions {Walk, Run };
    private Conditions cond;
    protected virtual Conditions Cond { get { return cond; } set { cond = value; } }

    private new string name;
    protected virtual string Name { get { return this.name; } set { this.name = value; } }



    protected virtual void DebugFunc()
    {
        Debug.Log("This is MyAnimal inherited class");
    }
    protected void DebugName()
    {
        Debug.Log(base.name+" | Your animal name is "+Name);
    }
}
