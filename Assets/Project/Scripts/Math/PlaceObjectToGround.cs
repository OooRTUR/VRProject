using UnityEngine;

//данный скрип на старте игры помещает объект, к которому приатачен скрипт 
//к объекту расположенному ниже по оси Y

public class PlaceObjectToGround : ScriptableObject {

    Transform transform;
    float maxDist = 55.0f;
    int layerMask = 1 << 9;

    public void Place(ref GameObject go)
    {
        transform = go.transform;
        Vector3 dir = transform.TransformDirection(Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, maxDist, layerMask))
        {
            
            Debug.DrawRay(transform.position, dir * hit.distance, Color.green);
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - hit.distance,
                transform.position.z
                );
            Debug.LogFormat("Объект {0} помещен на землю", transform.name);
        }
        else
        {
            Debug.Log("Ground is not hitted");
            Debug.DrawRay(transform.position, dir * maxDist, Color.red);
        }
    }

}
