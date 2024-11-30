using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN2DFlyerAgent : UnitNN
{
    public int hitsTaken = 0;
    
    public override float Fitness { get { return fitness; } set { fitness = value; } }
    public override float[] SensorValues { get { return sensorValues; } set { sensorValues = value; } }
    public override float[] NetworkValues { get { return networkValues; } set { networkValues = value; } }
    public override int SensorValueCount { get { return sensorValueCount; } set { sensorValueCount = value; } }

    public override Color DrawColor { get { return spawnTypeColor; } set { spawnTypeColor = value; } }

    public Vector3 position;
    public Vector3 direction;
 
    private float velocity;
    private float velocityX;
    private float velocityY;

    private float angle;

    private float maxRotationSpeed = 3f;
    private float rotationSpeed = 0f;
    private float rotationAccelration = .1f;


    public bool isStuck;

    private int inputValueCount = 5;
    public int sensorValueCount = 16 + 9;//rays(obstacle) + (rays * 3) + 9 + = 25
    public float[] sensorValues;//(rays * 3) + newWall1 + velocity2
    public float[] networkValues;//(rays * 3) + newWall1 + velocity2


    private int rayCount = 4;
    private float[] sensorLineDirectionsX;
    private float[] sensorLineDirectionsY;
    private float[] sensorLinesAngles;

    public float fitness = 0;
    private Vector3 fitnessVector = Vector3.zero;
    private int maxHull = 1000;
    public int hull = 500;

    private bool canShoot = true;
    private bool didShoot = false;
    private int shootCoolDown = 15;
    private int shootCoolDownCurrent = 0;
    private int dmg = 25;
    public int gotLockOn = 0;


    private float maxRayDistance = 50;
    private float rayDistanceDelta = 0.02f;
    private float distanceSensorCount = 3;



    private bool drawAdvanced = false;
    private int index = 0;
    private Color color;
    private Color spawnTypeColor;
    private Vector3[] drawRay;

    public NN2DFlyerAgent(System.Random randomGen,int index,WorldDataNN worldData)
    {
        NN2dFlyerWorldData world = worldData as NN2dFlyerWorldData;

        networkValues = new float[inputValueCount];

        float spawnAngle = 355f * (index / (float)world.agents.Length) + world.spawnIndex;

        angle = spawnAngle * -1f;
        position = new Vector3((float)System.Math.Sin(spawnAngle * 0.017453d) * world.spawnRadius, world.height, (float)System.Math.Cos(spawnAngle * 0.017453d) * world.spawnRadius) + world.center;
        sensorValues = new float[sensorValueCount];
        sensorLineDirectionsX = new float[rayCount];
        sensorLineDirectionsY = new float[rayCount];
        sensorLinesAngles = new float[rayCount];

        drawRay = new Vector3[rayCount];
        for (int i = 0; i < rayCount; i++)
            drawRay[i] = Vector3.zero;

        sensorLinesAngles[0] = 0;
        sensorLinesAngles[1] = 35; 
        sensorLinesAngles[2] = -35;
        sensorLinesAngles[3] = 180;
        spawnTypeColor = Color.black;
        float val = (float)randomGen.NextDouble();
        this.index = index;

        if (val < 0.5f)
            color = new Color(0, 0, val * 2f);
        else
            color = new Color(0, (val - .5f) * 2f, 0);
    }

    public override void Draw(float layer)
    {
        if (isStuck)
            Debug.DrawLine(position, position + direction, Color.white);
        else
            Debug.DrawLine(position, position + direction, spawnTypeColor);

        drawAdvanced = false;
    }//!

    public override void DrawAdvanced(float layer)
    {
        if (isStuck)
            return;
        
        Debug.DrawLine(position, position + fitnessVector, Color.white);

        for (int i = 0; i < rayCount; i++)
        {
            if (drawRay[i].Equals(Vector3.zero))
                continue;
            Debug.DrawLine(position, drawRay[i], color);
            drawRay[i] = Vector3.zero;
        }

        drawAdvanced = true;
    }//!

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public override void Reset(WorldDataNN worldData)
    {
        NN2dFlyerWorldData world = worldData as NN2dFlyerWorldData;
        float spawnAngle = 355f * (index / (float)world.agents.Length) + world.spawnIndex;
        fitnessVector = Vector3.zero;
        angle = spawnAngle * -1f;
        position = new Vector3((float)System.Math.Sin(spawnAngle * 0.017453d) * world.spawnRadius, world.height, (float)System.Math.Cos(spawnAngle * 0.017453d) * world.spawnRadius) + world.center;
        for (int i = 0; i < sensorValues.Length; i++)
            sensorValues[i] = 0;

        fitness = 0;

        canShoot = true;
        didShoot = false;
        shootCoolDownCurrent = 0;
        gotLockOn = 0;
        hull = maxHull;

        velocity = 0;
        velocityX = 0;
        velocityY = 0;
        isStuck = false;

        hitsTaken = 0;

        for (int i = 0; i < rayCount; i++)
            drawRay[i] = Vector3.zero;

    }//!


    private int ticks = 0;

    public override void Update()
    {
        if (isStuck)
            return;

        if (ticks > 5)
        {
            ticks = 0;

        }

        ticks++;

        hull -= 5;
        fitness += 1f;

        if (hull < 0)
        {
            isStuck = true;
            return;
        }

        float activationValue = 0f;

        if (networkValues[0] > activationValue)
        {
            if (rotationSpeed < maxRotationSpeed)
                rotationSpeed += rotationAccelration;
            else
            {
                rotationSpeed *= .95f;
                fitness += .01f;
            }

            angle += rotationSpeed;
            direction.x = (float)System.Math.Sin(angle * 0.017453d);
            direction.z = (float)System.Math.Cos(angle * 0.017453d);
        }

        if (networkValues[1] > activationValue)
        {
            if (rotationSpeed < maxRotationSpeed)
                rotationSpeed += rotationAccelration;
            else
            {
                rotationSpeed *= .95f;
                fitness += .01f;
            }

            angle -= rotationSpeed;
            direction.x = (float)System.Math.Sin(angle * 0.017453d);
            direction.z = (float)System.Math.Cos(angle * 0.017453d);
        }

        float accelarationX = 0;
        float accelarationY = 0;

        if (networkValues[2] > activationValue)
        {
            accelarationX = direction.x * 0.05f;
            accelarationY = direction.z * 0.05f;
        }

        if (networkValues[3] > activationValue)
        {
            accelarationX -= direction.x * 0.025f;
            accelarationY -= direction.z * 0.025f;
        }

        velocityX += accelarationX;
        velocityY += accelarationY;

        position.x += velocityX;
        position.z += velocityY;

        velocityX *= 0.95f;
        velocityY *= 0.95f;

        velocity = (System.Math.Abs(velocityX) + System.Math.Abs(velocityY)) / 2f;

        if (networkValues[4] > activationValue)
        {
            if (canShoot)
            {
                shootCoolDownCurrent = 0;
                canShoot = false;
                didShoot = true;
            }
        }

        if (!canShoot)
        {
            shootCoolDownCurrent++;
            if (shootCoolDownCurrent > shootCoolDown)
                canShoot = true;
        }
        

    } //!

    public override void Update(WorldDataNN worldData)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateSensors()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateSensors(WorldDataNN worldData)
    {
        if (isStuck)
            return;

        NN2dFlyerWorldData world = worldData as NN2dFlyerWorldData;

        if (Vector3.Distance(position, world.center) > world.radius)
        {
            position = (position - world.center) * -1f + world.center;
            position.y = world.height;
        }

        int currentSensor = 0;
        int rayTargetIndex = -1;
        float min = -1f;
        float max = 1f;

        for (int i = 0; i < sensorLinesAngles.Length; i++)
        {
            float minDistance = 500000;
            sensorLineDirectionsX[i] = (float)System.Math.Sin((sensorLinesAngles[i] + angle) * 0.017453f);
            sensorLineDirectionsY[i] = (float)System.Math.Cos((sensorLinesAngles[i] + angle) * 0.017453f);

            for (int d = 0; d < distanceSensorCount; d++)
                sensorValues[currentSensor + d] = min;

            for (int a = 0; a < world.agents.Length; a++)
            {
                if (a == index)
                    continue;

                NN2DFlyerAgent agent = world.agents[a] as NN2DFlyerAgent;
                float distance = Vector3.Distance(position, agent.position);

                if (agent.isStuck || distance > maxRayDistance || distance < world.agentSize * 2)                   
                    continue;
       

                float rayDistance = Raycast(i, agent.position, world.agentSize);

                if (rayDistance == -1)
                    rayDistance = maxRayDistance;

                if (drawAdvanced)
                    drawRay[i] = new Vector3(sensorLineDirectionsX[i], 0, sensorLineDirectionsY[i]) * rayDistance + position;

                if (rayDistance > 0 && distance < minDistance)
                {


                    if (i == 0)
                        rayTargetIndex = a;
                    minDistance = distance;

            
                    for (int d = (int)distanceSensorCount - 1; d != -1; d--)
                    {
                        float delta = rayDistance * rayDistanceDelta;
                        if (delta > d / distanceSensorCount)
                            sensorValues[currentSensor + d] = max;
                        else
                            sensorValues[currentSensor + d] = min;
                    }
                }
            }

            //currentSensor += (int)distanceSensorCount;// ?
        }

        if (rayTargetIndex != -1)
        {
            NN2DFlyerAgent agent = world.agents[rayTargetIndex] as NN2DFlyerAgent;
            agent.gotLockOn = 2;
             
            if (didShoot)
            {
                fitness += 500000f;
                fitnessVector = fitnessVector + Vector3.up * .1f;
                agent.hull -= dmg;
                agent.hitsTaken++;
                if (agent.hull < 0 && hull < maxHull)
                    hull += 25;

            }

            sensorValues[currentSensor] = max;
        }
        else 
            sensorValues[currentSensor] = min;
        currentSensor++;




        if (didShoot)
            didShoot = false;


        float distanceToCenter = Vector3.Distance(position, world.center);

        if (distanceToCenter > world.radius * .75f)
            sensorValues[currentSensor] = max;
        else
            sensorValues[currentSensor] = min;
        currentSensor++;

        if (distanceToCenter < world.radius * .25f)
            sensorValues[currentSensor] = max;
        else
            sensorValues[currentSensor] = min;
        currentSensor++;

        if (Vector3.Dot(direction,(world.center - position).normalized) > .5f)
            sensorValues[currentSensor] = max;
        else
            sensorValues[currentSensor] = min;
        currentSensor++;

        if (canShoot)
            sensorValues[currentSensor] = max;
        else
            sensorValues[currentSensor] = min;
        currentSensor++;

        if (gotLockOn != 0)
        {
            gotLockOn--;
            sensorValues[currentSensor] = max; 
        }
        else
            sensorValues[currentSensor] = min;
        currentSensor++;

        if (velocity > 5)
        {
            sensorValues[currentSensor] = max;
        }
        else
            sensorValues[currentSensor] = min;
        currentSensor++;

        if (velocity > 10)
        {
            sensorValues[currentSensor] = max;
        }
        else
            sensorValues[currentSensor] = min;
        currentSensor++;

        bool collided = false;

        for (int i = 0; i < sensorLinesAngles.Length; i++)
        {
            float minDistance = 500000;
            sensorValues[currentSensor] = min;


            for (int a = 0; a < world.obstaclePositions.Length; a++)
            {
                float distance = Vector3.Distance(position, world.obstaclePositions[a]);
                if (distance < world.obstacleSizes[a])
                    collided = true;

                if (distance > maxRayDistance)
                    continue;


                float rayDistance = Raycast(i, world.obstaclePositions[a], world.obstacleSizes[a]);//lines

                if (rayDistance > 0 && distance < minDistance)
                {

                    minDistance = distance;
                    if (drawAdvanced)
                        drawRay[i] = new Vector3(sensorLineDirectionsX[i], 0, sensorLineDirectionsY[i]) * rayDistance + position;
                    
                    sensorValues[currentSensor] = max;
                 }

            }
            currentSensor++;
        }
        if (collided)
        { 
            hull -= 10;
            sensorValues[currentSensor] = max;
        }
        else
            sensorValues[currentSensor] = min;
    }//!


    float Raycast(int sensorIndex, Vector3 targetPosition, float radius)
    {

        Vector3 d = new Vector3(sensorLineDirectionsX[sensorIndex], 0, sensorLineDirectionsY[sensorIndex]);
        float r = radius;
        
        Vector3 e = targetPosition - position;
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
    }//!

}
