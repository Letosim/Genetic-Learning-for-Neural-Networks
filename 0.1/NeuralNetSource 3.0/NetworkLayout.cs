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
        //uses memory
        this.layout = layout;
    }

    public NetworkLayout(Neurode.NeurodeType type, int count)
    {
        this.type = type;
        this.count = count;
        layout = new int[] {};

    }

    //networkLayout[0] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 0, 0, 0, 0, 0 });//crhomosen?
    //networkLayout[1] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 9, 0, 0, 0, 5 });//crhomosen?
    //networkLayout[2] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 0, 6, 6, 6, 0 });//crhomosen?


    //networkLayout[0] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 0{}, 0{}, 0{}, 0{}, 0{}});//crhomosen?
    //networkLayout[1] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 0{}, 0{}, 0{}, 0{}, 0{}});//crhomosen?
    //networkLayout[2] = new NetworkLayout(Neurode.NeurodeType.TanNeurode, new int[] { 0{}, 0{}, 0{}, 0{}, 0{}});//crhomosen?


    
}
