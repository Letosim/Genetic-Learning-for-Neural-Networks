using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAgent : UnitNN
{

    public override float Fitness { get { return fitness; } set { fitness = value; } }
    public override float[] SensorValues { get { return sensorValues; } set { sensorValues = value; } }
    public override float[] NetworkValues { get { return networkValues; } set { networkValues = value; } }

    public override int SensorValueCount { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override Color DrawColor { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    //{ get { return layer; } set { layer = value; } }

    private float positionX;
    private float positionY;
    private float positionZ;
    private float fitness;
    private float[] dataSetOne;
    private int[] dataSetTwo;
    private float[] sensorValues;
    private float[] networkValues;

    public DummyAgent()
    {
        sensorValues = new float[6];
        networkValues = new float[6];
    }


    public override void Reset()
    {
    }

    public override void Draw(float layer)
    {
    }

    public override void DrawAdvanced(float layer)
    {
    }

    public override void Update()
    {
    }


    public override void UpdateSensors()
    {
    }
    public override void UpdateSensors(WorldDataNN worldData)
    {
    }
    public override void Update(WorldDataNN worldData)
    {

    }
    public override void Reset(WorldDataNN worldData)
    {

    }


  


}
