using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetTrails : MonoBehaviour {

    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;
    List<float> angles;

    List<PlaceObjsByCurve> placeByCurve;
	public Transform startTransform;
	public Transform endTransform;
	NavMeshPath path;

    Vector3[] pathCorners;

	LineRenderer line;

    [SerializeField]bool debugMode;

    private void Awake()
    {
        path = new NavMeshPath();
        placeByCurve = new List<PlaceObjsByCurve>();
        
        

        NavMesh.CalculatePath(startTransform.position, endTransform.position, NavMesh.AllAreas, path);
        pathCorners = new Vector3[path.corners.Length];
        pathCorners = path.corners;
        

        if (debugMode)
        {
            line = GetComponent<LineRenderer>();
            StartCoroutine("DrawPath");
        }
    }
    private void Start()
    {
        //Debug.Log(path.corners.Length);
        float uplift = 10.0f;
        angles = new List<float>();
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            //Debug.Log(i);
            //Debug.Log(pathCorners[i].y + " | " + pathCorners[i+1].y);
            placeByCurve.Add(ScriptableObject.CreateInstance<PlaceObjsByCurve>());
            Vector3 nullY1 = new Vector3(pathCorners[i].x, 0.0f + uplift, pathCorners[i].z);
            Vector3 nullY2 = new Vector3(pathCorners[i + 1].x, 0.0f + uplift, pathCorners[i + 1].z);
            placeByCurve[i].Run(
                nullY1,
                nullY2);
            placeByCurve[i].PlaceObjByGraph(obj1, obj2);
            StartCoroutine(placeByCurve[i].DebugLines());
        }

    }


    IEnumerator DrawPath()
    {
        while (true)
        {
            NavMesh.CalculatePath(startTransform.position, endTransform.position, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            for (int i = 0; i < line.positionCount-1; i++)
            {

                Vector3 vectory = new Vector3(line.GetPosition(i).x, line.GetPosition(i).y + 10, line.GetPosition(i).z); // draw y
                Debug.DrawLine(line.GetPosition(i), vectory);

                //Vector3 x = new Vector3(line.GetPosition(i).x, 0.0f, 0.0f); // draw x
                //Debug.DrawLine(line.GetPosition(i), x, Color.red);

                //Vector3 z = new Vector3(0.0f, 0.0f, line.GetPosition(i).z); // draw z
                //Debug.DrawLine(line.GetPosition(i), z, Color.blue);
            }
            //for (int i=0; i < pathCorners.Length; i++)
            //{
            //    Vector3 vec3 = new Vector3(pathCorners[i].x, pathCorners[i].y + 10.0f, pathCorners[i].z);
            //    Debug.DrawLine(pathCorners[i], vec3);
            //}
            yield return new WaitForSeconds(0.01f);
        }
    }
}
