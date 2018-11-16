using UnityEngine;
using System.Collections;

namespace test
{
    public class GetRandomDir : MonoBehaviour
    {
        float alpha;
        float length;
        // расчетные поля
        float rad = 2.5f;
        float dim = 2.0f;
        readonly float sqrt2 = 1.141f;
        float angle = 0.0f;
        float graphLength = 0;
        Vector3[] vec3points;



        [SerializeField]Transform victimTrans;
        [SerializeField] Transform predatorTrans;

        Vector3 victimPos;
        Vector3 predatorPos;

        private void Awake()
        {
            victimPos = victimTrans.transform.position;
            predatorPos = predatorTrans.transform.position;
            alpha = Random.Range(-30.0f, 30.0f);
            length = Random.Range(10.0f, 55.0f);

            graphLength = Vector3.Distance(victimPos, predatorPos) / rad;
            vec3points = new Vector3[(int)graphLength];
            angle = Vec3Mathf.GetAngle(victimPos, predatorPos);

            CalcGraph();
            StartCoroutine(DebugLines());
        }
        IEnumerator DebugLines()
        {
            while (true)
            {
                victimPos = victimTrans.transform.position;
                predatorPos = predatorTrans.transform.position;
                Debug.DrawLine(victimPos,new Vector3(victimPos.x, victimPos.y, victimPos.z+5.0f), Color.blue);
                Debug.DrawLine(victimPos, new Vector3(victimPos.x+1.0f, victimPos.y, victimPos.z), Color.red);
                Debug.DrawLine(predatorPos, new Vector3(predatorPos.x, predatorPos.y, predatorPos.z + 5.0f), Color.blue);
                Debug.DrawLine(predatorPos, new Vector3(predatorPos.x+1.0f, predatorPos.y, predatorPos.z), Color.red);

                //Vector3 dir = victimPos - predatorPos;
                //float dist = Vector3.Distance(predatorPos, victimPos);
                //angle = Vec3Mathf.GetAngle(victimPos, predatorPos);
                //float c = Mathf.Sqrt(dim * dim + 1.0f * 1.0f);
                //float angle_s = Mathf.Atan(dim / 4.0f);
                //float x = dist * Mathf.Cos(Mathf.Deg2Rad * (angle + 90.0f));
                //float z = dist * Mathf.Sin(Mathf.Deg2Rad * (angle + 90.0f));
                //Debug.DrawLine(victimPos, new Vector3(x,0.0f,z),Color.red);
                //Debug.DrawLine(predatorPos, victimPos, Color.cyan);
                Vector3 dir = GetDir(victimPos, predatorPos);



                Debug.DrawLine(victimPos, dir,Color.cyan);

                
                //CalcGraph();
                //DrawGraph();

                yield return null;
            }
        }
        Vector3 GetDir(Vector3 start, Vector3 end)
        {
            float angle = Vec3Mathf.GetAngle(end, start);
            float dirX = start.x +  Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad)*length;
            float dirZ = start.z +  Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad)* length;
            Vector3 dir = new Vector3(dirX, start.y, dirZ);

            float dirAX = start.x + Mathf.Cos((angle + 90.0f+alpha) * Mathf.Deg2Rad) * length;
            float dirAZ = start.z + Mathf.Sin((angle + 90.0f+alpha) * Mathf.Deg2Rad) * length;
            Vector3 dirA = new Vector3(dirAX, start.y, dirAZ);

            //float dirA1X = start.x + Mathf.Cos((angle + 90.0f - alpha) * Mathf.Deg2Rad) * length;
            //float dirA1Z = start.z + Mathf.Sin((angle + 90.0f - alpha) * Mathf.Deg2Rad) * length;
            //Vector3 dir1A = new Vector3(dirA1X, start.y, dirA1Z);

            Debug.DrawLine(start, dirA, Color.yellow);
            //Debug.DrawLine(start, dir1A, Color.magenta);

            return dir;
        }

        void DrawGraph()
        {
            for (int i = 0; i < vec3points.Length; i++)
            {
                Debug.DrawLine(GetVec3(i), vec3points[i], Color.red);
            }
        }

        Vector3 GetVec3(float mod)
        {
            return new Vector3(
                victimPos.x + mod * rad * Mathf.Cos(Mathf.Deg2Rad * (angle + 90.0f)),
                victimPos.y,
                victimPos.z + mod * rad * Mathf.Sin(Mathf.Deg2Rad * (angle + 90.0f)));
        }

        public void CalcGraph()
        {
            vec3points[0] = new Vector3(victimPos.x, victimPos.y, victimPos.z);
            for (int i = 1; i < vec3points.Length; i++)
            {
                Vector3 vec3 = CalcVec3(i, 1, 1.0f);
                vec3points[i] = new Vector3(victimPos.x + vec3.x, victimPos.y, victimPos.z + vec3.z);
            }
        }

        Vector3 CalcVec3(float radmod, float modifier, float dim)
        {

            float radX = rad * radmod;
            float x, y, z;
            float angle_s = Mathf.Atan(dim / radX);
            float c = Mathf.Sqrt(dim * dim + radX * radX);

            x = c * Mathf.Cos(Mathf.Deg2Rad * (angle + 90.0f) + angle_s * modifier);
            z = c * Mathf.Sin(Mathf.Deg2Rad * (angle + 90.0f) + angle_s * modifier);

            return new Vector3(x, victimPos.y, z);
        }


    }
}
