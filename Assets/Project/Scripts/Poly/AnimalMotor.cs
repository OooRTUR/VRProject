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
        AnimalAI ai;

        public enum Condition { Secure, Alarm, Safety}
        public Condition cond;

        [SerializeField] protected AnimalType animalType;
        [HideInInspector] public Transform[] saveZones;
        float timeAfterSpot;
    
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
        private void Update()
        {
            //Debug.Log("is enemy spotted? : " + isEnemySpotted);
            //CheckPointDestination();
            //Debug.Log(cond);


            if (fow.isEnemySpotted && cond == Condition.Secure)
            {
                Debug.Log("changing condition to alarm");
                cond = Condition.Alarm;
                StopCoroutine("Secure");
                StartCoroutine("Alarm");
            }
            if(cond == Condition.Alarm)
            {
                timeAfterSpot += Time.deltaTime;
                Debug.Log(timeAfterSpot);
            }
        }


        void CheckPointDestination()
        {
            if (cond == Condition.Alarm && agent.remainingDistance < 0.5f)
            {
                agent.Warp(agent.pathEndPosition);
                agent.ResetPath();
                //cond = Condition.Secure;
                StartCoroutine("Secure");
            }
        }
        public void SawPredator(Vector3 runTo)
        {
            //cond = Condition.Alarm;
            //StopCoroutine("Secure");
            //agent.ResetPath();
            //agent.SetDestination(runTo);
        }
        // состояние безопасности, нормальная скорость перемещения
        protected virtual IEnumerator Secure()
        {
            Debug.Log("base Secure() started | this is base method for all AnimalMotor");
            transform.localScale = Vector3.one;
            agent.speed = walkSpeed;
            while (true)
            {
                agent.SetDestination(walkArea.GetWalkPoint()); // задать случайную точку перемещения
                float randomSec = Random.Range(2.5f, 6.5f);
                yield return new WaitForSeconds(randomSec);
            }
        }
        // состояние опасности
        protected virtual IEnumerator Alarm()
        {
            //cond = Condition.Alarm;
            Debug.Log("base Alarm() started | This is hide method for mouse");
            
            agent.speed = runSpeed;
            while (true)
            {
                //Debug.Log("is in alarm now");
                Debug.Log("finalzone: " + ai.FinalZone);
                agent.SetDestination(walkArea.GetWalkPoint());
                yield return new WaitForSeconds(0.1f);
            }
        }
        // состояние защиты
        protected virtual IEnumerator Safety()
        {
            Debug.Log("base Safety() started | This is hide method for mouse");
            transform.localScale = Vector3.one * 0.2f;
            yield return new WaitForSeconds(5);
            while (cond == Condition.Safety)
            {
                if (fow.visibleTargets.Count > 0)
                    yield return new WaitForSeconds(5);
                else
                    break;
            }
            //cond = Condition.Secure;
            //StartCoroutine("Secure");
            yield return null;
        }

    }

    [Serializable]
    public struct AnimalType
    {
        public enum Animal { Animal, Mouse, Chicken, Rabbit }
        public Animal type;
    }
}