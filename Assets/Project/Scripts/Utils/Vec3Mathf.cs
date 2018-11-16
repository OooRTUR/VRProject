using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Vec3Mathf
{
    static public Vector3 DirectionTo(Vector3 point1,Vector3 point2)
    {
        Vector3 direction = (point2 - point1).normalized;
        return direction;
    }

    static public float DistanceTo(Vector3 point1, Vector3 point2)
    {
        float distance = Vector3.Distance(point1, point2);
        return distance;
    }

    static public float GetAngle(Transform start, Transform end)
    {
        Vector3 targetDir = end.position - start.position;
        float angle = Vector3.Angle(targetDir, start.forward);
        if (!(Vector3.Angle(start.right, end.position - start.position) > 90f))
            angle = 360.0f - angle;
        return angle;
    }
    static public float GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 targetDir = end - start;
        float angle = Vector3.Angle(targetDir, Vector3.forward);
        if (!(Vector3.Angle(Vector3.right, end - start) > 90f))
            angle = 360.0f - angle;
        return angle;
    }

    static public Vector3 GetReverseDir(Vector3 start, Vector3 end, float length=1)
    {
        float angle = Vec3Mathf.GetAngle(end, start);
        float dirX = start.x + Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad) * length;
        float dirZ = start.z + Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad) * length;
        return new Vector3(dirX, start.y, dirZ);
    }
    static public Vector3 GetReverseDir(Vector3 start, Vector3 end, float length = 1, float alpha = 15)
    {
        float angle = Vec3Mathf.GetAngle(end, start);
        float dirX = start.x + Mathf.Cos((angle + 90.0f + alpha) * Mathf.Deg2Rad) * length;
        float dirZ = start.z + Mathf.Sin((angle + 90.0f + alpha) * Mathf.Deg2Rad) * length;
        return new Vector3(dirX, start.y, dirZ);
    }
}
