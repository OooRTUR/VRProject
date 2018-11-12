using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec3Math : MonoBehaviour {

    readonly float sqrt2 = 1.141f;

    [SerializeField] float rad = 1.0f;
    [SerializeField] float angle = 45.0f;
    [SerializeField] float dim = 0.1f;
    int graphLength = 60;

    DrawCircle drawCircle1;
    Vector3 vec3;
    Vector3 yVec3;
    Vector3 normVec3;
    Vector3 bis;
    Vector3 pointZero;

    Vector3 vec_1a;
    Vector3 vec_2a;
    Vector3 vec_3a;

    Vector3[] vec3points;
    Vector3[] vec3points2;

    void Start () {
        drawCircle1 = new DrawCircle(GetComponent<LineRenderer>());
        
        
        yVec3 = new Vector3(
            0.0f,
            Vector3.up.y * sqrt2,
            0.0f);
        
        //normVec3 = vec3 + yVec3;
        //vec_1a = CalcVec3(1,1);
        //vec_2a =  CalcVec3(2,1);

        vec3points = new Vector3[graphLength];
        vec3points2 = new Vector3[graphLength];


    }
	
	void Update () {
        drawCircle1.Draw();


        

        bis = new Vector3(
            transform.position.x +  1.0f * Mathf.Cos(Mathf.Deg2Rad * 45.0f),
            0.0f,
            transform.position.z +  1.0f * Mathf.Sin(Mathf.Deg2Rad * 45.0f)
            );

        Debug.DrawLine(transform.position, new Vector3(transform.position.x + 1.0f,0.0f, transform.position.z), Color.red); // x axis
        Debug.DrawLine(transform.position, new Vector3(transform.position.x,0.0f, transform.position.z+1.0f), Color.blue); // z axis
        Debug.DrawLine(transform.position, bis, Color.yellow);
        Debug.DrawLine(transform.position, GetVec3(2.0f),Color.cyan);
        //Debug.DrawLine(transform.position, GetVec3(2.0f), Color.cyan);
        //Debug.DrawLine(transform.position, GetVec3(3.0f), Color.cyan);
        //Debug.DrawLine(transform.position, GetVec3(4.0f), Color.cyan);

        //Debug.DrawLine(transform.position, vec3 * 0.0f, Color.cyan);

        //vec_1a = transform.position +  CalcVec3(1,1,1);
        //Debug.DrawLine(transform.position, vec_1a, Color.cyan);
        //Debug.DrawLine(GetVec3(1.0f),vec_1a,Color.yellow);

        //vec_2a = transform.position + CalcVec3(2, 1, 1);
        //Debug.DrawLine(transform.position, vec_2a, Color.cyan);
        //Debug.DrawLine(GetVec3(2.0f), vec_2a, Color.yellow);

        CalcGraph();
        DrawGraph();

        //Debug.DrawLine(Vector3.zero, vec3*4, Color.cyan);
        //Debug.DrawLine(Vector3.zero, vec_2a, Color.red);
        //Debug.DrawLine(vec3 * 1, vec_1a, Color.grey);
        //Debug.DrawLine(vec3 * 2, vec_2a, Color.grey);
        //Debug.DrawLine(vec3, yVec3, Color.yellow);
    }
    Vector3 GetVec3(float mod)
    {
        return new  Vector3(
            transform.position.x + mod * rad * Mathf.Cos(Mathf.Deg2Rad * angle),
            0.0f,
            transform.position.z + mod * rad * Mathf.Sin(Mathf.Deg2Rad * angle));
    }
    void DrawGraph()
    {
        Debug.DrawLine(Vector3.zero, vec3*vec3points.Length, Color.red);
        for(int i=0; i <vec3points.Length; i++)
        {
            Debug.DrawLine(GetVec3(i), vec3points[i], Color.grey);
        }
    }

    void CalcGraph()
    {
        for(int i=0; i < graphLength; i++)
        {
            float dim = Mathf.Sin(Mathf.Rad2Deg * i);
            vec3points[i] = transform.position +  CalcVec3(i, 1, dim);
        }
    }

    Vector3 CalcVec3(float radmod, float modifier, float dim)
    {
        
        float radX = rad * radmod;
        float x, y;
        float angle_s= Mathf.Atan(dim / radX);
        float c = Mathf.Sqrt(dim * dim + radX * radX);

        x = c * Mathf.Cos(Mathf.Deg2Rad*(angle) + angle_s * modifier);
        y = c * Mathf.Sin(Mathf.Deg2Rad * (angle) + angle_s * modifier);

        return new Vector3(x,0.0f, y);
    }

}
