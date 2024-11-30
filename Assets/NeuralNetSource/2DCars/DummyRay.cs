using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRay : MonoBehaviour
{
    float sensorLength = 10f;
    float angle = 0;

 
    Vector3 direction;
    Vector2[] wallsA;
    Vector2[] wallsB;

    Vector3[] spherePositions;
    float[] spheresRadius;

    //      -5,5        5,5
    //      -5,-5        5,-5

    public void Start()
    {
        spherePositions = new Vector3[] { new Vector3(0, -10, 0), new Vector3(15, 0, -15) };
        spheresRadius = new float[] { 3, 6 };
        rnd = new System.Random();
    }

    System.Random rnd;

    public void Update()
    {

        Debug.Log(rnd.Next(0, 2));
        Vector3 dir = this.transform.forward;

        for (int i = 0; i < spheresRadius.Length; i++)
        {
            float d = Raycast01(this.transform.position, dir, spherePositions[i], spheresRadius[i]);
            Debug.DrawLine(this.transform.position, this.transform.position + dir * d);
        }
    }

    float Raycast01(Vector3 origin, Vector3 direction, Vector3 position, float radius)
    {
        Vector3 p0 = origin;
        Vector3 d = direction;
        Vector3 c = position;
        float r = radius;

        Vector3 e = c - p0;
        // Using Length here would cause floating point error to creep in
        float Esq = Vector3.SqrMagnitude(e);
        float a = Vector3.Dot(e, d);
        float b = (float)System.Math.Sqrt(Esq - (a * a));
        float f = (float)System.Math.Sqrt((r * r) - (b * b));

        // No collision
        if (r * r - Esq + a * a < 0f)
        {
            return -1; // -1 is invalid.
        }
        // Ray is inside

        else if (Esq < r * r)
        {
            return a + f; // Just reverse direction
        }

        // else Normal intersection
        return a - f;
    }

    //  // Start is called before the first frame update
    //  void Start()
    //  {
    //      wallsA = new Vector2[] { new Vector2(5, 5), new Vector2(5, -5), new Vector2(5, -5), new Vector2(5, 5) };
    //      wallsB = new Vector2[]{ new Vector2(-5,5),new Vector2(-5,-5),new Vector2(5,5),new Vector2(-5,5) };
    //  }

    //  // Update is called once per frame
    //  void Update()
    //  {
    //      Vector3 position = this.transform.position;
    //      if (Input.GetKeyDown(KeyCode.RightArrow))
    //          angle  += 20f;

    //      direction.x = (float)System.Math.Sin(angle * 0.017453d);
    //      direction.y = (float)System.Math.Cos(angle * 0.017453d);
    //      //Mathf.Cos(Mathf.Deg2Rad * angle);


    //      Debug.DrawLine(position + Vector3.down, position + new Vector3(direction.x * sensorLength, 0, direction.y * sensorLength) + Vector3.down);



    //      for (int i = 0; i < wallsA.Length; i++)
    //      {
    //          GetLineIntersection(position.x, position.z, direction.x, direction.y, wallsA[i].x + position.x, wallsA[i].y + position.z, wallsB[i].x + position.x, wallsB[i].y + position.z);
    //          Debug.DrawLine(new Vector3(wallsA[i].x + position.x, 0, wallsA[i].y + position.z), new Vector3(wallsB[i].x + position.x, 0, wallsB[i].y + position.z), Color.green);
    //      }
    //  }

    //  public float GetLineIntersection(float positionX, float positionY, float dirX, float dirY,
    //float wallAX, float wallAY, float wallBX, float wallBY)
    //  {
    //      //if (GetLineIntersection(positionX, positionY, sensorLinesEndX[i], sensorLinesEndY[i], cardWorld.wallsX[n], cardWorld.wallsY[n], cardWorld.wallsX[n - 1], cardWorld.wallsY[n - 1]) != 0)

    //      Vector2 v1 = new Vector2(positionX, positionY) - new Vector2(wallAX, wallAY);  //wallAX, positionY - wallAY); //pointB - pointC;
    //      Vector2 v2 = new Vector2(wallBX, wallBY) - new Vector2(wallAX, wallAY); // new Vector2(wallBX - wallAX, wallBY - wallAY);           //pointD - pointC;
    //      Vector2 v3 = new Vector2(-dirY, dirX);

    //      float dot = Vector2.Dot(v2, v3);
    //      float t1 = ((v2.x * v1.y) - (v2.y * v1.x)) / dot;
    //      if (t1 > sensorLength)
    //          return 0;
    //      // if (t1 < 2f)
    //      // isStuck = true;
    //      //Debug.DrawLine(new Vector3(positionX, 0, positionY), new Vector3(dirX, 0, dirY) * t1, Color.red);


    //      float t2 = Vector2.Dot(v1, v3) / dot;

    //      if (t1 >= 0.0 && (t2 >= 0.0 && t2 <= 1.0))
    //      {
    //          Debug.DrawLine(new Vector3(positionX, 0, positionY),new Vector3(positionX, 0, positionY) + new Vector3(dirX, 0, dirY) * t1, Color.blue);
    //          Debug.DrawLine(new Vector3(wallAX, 0, wallAY) + Vector3.up, new Vector3(wallBX, 0, wallBY) + Vector3.up, Color.green);

    //          //Debug.DrawLine(new Vector3(positionX, 0, positionY), new Vector3(dirX * 20f, 0, dirY * 20f));
    //          return 1;
    //      }
    //      return 0;
    //  }
}
