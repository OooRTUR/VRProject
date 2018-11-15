using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjsByCurve : MonoBehaviour {
    
    // поля конфигурации
    [SerializeField] Transform target;
    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;
    [SerializeField] bool DebugMode;
    PlaceObjectToGround place;

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
        graphLength = (int)(Vector3.Distance(transform.position, target.position) / rad) - limit;
        vec3points = new Vector3[graphLength];
        angle = Vec3Mathf.GetAngle(transform, target);
        CalcGraph();
    }

    void Start () {
        place = new PlaceObjectToGround();
        PlaceObjByGraph();
        if (DebugMode)
            StartCoroutine("DebugLines");
    }
	

    IEnumerator DebugLines()
    {
        while (true)
        {
            angle = Vec3Mathf.GetAngle(transform, target);
            Debug.DrawLine(transform.position, new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z), Color.red); // x axis
            Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5.0f), Color.blue); // z axis
            Debug.DrawLine(transform.position, GetVec3(graphLength), Color.cyan); // rotation vector
            CalcGraph();
            DrawGraph();
            yield return new WaitForSeconds(0.01f);
        }
    }

    void PlaceObjByGraph()
    {
        //Debug.Log("graph length: "+graphLength);
        for(int i=0; i <graphLength; i++)
        {
            GameObject tempObj = Instantiate(obj1,transform);
            tempObj.transform.position = vec3points[i];
            Debug.Log(transform.name+" | i: "+i+" | " +vec3points[i]);
            
            place.Place(ref tempObj);

            if(i % 4 == 0)
            {
                GameObject tempObj2 = Instantiate(obj2, transform);
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
            transform.position.x + mod * rad * Mathf.Cos(Mathf.Deg2Rad * (angle+90.0f)),
            transform.position.y,
            transform.position.z + mod * rad * Mathf.Sin(Mathf.Deg2Rad * (angle+90.0f)));
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
        vec3points[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for(int i=1; i < graphLength; i++)
        {
            float dim = Mathf.Sin(Mathf.Rad2Deg * i) * this.dim;
            Vector3 vec3 = CalcVec3(i, 1, dim);
            vec3points[i] = new Vector3(transform.position.x + vec3.x, transform.position.y, transform.position.z + vec3.z);
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

        return new Vector3(x,transform.position.y, z);
    }

}
