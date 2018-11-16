﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RabbitMotor : AnimalMotor
{
    protected override IEnumerator Alarm()
    {
        float time = 0.0f;
        float chaseTime = 0.0f;
        Debug.Log("overrided Alarm() started | this is inherited method from AnimalMotor");
        transform.localScale = Vector3.one;
        agent.speed = 25.0f;
        float randomSec = Random.Range(0.5f, 0.6f);
        while (cond == Condition.Alarm)
        {
            time += Time.deltaTime;
            chaseTime += Time.deltaTime;
            Debug.Log(chaseTime);
            //Debug.Log(time);
            if (time >= randomSec)
            {
                float randomAngle = Random.Range(-15.0f, 15.0f);
                Debug.Log("Задаем новою точку перемещения");
                agent.SetDestination(Vec3Mathf.GetReverseDir(transform.position, visibleTarget.position, 120.0f, randomAngle));
                time = 0.0f;
                randomSec = Random.Range(0.5f, 0.6f);
            }
            if (fow.visibleTargets.Count == 0 && chaseTime > 1.5f)
                break;
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("changing condition to alarm");
        visibleTarget = null;
        ChangeCondition(Condition.Secure, "Alarm", "Secure");
    }
}