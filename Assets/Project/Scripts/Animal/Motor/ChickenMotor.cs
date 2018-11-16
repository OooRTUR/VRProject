using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChickenMotor : AnimalMotor
{
    [SerializeField] Transform escapePoint;

    protected override IEnumerator Alarm()
    {
        float time = 0.0f;
        float escape_time = 0.0f;
        //Debug.Log("overrided Alarm() started | this is inherited method from AnimalMotor");
        transform.localScale = Vector3.one;
        agent.speed = runSpeed;
        float randomSec = Random.Range(0.2f, 0.3f);
        agent.SetDestination(ai.GetWalkPoint());
        while (cond == Condition.Alarm)
        {
            time += Time.deltaTime;
            escape_time += Time.deltaTime;
            if (time >= randomSec)
            {
                //Debug.Log("Задаем новою точку перемещения");
                agent.SetDestination(ai.GetWalkPoint());
                time = 0.0f;
            }
            if (fow.visibleTargets.Count > 0 && escape_time > 1.0f)
                break;
            yield return new WaitForSeconds(0.05f);
        }
        escape_time = 0.0f;
        //Debug.Log("changing condition to alarm");
        visibleTarget = null;
        ChangeCondition(Condition.Safety, "Alarm", "Safety");
    }
    protected override IEnumerator Safety()
    {
        Vector3 lastPosition = agent.destination;
		agent.Warp (escapePoint.position);
        //agent.Warp(escapePoint.position);
        agent.ResetPath();
        //Debug.Log("Улетаю!");

        yield return new WaitForSeconds(4.0f);
		agent.Warp (lastPosition);
        //agent.Warp(lastPosition);
        agent.ResetPath();
        //Debug.Log("Возвращаюсь на исходную позицию");
        ChangeCondition(Condition.Secure, "Safety", "Secure");
    }
}