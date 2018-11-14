﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Poly
{
    public class AnimalAI : MonoBehaviour
    {
        [HideInInspector] public Transform[] saveZones;
        List<Transform> variantsZones = new List<Transform>();
        Transform finalZone;
        AreaOfWalk walkArea;
        AnimalType a_type;
        public Transform FinalZone { get { return finalZone; } }
        protected virtual void Awake()
        {
            walkArea = GetComponent<AreaOfWalk>();
        }

        public void Init(string saveZoneTag)
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag(saveZoneTag);
            saveZones = new Transform[obj.Length];
            for (int i = 0; i < saveZones.Length; i++)
            {
                saveZones[i] = obj[i].transform;
                //Debug.Log(saveZoneTag +" | " + saveZones[i].name);
            }
        }
        public void FindSaveZone(Vector3 predatorPos)
        {
            variantsZones.Clear();
            finalZone = null;
            foreach (Transform zone in saveZones)
            {
                if (Vector3.Angle(-DirectionTo(predatorPos), DirectionTo(zone.position)) < 90)
                    variantsZones.Add(zone);
            }
            Transform[] variants = variantsZones.ToArray();
            if (variants.Length > 1)
                ChooseZone(variants);
            else
                ChooseZone(saveZones);
        }

        void ChooseZone(Transform[] zones)
        {
            Transform prevCenter = walkArea.areaCenter;
            foreach (Transform zone in zones)
            {
                if (finalZone == null || DistanceTo(zone.position) < DistanceTo(finalZone.position))
                {
                    if (prevCenter != zone)
                        finalZone = zone;
                }
            }
            //Debug.Log(finalZone);
            //motor.SawPredator(finalZone.position);
            walkArea.areaCenter = finalZone;
        }

        Vector3 DirectionTo(Vector3 position)
        {
            Vector3 direction = (position - transform.position).normalized;
            return direction;
        }

        public float DistanceTo(Vector3 position)
        {
            float distance = Vector3.Distance(transform.position, position);
            return distance;
        }
    }
}
