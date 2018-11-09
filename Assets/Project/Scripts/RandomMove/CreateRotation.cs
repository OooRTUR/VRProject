using System;
using System.Collections.Generic;
using UnityEngine;

class CreateRotation : MonoBehaviour
{

    public Transform target;
    public float speed;

    Vector3 targetDir;
    Vector3 rotation;

    private void Update()
    {
        targetDir = target.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        rotation = new Vector3(transform.rotation.x, angle, transform.rotation.y);
        Debug.Log(angle);
        if (angle > 5.0f)
        {
            transform.Rotate(rotation * Time.deltaTime * speed);
        }
        else
        {
            Debug.Log("Угол вращения достигнут");
        }
    }
}
