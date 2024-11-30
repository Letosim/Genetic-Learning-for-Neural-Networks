using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItAgain : MonoBehaviour
{
    public Vector3 origin = Vector3.forward;
    public Vector3 targetPosition = new Vector3(1, 0, 1);

    public Vector3 angle;

    // Start is called before the first frame update
    void Start()
    {
        for (float i = 0; i < 360; i += 10)
            Debug.Log(Mathf.Deg2Rad * i);
    }




    // Update is called once per frame
    void Update()
    {

        Vector3 direction = Quaternion.Euler(angle.x, angle.y, angle.z) * Vector3.forward;

        Debug.DrawLine(origin, origin + direction, Color.blue);

        direction = Quaternion.Euler(angle.x, angle.y, angle.z) * Vector3.right;

        Debug.DrawLine(origin, origin + direction, Color.red);

        direction = Quaternion.Euler(angle.x, angle.y, angle.z) * Vector3.up;

        Debug.DrawLine(origin, origin + direction, Color.yellow);


        return;

        //float angleX = this.transform.rotation.eulerAngles.x;
        //float anglez = this.transform.rotation.eulerAngles.z;


        //float angleTowards;
        //float angle;

        //Vector3 direction = (targetPosition - this.transform.position).normalized;

        //angleTowards = Mathf.Atan2(direction.x, direction.z);
        ////Debug.Log(angleTowards + " angle towards");

        //angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
        ////Debug.Log(angle * Mathf.Rad2Deg + " angle");

        ////Vector3.Angle(Vector3.zero, Vector3.left);

        //Debug.Log(Vector3.Dot(targetPosition, transform.forward));

        //Vector3 midPoint = (origin + transform.forward) / 2f;

        //float radius = 1.75f;
        //float distance = Vector3.Distance(targetPosition, origin);

 
        //Vector3 point = this.transform.forward * distance;
        //float distanceB = Vector3.Distance(point, targetPosition);
        //Debug.Log(distanceB + " distanceB");

        //if (distanceB < radius)
        //    Debug.DrawLine(origin + Vector3.up, Vector3.up + this.transform.forward * 10f, Color.blue);

        //Vector3 pointB = this.transform.forward * (distance - (((radius * 2f) - distanceB / 2f) / 2f));

        ////pointB = this.transform.forward * (distance -  ( radius - distanceB) * 2f);
        ////   - r 
        ////Debug.Log(distanceB / radius);
        //Debug.DrawLine(point, point + Vector3.up);
        ////Debug.Log(Vector3.Distance(point, targetPosition) + " distance");







        ////Vector3 pointC = (point + pointB) / 2f;
        //Debug.DrawLine(pointB, pointB + Vector3.up,Color.red);

        //if (Vector3.Distance(point, targetPosition) < radius)
        //    Debug.Log("?");

        ////float delta = distance - radius;


        //Debug.Log(Mathf.Atan(Vector3.Dot(targetPosition, transform.forward)) * Mathf.Rad2Deg + "b");

        //Debug.DrawLine(midPoint, midPoint + Vector3.Cross(transform.forward, origin),Color.red);


        //Debug.DrawLine(origin, this.transform.forward * 10f);

        //for (float i = 0; i < 360; i += 10)
        //{
        //    Vector3 dir = new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, 0, Mathf.Sin(i * Mathf.Deg2Rad) * radius) + targetPosition;
        //    Vector3 dirB = new Vector3(Mathf.Cos((i + 10f) * Mathf.Deg2Rad) * radius, 0, Mathf.Sin((i + 10f) * Mathf.Deg2Rad) * radius) + targetPosition;

        //    Debug.DrawLine(dir, dirB);
        //}

    }

    
}
