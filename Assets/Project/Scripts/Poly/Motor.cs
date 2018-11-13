using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    protected string saveZoneTag;

    protected AreaOfWalk walkArea;
	protected AnimalMotor motor;
    [SerializeField]
	protected Poly.AnimalType animalType;

    [HideInInspector] public Transform[] saveZones;

    void Awake()
    {
        animalType = new Poly.AnimalType();
        walkArea = GetComponent<AreaOfWalk>();
        motor = GetComponent<AnimalMotor>();
        animalType.type = Poly.AnimalType.Animal.Animal;
        //Init();
    }

    void Init()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag(saveZoneTag);
        saveZones = new Transform[obj.Length];
        for (int i = 0; i < saveZones.Length; i++)
        {
            saveZones[i] = obj[i].transform;
        }
    }
}

namespace Poly
{
    [Serializable]
    public struct AnimalType
    {
        public enum Animal { Animal,Mouse, Chicken, Rabbit }
        public Animal type;
    }
}