using UnityEngine;

public class Buffer
{
    float currentValue;
    float baseValue;

    float minValue;
    float maxValue;

    float vector;


    public float Compute(float t, float dir)
    {
        vector = dir;

        currentValue += vector * t;

        if( currentValue > baseValue)
            currentValue -= Math.Abs(vector) * t;
        else
            currentValue += Math.Abs(vector) * t;
    }
}
