using UnityEngine;

public class Buffer
{
    private float currentValue;
    private float baseValue;
    private float baseValuePercentage;
    private float minValue;
    private float maxValue;
    private float vector;

    public float CurrentValue { get { return currentValue; } set { currentValue = value; } }
    public float BaseValue { get { return baseValue; } set { baseValue = value; } }
    public float MinValue { get { return minValue; } set { minValue = value; } }
    public float MaxValue { get { return maxValue; } set { maxValue = value; } }
    public float Vector { get { return vector; } set { vector = value; } }



    Buffer(float baseValue,float minValue, float maxValue) 
    {
        CurrentValue = 0;
        BaseValue = baseValue;
        MinValue = minValue;
        MaxValue = maxValue;
        baseValuePercentage = (baseValue / (maxValue + minValue));
        Vector = 0;
    }

    public float Compute(float t, float dir)
    {
        return 0;
        ////  maxValue         minValue           currentValue
        ////     (13              4)                  7          =   10   >?   (maxValue + min / 2)//8.5               17
        ////     (17              4)                  13         =    8   >?   (maxValue + min / 2)//10.5              21
        ////     (20              10)                 11         =    19   >?   (maxValue + min / 2)//10.5              15       1f -  /2

        ////                                   6  * 5                        (10          / 3) * 2
        ////                                   5                              15          / 3
        ///
        ///                                                             -15     15
        ///                                     10                          30
        ////                                    30%                      0%   -   100%
        //// .5f    -                 baseValuePercentage - (currentValue / (maxValue + System.Math.Abs(minValue)));
        //                                      0 - 1                              0           .5      1
        // 30 * 

        //dir = (baseValue + currentValue - minValue);  


        //vector = dir;

        //currentValue += vector * t;

        //if( currentValue > baseValue)
        //    currentValue -= Math.Abs(vector) * t;
        //else
        //    currentValue += Math.Abs(vector) * t;
    }
}
