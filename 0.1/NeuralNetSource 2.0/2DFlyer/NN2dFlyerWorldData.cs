using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN2dFlyerWorldData : WorldDataNN
{
    public UnitNN[] agents;
    public float agentSize = 2;

    public Vector3 center;
    public float spawnRadius;
    public float radius;
    public float height = 0;
    public float spawnIndex = 0;


    public Vector3[] obstaclePositions;
    public float[] obstacleSizes;
    System.Random randomGen;


    public NN2dFlyerWorldData(UnitNN[] agents, Vector3 center, float spawnRadius, float radius, float height)
    {
        this.agents = agents;
        this.center = center;
        this.spawnRadius = spawnRadius;
        this.radius = radius;
        this.height = height;
        randomGen = new System.Random();
    }

    public override void Draw()
    {
        for (float i = 0; i < 360; i+= 20)
        {
            Vector3 posA = new Vector3((float)System.Math.Sin(i * 0.017453d) * radius, 0, (float)System.Math.Cos(i * 0.017453d) * radius) + center;
            Vector3 posb = new Vector3((float)System.Math.Sin((i + 10) * 0.017453d) * radius, 0, (float)System.Math.Cos((i + 10) * 0.017453d) * radius) + center;
            Debug.DrawLine(posA, posb);
        }

        for (int n = 0; n < obstaclePositions.Length; n++)
        {
            for (float i = 0; i < 360; i += 30)
            {
                Vector3 posA = new Vector3((float)System.Math.Sin(i * 0.017453d) * obstacleSizes[n], 0, (float)System.Math.Cos(i * 0.017453d) * obstacleSizes[n]) + obstaclePositions[n];
                Vector3 posb = new Vector3((float)System.Math.Sin((i + 20) * 0.017453d) * obstacleSizes[n], 0, (float)System.Math.Cos((i + 20) * 0.017453d) * obstacleSizes[n]) + obstaclePositions[n];
                Debug.DrawLine(posA, posb);
            }
        }

    }

    public override void GenerateNewWorld()
    {
        int obstaclesPlaced = 0;
        int obstaclesToPlace = 30;

        int maxSize = 15;
        int minSize = 5;

        List<Vector3> positions = new List<Vector3>();
        List<float> size = new List<float>();

        while (obstaclesPlaced < obstaclesToPlace)
        {

            bool gotPlace = false;
            int killCounter = 150;
            Vector3 position = Vector3.zero;
            while (!gotPlace)
            {
                if(obstaclesPlaced > obstaclesToPlace / 2)
                    position = new Vector3(.5f - (float)randomGen.NextDouble(), 0, .5f - (float)randomGen.NextDouble()) * spawnRadius * 1.2f + center;
                else
                    position = new Vector3(.5f - (float)randomGen.NextDouble(), 0, .5f - (float)randomGen.NextDouble()) * radius * 2f + center;

                gotPlace = true;

                float distance = Vector3.Distance(position, center);

                if (System.Math.Abs(distance - radius) < 20 || System.Math.Abs(distance - spawnRadius) < 20)
                    gotPlace = false;

                if (gotPlace)
                    for (int i = 0; i < positions.Count; i ++)
                        if(Vector3.Distance(position, positions[i]) < maxSize * 3f)
                            gotPlace = false;


                killCounter--;
                if (killCounter == 0)
                    break;
            }

            positions.Add(position);

            size.Add((float)randomGen.NextDouble() * (maxSize - minSize) + minSize);

            
            obstaclesPlaced++;
        }




        obstaclePositions = positions.ToArray();
        obstacleSizes = size.ToArray();
    }

    public override void RegenerateWorld()
    {
        spawnIndex += 10;
    }

    public override void Update()
    {

    }

}
