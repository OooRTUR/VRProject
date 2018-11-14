using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

// базовые состояния для всех AnimalMotor InSecurity (безопасность) и InAlarm (угроза),
// если поведение объекта отличается от InAlarm (например объект вместо бегства объект пытается скрыться),
// то достаточно переписать метод InAlarm в производном классе

namespace Poly { 

    public class AnimalMotor : MonoBehaviour
    {
        protected float walkSpeed = 3f;
        protected float runSpeed = 12f;

        protected NavMeshAgent agent;
        protected AreaOfWalk walkArea;
        protected FieldOfView fow;
        protected AnimalType a_type;
        protected AnimalAI ai;
        protected Transform visibleTarget;


        public enum Condition { Secure, Alarm, Safety}
        public Condition cond;

        [SerializeField] protected AnimalType animalType;
        [HideInInspector] public Transform[] saveZones;
    
        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            walkArea = GetComponent<AreaOfWalk>();
            fow = GetComponent<FieldOfView>();
            ai = GetComponent<AnimalAI>();
        }
        protected virtual void Start()
        {
            StartCoroutine("Secure");
        }
        // состояние безопасности, нормальная скорость перемещения
        protected virtual IEnumerator Secure()
        {
            float time = 0.0f;
            Debug.Log("base Secure() started | this is base method from AnimalMotor");
            transform.localScale = Vector3.one;
            agent.speed = walkSpeed;
            float randomSec = Random.Range(1.5f, 3.5f);
            agent.SetDestination(walkArea.GetWalkPoint());
            while (cond == Condition.Secure)
            {
                time += Time.deltaTime;
                //Debug.Log(time);
                if (time >= randomSec)
                {
                    Debug.Log("Задаем новою точку перемещения");
                    agent.SetDestination(walkArea.GetWalkPoint());
                    time = 0.0f;
                }
                if (fow.visibleTargets.Count > 0)
                    break;
                yield return new WaitForSeconds(0.1f);
            }
            Debug.Log("changing condition to alarm");
            visibleTarget = fow.visibleTargets[0];
            ChangeCondition(Condition.Alarm, "Secure", "Alarm");
        }
        // состояние опасности
        protected virtual IEnumerator Alarm()
        {
            yield return null;    
        }
        // состояние защиты
        protected virtual IEnumerator Safety()
        {
            yield return null;
        }
        protected void ChangeCondition(Condition targetCond, string currentCoroutine, string targetCoroutine)
        {
            this.cond = targetCond;
            StartCoroutine(targetCoroutine);
            StopCoroutine(currentCoroutine);
        }

    }

    [Serializable]
    public struct AnimalType
    {
        public enum Animal { Animal, Mouse, Chicken, Rabbit }
        public Animal type;
    }
}