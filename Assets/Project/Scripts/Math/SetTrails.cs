using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetTrails : MonoBehaviour {
	
	public Transform startTransform;
	public Transform endTransform;
	NavMeshPath path;

    Vector3[] pathCorners;

	LineRenderer line;

    [SerializeField]bool debugMode;

    private void Awake()
    {
        path = new NavMeshPath();
        NavMesh.CalculatePath(startTransform.position, endTransform.position, NavMesh.AllAreas, path);
        pathCorners = new Vector3[path.corners.Length];
        pathCorners = path.corners;
        if (debugMode)
        {
            line = GetComponent<LineRenderer>();
            StartCoroutine("DrawPath");
        }
    }


    IEnumerator DrawPath()
    {
        while (true)
        {
            NavMesh.CalculatePath(startTransform.position, endTransform.position, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            //for (int i = 0; i < line.positionCount; i++)
            //{
            //    Vector3 vectory = new Vector3(line.GetPosition(i).x, line.GetPosition(i).y + 10, line.GetPosition(i).z);
            //    Debug.DrawLine(line.GetPosition(i), vectory);
            //}
            for(int i=0; i < pathCorners.Length; i++)
            {
                Vector3 vec3 = new Vector3(pathCorners[i].x, pathCorners[i].y + 10.0f, pathCorners[i].z);
                Debug.DrawLine(pathCorners[i], vec3);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
