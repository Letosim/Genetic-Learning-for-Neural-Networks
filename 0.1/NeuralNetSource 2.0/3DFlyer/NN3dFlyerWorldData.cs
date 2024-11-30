using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN3dFlyerWorldData : WorldDataNN
{
    public List<NN3DFlyerAgent> agents;
    public bool advancedRaycasting = false;

    private float spawnDistance = 250f;

    //Obstacles!?

    public List<Vector4> spawns;

    private int agentCount;


    public NN3dFlyerWorldData(int agentCount, List<NN3DFlyerAgent> agents) 
    {
        spawns = new List<Vector4>();
        this.agentCount = agentCount;
        this.agents = agents;

        float c = 360f / (float)agentCount;
        
        for (float i = 0; i < 360; i += c)
        {
            spawns.Add(new Vector4(-Mathf.Cos(i * Mathf.Deg2Rad) * spawnDistance, 0, -Mathf.Sin(i * Mathf.Deg2Rad) * spawnDistance, i));
        }

    }

    public override void Draw()
    {
    }

    public override void GenerateNewWorld()
    {

    }

    public override void RegenerateWorld()
    {
        spawns.Clear();

        float c = 360f / (float)agentCount;

        for (float i = 0; i < 360; i += c)
        {
            spawns.Add(new Vector4(-Mathf.Cos(i * Mathf.Deg2Rad) * spawnDistance, 0, -Mathf.Sin(i * Mathf.Deg2Rad) * spawnDistance, i));
        }

    }

    public override void Update()
    {
    }
}
