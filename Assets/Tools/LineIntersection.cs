using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineIntersection : MonoBehaviour
{

    public float angle = 0;


    // Start is called before the first frame update
    void Start()
    {
    
        DrawStroke(10, 10, 1, 1, 1, 15, 1, 1, 1, null, .5f);
    }


    public void DrawStroke(float centerX, float centerY, float r, float g, float b, float distance, float dirX, float dirY, float arcDelta, float[,,] image, float stepDistance)// float toX, float toY, float stepDistance)
    {
        dirX -= .5f;
        dirY -= .5f;

        float delta = (System.Math.Abs(dirX) + System.Math.Abs(dirY)) / 2f;

        dirX *= delta;
        dirY *= delta;

        arcDelta *= 25;
        distance *= 1;
        float curDistance = distance;

        float posX = centerX + -dirX * (distance / 2f);
        float posY = centerY + -dirY * (distance / 2f);

        while (curDistance > 0)
        {

            posY += stepDistance * ((Mathf.Cos((180f * curDistance / distance) * Mathf.Deg2Rad)) + dirY);
            posX += stepDistance * ((Mathf.Sin((180f * curDistance / distance) * Mathf.Deg2Rad)) + dirX);

            curDistance -= stepDistance;
            Debug.DrawLine(new Vector3(posX,0,posY), new Vector3(posX,0, posY) + Vector3.up,Color.red,60);
            Debug.Log(Mathf.Sin((180f * curDistance / distance) * Mathf.Deg2Rad) * arcDelta);
        }
    }

    void New ()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pointA = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector2 pointB = Vector3.zero;
        //Debug.DrawLine(pointA, pointB);

        Vector2 pointC = new Vector3(3, -15);
        Vector2 pointD = new Vector3(6, 15);


        Debug.DrawLine(new Vector3(pointC.x, 0, pointC.y), new Vector3(pointD.x, 0, pointD.y), Color.white);

        GetLineIntersection(pointB.x, pointB.y, pointC.x, pointC.y, pointD.x, pointD.y, pointA.x, pointA.y);


        return;

        Vector2 v1 = pointB - pointC;
        Vector2 v2 = pointD - pointC;
        Vector2 v3 = new Vector2(-pointA.y, pointA.x);

        float dot = Vector2.Dot(v2, v3);
 

        float t1 = ((v2.x * v1.y) - (v2.y * v1.x)) / dot;
        float t2 = Vector2.Dot(v1,v3) / dot;

        if (t1 >= 0.0 && (t2 >= 0.0 && t2 <= 1.0))
            Debug.DrawLine(new Vector3(pointA.x, 0, pointA.y), new Vector3(pointB.x, 0, pointB.y) + Vector3.up);
        else
            Debug.DrawLine(new Vector3(pointA.x, 0, pointA.y), new Vector3(pointB.x, 0, pointB.y) + Vector3.up,Color.red);

        Debug.DrawLine(new Vector3(pointC.x, 0, pointC.y), new Vector3(pointD.x, 0, pointD.y) + Vector3.up, Color.white);

        return;

    }



    public float GetLineIntersection(float positionX, float positionY, float wallAX, float wallAY,
float wallBX, float wallBY, float dirX, float dirY)
    {
        //if (GetLineIntersection(positionX, positionY, sensorLinesEndX[i], sensorLinesEndY[i], cardWorld.wallsX[n], cardWorld.wallsY[n], cardWorld.wallsX[n - 1], cardWorld.wallsY[n - 1]) != 0)

        Vector2 v1 = new Vector2(positionX - wallAX, positionY - wallAY); //pointB - pointC;
        Vector2 v2 = new Vector2(wallBX - wallAX, wallBY - wallAY);           //pointD - pointC;
        Vector2 v3 = new Vector2(-dirY, dirX);

        float dot = Vector2.Dot(v2, v3);
        float t1 = ((v2.x * v1.y) - (v2.y * v1.x)) / dot;
        float t2 = Vector2.Dot(v1, v3) / dot;
        Debug.DrawLine(new Vector3(dirX, 0, dirY) * t1, new Vector3(dirX, 0, dirY) * t1 + Vector3.up,Color.blue);

        Debug.Log(t1);
        Debug.Log(t2);

        if (t1 >= 0.0 && (t2 >= 0.0 && t2 <= 1.0))
        {
            Debug.DrawLine(new Vector3(positionX, 0, positionY), new Vector3(dirX * 20f, 0, dirY * 20f));
            return 1;
        }
        return 0;
    }



    public void old()
    {

        angle += 20f * Time.deltaTime;
        Vector3 pointA = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * 25f;
        Vector3 pointB = Vector3.zero;
        //Debug.DrawLine(pointA, pointB);

        Vector3 pointC = new Vector3(3, 0, -15);
        Vector3 pointD = new Vector3(6, 0, 15);
        Debug.DrawLine(pointC, pointD);

        Vector3 pointE = (pointC + pointD) / 2;

        Debug.DrawLine(pointE, pointE + Vector3.up);

        float dot = Vector3.Dot(pointE, pointA);
        float t1 = Vector3.Cross(pointC, pointD).y / dot;
        float t2 = Vector3.Dot(pointA, pointE) / dot;

        Debug.Log(dot);
        Debug.Log(t1);
        Debug.Log(t2);
        Debug.Log("");

        if (dot > 0)
            Debug.DrawLine(pointA, pointB);
    }

}
