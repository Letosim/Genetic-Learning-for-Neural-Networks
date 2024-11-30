using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkLayout
{

    public Neurode.NeurodeType type;
    public int[] layout;
    public int count;

    public NetworkLayout(Neurode.NeurodeType type, int[] layout)
    {
        this.type = type;
        this.layout = layout;
    }

    public NetworkLayout(Neurode.NeurodeType type, int count)
    {
        this.type = type;
        this.count = count;
        layout = new int[] {};

    }
}
