using UnityEditor;
using UnityEngine;
using System;


[Serializable]
public class InterfaceNodeField     //Vector3Int        count type depth        In->             >(->  ->  ->)> 
{
    public Vector3Int[] neurode;
}

public class InterfaceNodeContainer : ScriptableObject
{
    public InterfaceNodeField interfaceNode;
}

//public class PropertyFieldd : VisualElement//SendToBack
//{
    
//}