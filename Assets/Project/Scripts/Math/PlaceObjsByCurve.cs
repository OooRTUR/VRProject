using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjsByCurve : ScriptableObject {

    // поля конфигурации
    [SerializeField] Transform start;
    [SerializeField] Transform target;
    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;
    [SerializeField] bool DebugMode;
    PlaceObjectToGround place;


    Vector3 startPos;
    Vector3 targPos;
    // расчетные поля
    [SerializeField] float rad = 1.0f;
    [SerializeField] float dim = 0.1f;
    readonly float sqrt2 = 1.141f;
    float angle = 0.0f;
    int graphLength = 0;
    readonly int limit = 0;
    Vector3[] vec3points;


    private void Awake()
    {
        startPos = start.position;
        targPos = target.position;

        graphLength = (int)(Vector3.Distance(startPos, targPos) / rad) - limit;
        vec3points = new Vector3[graphLength];
        angle = Vec3Mathf.GetAngle(start, target);
        CalcGraph();
    }

    void Start () {
        PlaceObjByGraph();
        if (DebugMode)
            StartCoroutine("DebugLines");
    }
	

    public IEnumerator DebugLines()
    {
        while (true)
        {
            angle = Vec3Mathf.GetAngle(start, target);
            Debug.DrawLine(start.position, new Vector3(start.position.x + 5.0f, start.position.y, start.position.z), Color.red); // x axis
            Debug.DrawLine(start.position, new Vector3(start.position.x, start.position.y, start.position.z + 5.0f), Color.blue); // z axis
            Debug.DrawLine(start.position, GetVec3(graphLength), Color.cyan); // rotation vector
            CalcGraph();
            DrawGraph();
            yield return new WaitForSeconds(0.01f);
        }
    }

    void PlaceObjByGraph()
    {
        place = new PlaceObjectToGround();
        //Debug.Log("graph length: "+graphLength);
        for (int i=0; i <graphLength; i++)
        {
            GameObject tempObj = Instantiate(obj1, start);
            tempObj.transform.position = vec3points[i];
            //Debug.Log(transform.name+" | i: "+i+" | " +vec3points[i]);
            
            place.Place(ref tempObj);

            if(i % 4 == 0)
            {
                GameObject tempObj2 = Instantiate(obj2, start);
                tempObj2.transform.position = vec3points[i];
                place.Place(ref tempObj2);
                tempObj2.transform.position = new Vector3(tempObj2.transform.position.x, tempObj2.transform.position.y + 4.5f, tempObj2.transform.position.z);
            }
        }
    }

    //методы отображения

    Vector3 GetVec3(float mod)
    {
        return new  Vector3(
            start.position.x + mod * rad * Mathf.Cos(Mathf.Deg2Rad * (angle+90.0f)),
            start.position.y,
            start.position.z + mod * rad * Mathf.Sin(Mathf.Deg2Rad * (angle+90.0f)));
    }
    void DrawGraph()
    {
        for(int i=0; i < graphLength; i++)
        {
            Debug.DrawLine(GetVec3(i), vec3points[i], Color.red);
        }
    }

    // расчетные методы

    void CalcGraph()
    {
        vec3points[0] = new Vector3(start.position.x, start.position.y, start.position.z);
        for(int i=1; i < graphLength; i++)
        {
            float dim = Mathf.Sin(Mathf.Rad2Deg * i) * this.dim;
            Vector3 vec3 = CalcVec3(i, 1, dim);
            vec3points[i] = new Vector3(start.position.x + vec3.x, start.position.y, start.position.z + vec3.z);
        }
    }

    Vector3 CalcVec3(float radmod, float modifier, float dim)
    {
        
        float radX = rad * radmod;
        float x, y, z;
        float angle_s= Mathf.Atan(dim / radX);
        float c = Mathf.Sqrt(dim * dim + radX * radX);

        x = c * Mathf.Cos(Mathf.Deg2Rad*(angle+90.0f) + angle_s * modifier);
        z = c * Mathf.Sin(Mathf.Deg2Rad * (angle+90.0f) + angle_s * modifier);

        return new Vector3(x, start.position.y, z);
    }

}
