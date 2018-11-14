using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesManager : MonoBehaviour {

    [HideInInspector] public Transform[] saveZones;

    private void Awake()
    {
        saveZones = new Transform[transform.childCount];
        for (int i = 0; i < saveZones.Length; i++)
        {
            saveZones[i] = transform.GetChild(i);
            //Debug.Log(saveZoneTag +" | " + saveZones[i].name);
        }
    }
}
