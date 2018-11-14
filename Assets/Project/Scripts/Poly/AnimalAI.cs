using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Poly
{
    public class AnimalAI : MonoBehaviour
    {
        [HideInInspector] public Transform[] SaveZones { get { if (zonesManager.saveZones != null) return zonesManager.saveZones; else return null; } }
        List<Transform> variantsZones = new List<Transform>();
        Transform finalZone;
        AreaOfWalk walkArea;
        AnimalType a_type;
        [SerializeField]ZonesManager zonesManager;
        public Transform FinalZone { get { return finalZone; } }
        protected virtual void Awake()
        {
            walkArea = GetComponent<AreaOfWalk>();
        }
        public void FindSaveZone(Vector3 predatorPos)
        {
            variantsZones.Clear();
            finalZone = null;
            foreach (Transform zone in SaveZones)
            {
                if (Vector3.Angle(-Vec3Mathf.DirectionTo(transform.position,predatorPos), Vec3Mathf.DirectionTo(transform.position,zone.position)) < 90)
                    variantsZones.Add(zone);
            }
            Transform[] variants = variantsZones.ToArray();
            if (variants.Length > 1)
                ChooseZone(variants);
            else
                ChooseZone(SaveZones);
        }

        void ChooseZone(Transform[] zones)
        {
            Transform prevCenter = walkArea.areaCenter;
            foreach (Transform zone in zones)
            {
                if (finalZone == null || Vec3Mathf.DistanceTo(transform.position,zone.position) < Vec3Mathf.DistanceTo(transform.position,finalZone.position))
                {
                    if (prevCenter != zone)
                        finalZone = zone;
                }
            }
            //Debug.Log(finalZone);
            walkArea.areaCenter = finalZone;
        }
    }
}
