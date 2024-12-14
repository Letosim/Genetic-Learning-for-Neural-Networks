using UnityEditor;
using UnityEngine;
using System;


[Serializable]
public class InterfaceNodeField     Vector4Int        count type depth        In->             >(->  ->  ->)> 
{
    public Vector4Int[] neurode;
}

public class InterfaceNodeContainer : ScriptableObject
{
    public InterfaceNodeField interfaceNode;
}

//public class PropertyFieldd : VisualElement//SendToBack
//{
    
//}


public class Vector4Int()
{

public float X;
public float y;
public float z;


public override int X { get { return X; } set { X = value; } }

public override int Y { get { return y; } set { Y = value; } }

public override int Z { get { return z; } set { Z = value; } }


}
