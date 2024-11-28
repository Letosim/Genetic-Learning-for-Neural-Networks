using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN3DFlyerAgent : UnitNN
{
    public int ID;

    private float activationValue = .5f;
    private float defaultSensorValue = 1f;
    private float defaultNegativeSensorValue = 1f;

    public override float Fitness { get { return fitness; } set { fitness = value; } }
    public override float[] SensorValues { get { return sensorValues; } set { sensorValues = value; } }
    public override float[] NetworkValues { get { return networkValues; } set { networkValues = value; } }
    public override int SensorValueCount { get { return sensorValueCount; } set { sensorValueCount = value; } }
    public override Color DrawColor { get { return color; } set { color = value; } }

    private float fitness = 0;
    private int sensorValueCount = 6;//rays(obstacle)                       + (rays * 3) + 9 + = 25
    private float[] sensorValues;// rayAnglesX + rayAnglesY + 1 + directionSteps * 6
    private float[] networkValues;//7
    private Color color;

    private float size = 1f;
    private float accelaration = .25f;
    private float angularAccelaration = .05f;

    private int targetIndex = -1;

    private Vector3 angle;

    private float[] rayAnglesX = new float[] {35, -35 };
    private float[] rayAnglesY = new float[] {35, -35 };
    
     

    private float directionSteps = 3;

    public bool active = true;

    public Vector3 position;
    public Vector3 velocity;

    public Vector3 direction;
    public Vector3 angularVelocity;

    private NN3dFlyerWorldData worldData;

    public NN3DFlyerAgent(int ID)
    {
        this.ID = ID;
        sensorValues = new float[sensorValueCount];


        // les go sensor
    }



    public override void UpdateSensors(WorldDataNN worldData)
    {
        NN3dFlyerWorldData data = worldData as NN3dFlyerWorldData;
        int index = 1;
        targetIndex = -1;

        if (data.advancedRaycasting)
        {
            targetIndex = CastRayAdvanced(data.agents, 0, 0);

            if (targetIndex != -1)
                SensorValues[0] = defaultSensorValue;
            else
                SensorValues[0] = defaultNegativeSensorValue;
        }
        else
        {
            targetIndex = CastRay(data.agents, 0, 0);

            if (targetIndex != -1)
                SensorValues[0] = defaultSensorValue;
            else
                SensorValues[0] = defaultNegativeSensorValue;
        }
     





        for (int i = 0; i < rayAnglesX.Length; i++)
        {
            if(data.advancedRaycasting)
                if (CastRayAdvanced(data.agents, rayAnglesX[i], 0) != -1)
                    SensorValues[index] = defaultSensorValue;
                else
                    SensorValues[index] = defaultNegativeSensorValue;
            else
                if (CastRay(data.agents, rayAnglesX[i], 0) != -1)
                    SensorValues[index] = defaultSensorValue;
                else
                    SensorValues[index] = defaultNegativeSensorValue;

            index++;
        }

        for (int i = 0; i < rayAnglesY.Length; i++)
        {
            if (data.advancedRaycasting)
                if (CastRayAdvanced(data.agents, 0, rayAnglesY[i]) != -1)
                SensorValues[index] = defaultSensorValue;
            else
                SensorValues[index] = defaultNegativeSensorValue;
            else
                if (CastRay(data.agents, 0, rayAnglesY[i]) != -1)
                    SensorValues[index] = defaultSensorValue;
                else
                    SensorValues[index] = defaultNegativeSensorValue;
            index++;
        }

        for (float i = 0; i < directionSteps - 1; i++)
        {
            if(direction.z > i / directionSteps)
                SensorValues[index] = defaultSensorValue;
            else
                SensorValues[index] = defaultNegativeSensorValue;
            index++;

            if (direction.z < -(i / directionSteps))
                SensorValues[index] = defaultSensorValue;
            else
                SensorValues[index] = defaultNegativeSensorValue;
            index++;

            if (direction.x > i / directionSteps)
                SensorValues[index] = defaultSensorValue;
            else
                SensorValues[index] = defaultNegativeSensorValue;
            index++;

            if (direction.x < -(i / directionSteps))
                SensorValues[index] = defaultSensorValue;
            else
                SensorValues[index] = defaultNegativeSensorValue;
            index++;

            if (direction.y > i / directionSteps)
                SensorValues[index] = defaultSensorValue;
            else
                SensorValues[index] = defaultNegativeSensorValue;
            index++;

            if (direction.y < -(i / directionSteps))
                SensorValues[index] = defaultSensorValue;
            else
                SensorValues[index] = defaultNegativeSensorValue;
            index++;
        }





        DetectCollision();
    }

    public override void Update(WorldDataNN worldData)
    {

        if (networkValues[0] > activationValue)
            velocity += direction * accelaration;

        if (networkValues[1] > activationValue)
            velocity -= direction * accelaration;


        if (networkValues[2] > activationValue)
            angularVelocity.x += angularAccelaration;

        if (networkValues[3] > activationValue)
            angularVelocity.x -= angularAccelaration;

        if (networkValues[4] > activationValue)
            angularVelocity.y += angularAccelaration;

        if (networkValues[5] > activationValue)
            angularVelocity.y -= angularAccelaration;

        if (networkValues[4] > activationValue)
            angularVelocity.z += angularAccelaration;

        if (networkValues[5] > activationValue)
            angularVelocity.z -= angularAccelaration;

        angle += angularVelocity;
        direction = Quaternion.Euler(angle.x, angle.y, angle.z) * Vector3.up;

        position += velocity;

        if (targetIndex != -1)
        {
            fitness += 1f;

            if (networkValues[6] > activationValue)
                fitness += 50f;
        }

    }


    //..... les go...........



    public override void Reset(WorldDataNN worldData)
    {
        NN3dFlyerWorldData data = worldData as NN3dFlyerWorldData;

        active = true;
        targetIndex = -1;
        fitness = 0;

        position = data.spawns[ID];
        angle = new Vector3(0, data.spawns[ID].w, 0);
        direction = Vector3.zero;
        velocity = Vector3.zero;
        angularVelocity = Vector3.zero;
    }

    public void DetectCollision()
    {
        for (int i = 0; i < worldData.agents.Count; i++)
            if (Vector3.Distance(position, worldData.agents[i].position) < size)
            {
                active = false;
                return;
            }
    }



    public int CastRay(List<NN3DFlyerAgent> obstacles,float angleX, float angleY)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            float distance = Vector3.Distance(position, obstacles[i].position);

            Vector3 dir = Quaternion.Euler(angle.x + angleX, angle.y + angleY, angle.z) * Vector3.up; //up?
            Vector3 closePoint = position + dir * distance;

            if (Vector3.Distance(closePoint, obstacles[i].position) < obstacles[i].size)
                return i;
        }

        return -1;
    }

    public int CastRayAdvanced(List<NN3DFlyerAgent> obstacles, float angleX, float angleY)
    {
        int index = -1;
        float minDistance = 100000;

        for (int i = 0; i < obstacles.Count; i++)
        {
            float distance = Vector3.Distance(position, obstacles[i].position);
            Vector3 closePoint = position + direction * distance;
            distance = Vector3.Distance(closePoint, obstacles[i].position);

            if (distance < obstacles[i].size && distance < minDistance)
            {
                minDistance = distance;
                index = i;
            }
        }

        return index;
    }



    public override void Draw(float layer)
    {
        for (float i = 0; i < 360; i += 36)
        {
            float cos = Mathf.Cos(i * Mathf.Deg2Rad) * size;
            float sin = Mathf.Sin(i * Mathf.Deg2Rad) * size;
            float cosB = Mathf.Cos(i * Mathf.Deg2Rad + 36f) * size;
            float sinB = Mathf.Sin(i * Mathf.Deg2Rad + 36f) * size;

            Vector3 posA = new Vector3(cos, 0, sin) + position;
            Vector3 posB = new Vector3(cosB, 0, sinB) + position;
            Debug.DrawLine(posA, posB, color);

            posA = new Vector3(0, cos, sin) + position;
            posB = new Vector3(0, cosB, sinB) + position;
            Debug.DrawLine(posA, posB, color);
        }
    }

    public override void DrawAdvanced(float layer)
    {
        for (float i = 0; i < 360; i += 36)
        {
            float cos = Mathf.Cos(i * Mathf.Deg2Rad) * size;
            float sin = Mathf.Sin(i * Mathf.Deg2Rad) * size;
            float cosB = Mathf.Cos(i * Mathf.Deg2Rad + 36f) * size;
            float sinB = Mathf.Sin(i * Mathf.Deg2Rad + 36f) * size;

            Vector3 posA = new Vector3(cos, 0, sin) + position;
            Vector3 posB = new Vector3(0, cosB, sinB) + position;

            Debug.DrawLine(posA, posB, color);
            Debug.DrawLine(posA, position + Vector3.up * size, color);
            Debug.DrawLine(posA, position + Vector3.down * size, color);
        }
    }


    public override void Reset()
    {
    }

    public override void Update()
    {
    }

    public override void UpdateSensors()
    {
    }
}
