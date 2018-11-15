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

    static public float GetAngle(Transform point1, Transform point2)
    {
        Vector3 targetDir = point2.position - point1.position;
        float angle = Vector3.Angle(targetDir, point1.forward);
        if (!(Vector3.Angle(point1.right, point2.position - point1.position) > 90f))
            angle = 360.0f - angle;
        return angle;
    }
}
