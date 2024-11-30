using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN2dCarAgent : UnitNN
{
    public override float Fitness { get { return fitness; } set { fitness = value; } }
    public override float[] SensorValues { get { return sensorValues; } set { sensorValues = value; } }
    public override float[] NetworkValues { get { return networkValues; } set { networkValues = value; } }
    public override int SensorValueCount { get { return sensorValueCount; } set { sensorValueCount = value; } }

    public override Color DrawColor { get { return spawnTypeColor; } set { spawnTypeColor = value; } }

    //{ get { return layer; } set { layer = value; } }
    //public int sensorValueCount = 6 + 4 + 3;//rays + directions + velocity
    private int rayCount = 5;

    public int sensorValueCount = 10 + 3;//(rays * 2) + newWall1 + velocity2
    public int inputValueCount = 5;//
    //public float activationOffset = 0;

    private float positionX;
    private float positionY;
    private float directionX;
    private float directionY;

    private float velocity = 0;
    private float velocityX = 0;
    private float velocityY = 0;
    private float accelarationX = 0;
    private float accelarationY = 0;
    private float dampening = .975f;

    private float angle = 0;
    private float rotationSpeed = 2;


    private float fitness = 0;
    private float distanceToStart = 1;
    private bool[] wallDiscovered;



    private float[] sensorValues;
    private float[] networkValues;

    private float startPositionX;
    private float startPositionY;

    private float[] sensorLinesEndX;
    private float[] sensorLinesEndY;
    private float[] sensorLinesAngles;
    private float[] sensorLengths;

    private float[] rayDistances;

    private int currentStep = 0;

    private int stepsWithoutContact = 0;
    private int maxStepsWithoutContact = 25;

    private bool isStuck = false;
    private bool drawAndaved = false;

    private Color color;
    private Color spawnTypeColor;
    private float drawHeight;

    public NN2dCarAgent(WorldDataNN worldData, int a)
    {
        NN2DCarWorldData cardWorld = worldData as NN2DCarWorldData; 
        sensorValues = new float[sensorValueCount];
        sensorLinesEndX = new float[rayCount];
        sensorLinesEndY = new float[rayCount];
        sensorLinesAngles = new float[rayCount];
        sensorLengths = new float[rayCount];
        rayDistances = new float[rayCount];

        sensorLinesAngles[0] = 25; sensorLengths[0] = 9;
        sensorLinesAngles[1] = -50; sensorLengths[1] = 5;
        sensorLinesAngles[2] = -25; sensorLengths[2] = 9;
        sensorLinesAngles[3] = 50; sensorLengths[3] = 5;
        sensorLinesAngles[4] = 0; sensorLengths[4] = 5;

        //sensorLinesAngles[4] = 15; sensorLengths[4] = 12;
        //sensorLinesAngles[5] = -15; sensorLengths[5] = 12;

        UpdateSensorLines();
 
        networkValues = new float[inputValueCount];
        startPositionX = cardWorld.startX;
        startPositionY = cardWorld.startY;
        positionX = startPositionX;
        positionY = startPositionY;
        directionX = 0;
        directionY = 1;
        velocityX = 0;
        velocityY = 0;
        accelarationX = 0;
        accelarationY = 0;
        angle = 0;

    }

    public NN2dCarAgent(WorldDataNN worldData, System.Random randomGen)
    {
        NN2DCarWorldData cardWorld = worldData as NN2DCarWorldData;
        wallDiscovered = new bool[cardWorld.walls.Length];
        for (int i = 0; i < wallDiscovered.Length; i++)
            wallDiscovered[i] = false;
        sensorValues = new float[sensorValueCount];
        sensorLinesEndX = new float[rayCount];
        sensorLinesEndY = new float[rayCount];
        sensorLinesAngles = new float[rayCount];
        sensorLengths = new float[rayCount];
        rayDistances = new float[rayCount];

        //sensorLinesAngles[0] = 25; sensorLengths[0] = 6;
        //sensorLinesAngles[1] = -50; sensorLengths[1] = 9;
        //sensorLinesAngles[2] = -25; sensorLengths[2] = 6;
        //sensorLinesAngles[3] = 50; sensorLengths[3] = 9;

        //sensorLinesAngles[4] = 15; sensorLengths[4] = 12;
        //sensorLinesAngles[5] = -15; sensorLengths[5] = 12;

        sensorLinesAngles[0] = 25; sensorLengths[0] = 20;
        sensorLinesAngles[1] = -25; sensorLengths[1] = 20;

        sensorLinesAngles[2] = 0; sensorLengths[2] = 10;
        //sensorLinesAngles[3] = 180; sensorLengths[3] = 5;
        sensorLinesAngles[3] = 50; sensorLengths[3] = 15;
        sensorLinesAngles[4] = -50; sensorLengths[4] = 15;



        UpdateSensorLines();

        networkValues = new float[inputValueCount];
        startPositionX = cardWorld.startX;
        startPositionY = cardWorld.startY;
        positionX = startPositionX;
        positionY = startPositionY;
        directionX = 0;
        directionY = 1;
        velocityX = 0;
        velocityY = 0;
        accelarationX = 0;
        accelarationY = 0;
        angle = 0;

        float val = (float)randomGen.NextDouble();
        if (val < 0.5f)
            color = new Color(0, 0, val * 2f);
                else
                color = new Color(0, (val - .5f) * 2f, 0);

    }

    private void UpdateSensorLines()
    {
        for (int i = 0; i < sensorLinesAngles.Length; i++)
        {
            sensorLinesEndX[i] = (float)System.Math.Sin((sensorLinesAngles[i] + angle) * 0.017453f);// * sensorLength + positionX;
            sensorLinesEndY[i] = (float)System.Math.Cos((sensorLinesAngles[i] + angle) * 0.017453f);// * sensorLength + positionY;
            float delta = (System.Math.Abs(sensorLinesEndX[i]) + System.Math.Abs(sensorLinesEndY[i])) / 2f;
            sensorLinesEndX[i] *= delta;
            sensorLinesEndY[i] *= delta;
        }
    
    }

    public override void Reset()
    {
        Debug.Log("just no");
    }

    public override void Draw(float layer)
    {
        drawHeight = layer * .25f;
        Vector3 position = new Vector3(positionX, drawHeight, positionY);
        if(fitness == 0)
            Debug.DrawLine(position, position + new Vector3(directionX, 0, directionY), Color.blue);
        else
            Debug.DrawLine(position, position + new Vector3(directionX, 0, directionY), spawnTypeColor);

        Debug.DrawLine(position, position + new Vector3(0, fitness / 5f, 0), color);

        drawAndaved = false;
    }

    public override void DrawAdvanced(float layer)
    {
      
        drawHeight = layer * .25f;
        Vector3 position = new Vector3(positionX, drawHeight, positionY);
        if (isStuck)
        {
            Debug.DrawLine(position, position + Vector3.up, Color.white);
            return;
        }
        //Debug.DrawLine(position + new Vector3(0, c, 0), position + new Vector3(directionX, c, directionY), Color.red);
        //for (int i = 0; i < sensorLinesEndX.Length; i++)
            //Debug.DrawLine(position , position + new Vector3(sensorLinesEndX[i] * rayDistances[i], drawHeight, sensorLinesEndY[i] * rayDistances[i]), color);
        drawAndaved = true;
    }

    public override void UpdateSensors(WorldDataNN worldData)
    {
        if (isStuck)
            return;
        NN2DCarWorldData cardWorld = worldData as NN2DCarWorldData;
        float min = -1f;
        float max = 1f;

        int nextSensor = 0;
        bool hadVisionToWall = false;
        bool discoveredNewWall = false;

        for (int i = 0; i < sensorLinesEndX.Length; i++) //sensorLinesEndX.Length; i++) //
        {

            float minDistance = 500000000000;
            int discoveredWallIndex = -1;
            for (int s = 0; s < 2; s ++)
                sensorValues[nextSensor + s] = min;


            for (int n = 0; n < cardWorld.walls.Length; n++)
            {
                rayDistances[i] = GetLineIntersection(positionX, positionY, sensorLinesEndX[i], sensorLinesEndY[i], cardWorld.walls[n].positionA.x, cardWorld.walls[n].positionA.z, cardWorld.walls[n].positionB.x, cardWorld.walls[n].positionB.z, sensorLengths[i]);

                if (rayDistances[i] != 0)
                {
                    if (rayDistances[i] > minDistance || rayDistances[i] > sensorLengths[i])
                        continue;
                    minDistance = rayDistances[i];
                    hadVisionToWall = true;
                    //sensorValues[i] = max;

                    if (rayDistances[i] > sensorLengths[i] * .5f)
                        sensorValues[nextSensor] = rayDistances[i] / sensorLengths[i];// max;
                    else 
                        sensorValues[nextSensor + 1] = 0;
                    //else if (rayDistances[i] > sensorLengths[i] * .2f)
                        //sensorValues[nextSensor + 2] = max;

                    if (!wallDiscovered[n])
                    {
                        discoveredWallIndex = n;
                    }

                }

            }

            if (discoveredWallIndex != -1)
            {
                wallDiscovered[discoveredWallIndex] = true;
                discoveredNewWall = true;
                fitness += 10f * Vector3.Distance(new Vector3(startPositionX, 0, startPositionY), cardWorld.walls[discoveredWallIndex].centerPositon);
            }

            nextSensor += 2;
        }

        if (hadVisionToWall)
            stepsWithoutContact = 0;
        else
        {
            stepsWithoutContact++;
        }

        if (stepsWithoutContact > maxStepsWithoutContact)
        {
            fitness = 0;
            isStuck = true;
        }
        //if (directionX > 0)
        //    sensorValues[nextSensor] = 1;
        //else
        //    sensorValues[nextSensor] = 0;
        //nextSensor++;

        //if (directionY > 0)
        //    sensorValues[nextSensor] = 1;
        //else
        //    sensorValues[nextSensor] = 0;
        //nextSensor++;

        //if (directionX < 0)
        //    sensorValues[nextSensor] = 1;
        //else
        //    sensorValues[nextSensor] = 0;
        //nextSensor++;

        //if (directionY < 0)
        //    sensorValues[nextSensor] = 1;
        //else
        //    sensorValues[nextSensor] = 0;
        //nextSensor++;

        if (velocity > .5f)
            sensorValues[nextSensor] = max;
        else
            sensorValues[nextSensor] = min;
        nextSensor++;

        //if (velocity > 4)
        //    sensorValues[nextSensor] = 1;
        //else
        //    sensorValues[nextSensor] = 0;
        //nextSensor++;

        if (velocity > 1)
            sensorValues[nextSensor] = max;
        else
            sensorValues[nextSensor] = min;
        nextSensor++;

        if (discoveredNewWall)
            sensorValues[nextSensor] = max;
        else
            sensorValues[nextSensor] = min;

        
    }

    public float GetLineIntersection(float positionX, float positionY, float dirX, float dirY,
   float wallAX, float wallAY, float wallBX, float wallBY, float distance)
    {

       

        Vector2 v1 = new Vector2(positionX, positionY) - new Vector2(wallAX, wallAY);  //wallAX, positionY - wallAY); //pointB - pointC;
        Vector2 v2 = new Vector2(wallBX, wallBY) - new Vector2(wallAX, wallAY); // new Vector2(wallBX - wallAX, wallBY - wallAY);           //pointD - pointC;
        Vector2 v3 = new Vector2(-dirY, dirX);

        float dot = Vector2.Dot(v2, v3);
        float t1 = ((v2.x * v1.y) - (v2.y * v1.x)) / dot;
        if (t1 > distance)
        {
            return 0;
        }

        float t2 = Vector2.Dot(v1, v3) / dot;


        if (t1 >= 0.0 && (t2 >= 0.0 && t2 <= 1.0))
        {
            if (drawAndaved)
            {
               
               Debug.DrawLine(new Vector3(positionX, drawHeight, positionY), new Vector3(positionX, drawHeight, positionY) + new Vector3(dirX * t1, 0, dirY * t1), color);
            }



            if (t1 < 1.5f)
            {
                isStuck = true;
                fitness = 0;
            }

            return t1;
        }

        return 0;
    }


    //public float  GetLineIntersection(float p0_x, float p0_y, float p1_x, float p1_y,
    //  float p2_x, float p2_y, float p3_x, float p3_y)
    //{
    //    float s1_x, s1_y, s2_x, s2_y;
    //    s1_x = p1_x - p0_x; s1_y = p1_y - p0_y;
    //    s2_x = p3_x - p2_x; s2_y = p3_y - p2_y;

    //    float s, t;
    //    s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y);
    //    t = (s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

    //    if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
    //    {
    //        // Collision detected
    //        //if (i_x != NULL)
    //        float positionX = p0_x + (t * s1_x);
    //        //if (i_y != NULL)
    //        float positionY = p0_y + (t * s1_y);

    //        if (System.MathF.Sqrt( p0_x * p0_x - positionX * positionX + p0_y * p0_y - positionY * positionY) < 5f)
    //            isStuck = true;

    //            Debug.DrawLine(new Vector3(p0_x,0,p0_y), new Vector3(positionX, 0, positionY));
    //        return 1;
    //    }

    //    return 0; // No collision
    //}

    public override void Reset(WorldDataNN worldData)
    {
        NN2DCarWorldData cardWorld = worldData as NN2DCarWorldData;
        startPositionX = cardWorld.startX;
        startPositionY = cardWorld.startY;
        positionX = startPositionX;
        positionY = startPositionY;
        directionX = 0;
        directionY = 1;
        velocityX = 0;
        velocityY = 0;
        accelarationX = 0;
        accelarationY = 0;
        angle = 0;

        fitness = 0;
        distanceToStart = 1;
        wallDiscovered = new bool[cardWorld.walls.Length];
        currentStep = 0;
        stepsWithoutContact = 0;

        UpdateSensorLines();
        //sensor values
        for (int i = 0; i < sensorValues.Length; i++)
            sensorValues[i] = 0;
        for (int i = 0; i < wallDiscovered.Length; i++)
            wallDiscovered[i] = false;
        isStuck = false;
    }

    public override void Update()
    {
        if (isStuck)
         return;
        float activationValue = 0f;

        distanceToStart = (float)System.Math.Sqrt((startPositionX * startPositionX - positionX * positionX) + (startPositionY * startPositionY - positionY * positionY));

        if (distanceToStart < 2f && currentStep > 200)
        {

            fitness *= .1f;
            isStuck = true;
            return;
        }

        //if (velocity > 4f )
        //{
        //    isStuck = true;
        //    fitness = 0;
        //    //Debug.Log("out");
        //    return;
        //}

        if (networkValues[0] > activationValue)
        {
            angle += rotationSpeed;
            directionX = (float)System.Math.Sin(angle * 0.017453d);
            directionY = (float)System.Math.Cos(angle * 0.017453d);
            float delta = (System.Math.Abs(directionX) + System.Math.Abs(directionY)) / 2f;
            directionX *= delta;
            directionY *= delta;
        }

        if (networkValues[1] > activationValue)
        {
            angle -= rotationSpeed;
            directionX = (float)System.Math.Sin(angle * 0.017453d);
            directionY = (float)System.Math.Cos(angle * 0.017453d);
            float delta = (System.Math.Abs(directionX) + System.Math.Abs(directionY)) / 2f;
            directionX *= delta;
            directionY *= delta;
        }

        accelarationX = 0;
        accelarationY = 0;

        if (networkValues[2] > activationValue)
        {
            accelarationX = directionX * 0.05f;
            accelarationY = directionY * 0.05f;
        }

        if (networkValues[3] > activationValue)
        {
            accelarationX -= directionX * 0.025f;
            accelarationY -= directionY * 0.025f;
        }

        //if (networkValues[4] > .5f)
        //{
        //    accelarationX = 0;
        //    accelarationY = 0;
        //}

        velocityX += accelarationX;
        velocityY += accelarationY;

        positionX += velocityX;
        positionY += velocityY;
        UpdateSensorLines();

        velocityX *= dampening;
        velocityY *= dampening;

        velocity = (System.Math.Abs(velocityX) + System.Math.Abs(velocityY)) / 2f;
        if (velocity > 1.5f)
            velocity = 1.5f;
             //avarageVelocity = (avarageVelocity + velocity) / 2f;
             currentStep ++;
    }

    public override void UpdateSensors() { }
    public override void Update(WorldDataNN worldData)
    {



    }
}