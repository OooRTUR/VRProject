using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Poly
{
    public class MouseMotor : AnimalMotor
    {
        //protected override IEnumerator Safety()
        //{
        //    Debug.Log("overrided Safety() started | This is hide method for mouse");
        //    transform.localScale = Vector3.one * 0.2f;
        //    yield return new WaitForSeconds(5);
        //    while (cond == Condition.Safety)
        //    {
        //        //Debug.Log("coroutine Safety() is running");
        //        if (fow.visibleTargets.Count > 0)
        //            yield return new WaitForSeconds(15);
        //        else
        //            break;
        //    }
        //    cond = Condition.Secure;
        //    StartCoroutine("Secure");
        //}
    }
}
