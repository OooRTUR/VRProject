using System;
using System.Collections.Generic;
using UnityEngine;

public class ListSetter
{
    static public void SetList(ref List<GameObject> objectsList, Transform spawnContainer)
    {
        for (int i = 0; i < spawnContainer.childCount; i++)
        {
            objectsList.Add(spawnContainer.GetChild(i).gameObject);
        }
    }
}
