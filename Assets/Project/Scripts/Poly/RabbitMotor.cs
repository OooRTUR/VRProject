using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Poly
{
    public class RabbitMotor : AnimalMotor
    {
        protected override void Awake()
        {
            
            base.Awake();
            animalType.type = AnimalType.Animal.Rabbit;
            ai.Init("RabbitPoint");
        }
        protected override void Start()
        {
            walkArea.FindAreaCenter();
            base.Start();
        }

        protected override IEnumerator Alarm()
        {
            float time = 0.0f;
            Debug.Log("overrided Alarm() started | this is inherited method from AnimalMotor");
            transform.localScale = Vector3.one;
            agent.speed = 25.0f;
            float randomSec = Random.Range(0.2f, 0.3f);
            agent.SetDestination(walkArea.GetWalkPoint());
            while (cond == Condition.Alarm)
            {
                time += Time.deltaTime;
                //Debug.Log(time);
                if (time >= randomSec)
                {
                    Debug.Log("Задаем новою точку перемещения");
                    agent.SetDestination(walkArea.GetWalkPoint());
                    time = 0.0f;
                }
                if (fow.visibleTargets.Count == 0)
                    break;
                yield return new WaitForSeconds(0.05f);
            }
            Debug.Log("changing condition to alarm");
            visibleTarget = null;
            ChangeCondition(Condition.Secure, "Alarm", "Secure");
        }
    }
}
