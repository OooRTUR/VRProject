using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AnimalAI : MonoBehaviour
{
    [HideInInspector] public Transform[] SaveZones { get { if (zonesManager.saveZones != null) return zonesManager.saveZones; else return null; } }
    List<Transform> variantsZones = new List<Transform>();
    Transform finalZone;

    public float walkRadius;
    [HideInInspector] public Transform areaCenter;


    [SerializeField]ZonesManager zonesManager;
    public Transform FinalZone { get { return finalZone; } }

    public virtual void FindSaveZone(Vector3 predatorPos)
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
        Transform prevCenter = areaCenter;
        foreach (Transform zone in zones)
        {
            if (finalZone == null || Vec3Mathf.DistanceTo(transform.position,zone.position) < Vec3Mathf.DistanceTo(transform.position,finalZone.position))
            {
                if (prevCenter != zone)
                    finalZone = zone;
            }
        }
        //Debug.Log(finalZone);
        areaCenter = finalZone;
    }

    public void FindAreaCenter()
    {
        foreach (Transform trans in SaveZones)
        {
            //Debug.Log(trans.name);
            //Debug.Log(areaCenter);
            if (areaCenter == null || Vec3Mathf.DistanceTo(transform.position, trans.position) < Vec3Mathf.DistanceTo(transform.position, areaCenter.position))
                areaCenter = trans;
        }
    }

    public Vector3 GetWalkPoint()
    {
        float x = Random.Range(-walkRadius, walkRadius);
        float z = Random.Range(-walkRadius, walkRadius);
        Vector3 waypoint = new Vector3(x + areaCenter.position.x, areaCenter.transform.position.y, z + areaCenter.position.z);

        return waypoint;
    }
}