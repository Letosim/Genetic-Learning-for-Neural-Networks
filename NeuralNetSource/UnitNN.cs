using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitNN
{
    public abstract int SensorValueCount { get; set; }

    public abstract float Fitness { get; set; }
    public abstract float[] SensorValues { get; set; }
    public abstract float[] NetworkValues { get; set; }
    public abstract Color DrawColor { get; set; }


    public abstract void UpdateSensors();
    public abstract void UpdateSensors(WorldDataNN worldData);

    public abstract void Update();
    public abstract void Update(WorldDataNN worldData);
    //public abstract void Update(float fitness);

    public abstract void Reset();
    public abstract void Reset(WorldDataNN worldData);

    public abstract void Draw(float layer);
    public abstract void DrawAdvanced(float layer);

}
