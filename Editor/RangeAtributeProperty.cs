using UnityEngine;

public class RangeAtributeProperty : PropertyAttribute
{
    public float min;
    public float max;

    public void RangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}