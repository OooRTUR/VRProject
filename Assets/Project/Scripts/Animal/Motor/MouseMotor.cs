using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MouseMotor : AnimalMotor
{

    // состояние опасности
    protected override IEnumerator Alarm()
    {
        //cond = Condition.Alarm;
        //Debug.Log("override Alarm() started | this is inherited method from AnimalMotor");
        agent.speed = runSpeed;
        ai.FindSaveZone(visibleTarget.position);
        agent.ResetPath();
        agent.SetDestination(ai.FinalZone.position);
        while (cond == Condition.Alarm)
        {
            yield return new WaitForSeconds(0.5f);
            //Debug.Log(ai.FinalZone.name);
            if (agent.remainingDistance < 0.5f)
                break;
            yield return new WaitForSeconds(0.2f);
        }
        agent.Warp(agent.pathEndPosition);
        agent.ResetPath();
        ChangeCondition(Condition.Safety, "Alarm", "Safety");
    }
    // состояние защиты
    protected override IEnumerator Safety()
    {
        float delay = 3.5f;
        //Debug.Log("override Safety() started | this is inherited method from AnimalMotor");
        transform.localScale = Vector3.one * 0.2f;
        yield return new WaitForSeconds(delay);
        while (cond == Condition.Safety)
        {
            if (fow.visibleTargets.Count > 0)
                yield return new WaitForSeconds(delay);
            else
                break;
        }
        ChangeCondition(Condition.Secure, "Safety", "Secure");

    }
}
