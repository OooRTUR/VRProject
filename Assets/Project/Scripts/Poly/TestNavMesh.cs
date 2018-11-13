using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavMesh : MonoBehaviour
{
    Transform trans;
    NavMeshAgent agent;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        
    }
    bool flag = true;
    private void Update()
    {
        if (flag)
        {
            agent.SetDestination(Vector3.zero);
            flag = false;
        }
    }
}
