using UnityEngine;

public class SliderDrawer : MonoBehaviour
{
    // Show this float in the Inspector as a slider between 0 and 10
    [Range(0.0F, 10.0F)]
    public float myFloat = 0.0F;
}
