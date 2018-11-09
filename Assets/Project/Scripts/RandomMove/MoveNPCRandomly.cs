using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPCRandomly : MonoBehaviour
{
    public List<Transform> targetList;
    public int targetIndex = -1;

    public Transform target;

    public float rotationSpeed;
    public float moveSpeed;

    CreateTarget createTarget;
    CreateRotation createRot;
    CreateMove createMove;


    [HideInInspector]public bool destinationReached = false;

    

    private void Awake()
    {
        createRot = GetComponent<CreateRotation>();
        createMove = GetComponent<CreateMove>();
        createTarget = GetComponent<CreateTarget>();

        createRot.speed = rotationSpeed;
        createMove.speed = moveSpeed;

        
    }

    private void OnEnable()
    {
        //if (targetIndex < targetList.Count-1)
        //    targetIndex++;
        //else
        //    targetIndex = 0;
        //Debug.Log("targetListIndex: "+ targetIndex);
        //Debug.Log("создание нового направления");
        //createRot.target = targetList[targetIndex];
        //createMove.target = targetList[targetIndex];

        createMove.target = target;
        createRot.target = target;
        createTarget.enabled = true;
        GetComponent<MoveNPCRandomly>().enabled = false;
    }
}
