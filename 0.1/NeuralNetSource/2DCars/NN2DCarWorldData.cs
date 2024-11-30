using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN2DCarWorldData : WorldDataNN
{
    public CarWall[] walls;
    //public float[] wallsX;
    //public float[] wallsY;
    private System.Random randomGen;
    private int cursePoints;
    private float innerRadius;
    private float outerRadius;
    public float startX;
    public float startY;


    public NN2DCarWorldData(int cursePoints, float radius, float outerRadius)
    {
        this.walls = new CarWall[cursePoints * 2];
        //this.wallsX = new float[cursePoints * 2];
        //this.wallsY = new float[cursePoints * 2];
        this.cursePoints = cursePoints;
        this.innerRadius = radius;
        this.outerRadius = outerRadius;
        randomGen = new System.Random(System.DateTime.Now.Millisecond);

        GenerateNewWorld();
    }

    public override void Draw()
    {
        for (int i = 0; i < walls.Length; i++)
            walls[i].Draw();

            return;
        //int halfLength = wallsX.Length / 2;
        //for (int i = 1; i < halfLength; i++)
        //{
        //    Debug.DrawLine(new Vector3(wallsX[i], 0, wallsY[i]), new Vector3(wallsX[i - 1], 0, wallsY[i - 1]));
        //}
        //Debug.DrawLine(new Vector3(wallsX[0], 0, wallsY[0]), new Vector3(wallsX[halfLength - 1], 0, wallsY[halfLength - 1]));

        //for (int i = halfLength + 1; i < wallsX.Length; i++)
        //{
        //    Debug.DrawLine(new Vector3(wallsX[i], 0, wallsY[i]), new Vector3(wallsX[i - 1], 0, wallsY[i - 1]));
        //}
        //Debug.DrawLine(new Vector3(wallsX[halfLength], 0, wallsY[halfLength]), new Vector3(wallsX[wallsX.Length - 1], 0, wallsY[wallsX.Length - 1]));

        //Debug.DrawLine(new Vector3(startX, 0, startY), new Vector3(startX, 0, startY) + Vector3.forward);

    }

    public override void GenerateNewWorld()
    {
        double[] offsets = new double[cursePoints * 2];
        for (int i = 0; i < offsets.Length; i++)
            offsets[i] = System.Math.Clamp(randomGen.NextDouble() + 0.5d, 1d, 1.1d);
        
        float angleA = 0;
        float angleB = (float)(cursePoints - 1) / (float)cursePoints * 360f;
        walls[0] = new CarWall(new Vector3((float)(System.Math.Cos(angleA * 0.017453d) * offsets[0]) * innerRadius, 0, (float)(System.Math.Sin(angleA * 0.017453d) * offsets[0]) * innerRadius), new Vector3((float)(System.Math.Cos(angleB * 0.017453d) * offsets[cursePoints - 1]) * innerRadius, 0, (float)(System.Math.Sin(angleB * 0.017453d) * offsets[cursePoints - 1]) * innerRadius));

        for (int i = 1; i < cursePoints; i++)
        {
            angleA = (float)i / (float)cursePoints * 360f;
            angleB = (float)(i - 1) / (float)cursePoints * 360f;
            walls[i] = new CarWall(new Vector3((float)(System.Math.Cos(angleA * 0.017453d) * offsets[i]) * innerRadius, 0, (float)(System.Math.Sin(angleA * 0.017453d) * offsets[i]) * innerRadius), new Vector3((float)(System.Math.Cos(angleB * 0.017453d) * offsets[i - 1]) * innerRadius, 0, (float)(System.Math.Sin(angleB * 0.017453d) * offsets[i - 1]) * innerRadius));
            //wallsX[i] = (float)(System.Math.Cos(angleA * 0.017453d) * offsets[i]) * innerRadius;//rad deg
           // wallsY[i] = (float)(System.Math.Sin(angleA * 0.017453d) * offsets[i]) * innerRadius;
        }

        angleA = 0;
        angleB = (float)(cursePoints - 1) / (float)cursePoints * 360f;
        walls[cursePoints] = new CarWall(new Vector3((float)(System.Math.Cos(angleA * 0.017453d) * offsets[0]) * outerRadius, 0, (float)(System.Math.Sin(angleA * 0.017453d) * offsets[0]) * outerRadius), new Vector3((float)(System.Math.Cos(angleB * 0.017453d) * offsets[cursePoints - 1]) * outerRadius, 0, (float)(System.Math.Sin(angleB * 0.017453d) * offsets[cursePoints - 1]) * outerRadius));

        for (int i = 1; i < cursePoints; i++)
        {
            angleA = (float)i / (float)cursePoints * 360f;
            angleB = (float)(i - 1) / (float)cursePoints * 360f;

            walls[i + cursePoints] = new CarWall(new Vector3((float)(System.Math.Cos(angleA * 0.017453d) * offsets[i]) * outerRadius, 0, (float)(System.Math.Sin(angleA * 0.017453d) * offsets[i]) * outerRadius), new Vector3((float)(System.Math.Cos(angleB * 0.017453d) * offsets[i - 1]) * outerRadius, 0, (float)(System.Math.Sin(angleB * 0.017453d) * offsets[i - 1]) * outerRadius));
            //wallsX[i + cursePoints] = (float)(System.Math.Cos(angleA * 0.017453d) * offsets[i]) * outerRadius;//rad deg
            //wallsY[i + cursePoints] = (float)(System.Math.Sin(angleA * 0.017453d) * offsets[i]) * outerRadius;
        }

        startX = (walls[0].positionA.x + walls[0].positionB.x + walls[cursePoints].positionA.x + walls[cursePoints].positionA.x) / 4f;
        startY = (walls[0].positionA.z + walls[0].positionB.z + walls[cursePoints].positionA.z + walls[cursePoints].positionA.z) / 4f;

    }

    public override void RegenerateWorld()
    {

    }
    public override void Update()
    {

    }

    public class CarWall
    {
        public Vector3 positionA;
        public Vector3 positionB;
        public Vector3 centerPositon;

        public CarWall(Vector3 positionA, Vector3 positionB)
        {
            this.positionA = positionA;
            this.positionB = positionB;
            this.centerPositon = (positionA + positionB) / 2f;
        }

        public void Draw()
        {
            Debug.DrawLine(positionA, positionB);
        }
    }
}
