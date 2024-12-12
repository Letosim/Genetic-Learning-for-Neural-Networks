using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

//[CreateAssetMenu(fileName = "hmmmmmmm", menuName = "Scriptable Objects/hmmmmmmm")]
public class hmmmmmmm : MonoBehaviour
{
    private float maxNetworkDistance = 30;

    List<Vector3> positions = new List<Vector3>();
    public System.Random randomGen = new System.Random();

    void Start()
    {
        positions.Add(Vector3.zero);

        for (int o = 0; o < 10; o++)
        {
            Vector3 newPosition = new Vector3((float)randomGen.NextDouble() * 190f, (float)randomGen.NextDouble() * 190f, (float)randomGen.NextDouble() * 190f);

            for (int n = 0; n < positions.Count; n++)
            {
                if (n != o && Vector3.Distance(positions[n], newPosition) < maxNetworkDistance)
                {
                    newPosition = new Vector3((float)randomGen.NextDouble() * 190f, (float)randomGen.NextDouble() * 190f, (float)randomGen.NextDouble() * 190f);
                    n = 0;
                }
            }

            positions.Add(newPosition);
            colors.Add(new Color((float)randomGen.NextDouble(), (float)randomGen.NextDouble(), (float)randomGen.NextDouble(),.25f));//№###########################
            //Vector3 dir = networks[linknodes[o][0]].neurodes[linknodes[o][1]].position - networks[linknodes[o][2]].neurodes[linknodes[o][3]].position;
        }

        for (int o = 0; o < positions.Count; o++)
            Debug.Log(positions[o]);
    }


    List<Vector3> positionss = new List<Vector3>();
    List<Color> colors = new List<Color>();

  



  void Update()
    {
        positionss.Clear();

        for (int o = 0; o < positions.Count; o++)
        {
            Debug.DrawLine(positions[o], positions[o] + Vector3.up);
            Debug.DrawLine(positions[o] + (Vector3.left + Vector3.forward / 2f) * 5f, positions[o] + (Vector3.right + Vector3.forward / 2f) * 5f, Color.blue);
            positionss.Add(positions[o] + (Vector3.left + Vector3.forward / 2f) * 5f);
            positionss.Add(positions[o] + (Vector3.right + Vector3.forward / 2f) * 5f);

            Debug.DrawLine(positions[o] + (Vector3.left + Vector3.back / 2f) * 5f, positions[o] + (Vector3.right + Vector3.back / 2f) * 5f, Color.blue);
            positionss.Add(positions[o] + (Vector3.left + Vector3.back / 2f) * 5f);
            positionss.Add(positions[o] + (Vector3.right + Vector3.back / 2f) * 5f);

            Debug.DrawLine(positions[o] + (Vector3.back + Vector3.left / 2f) * 5f, positions[o] + (Vector3.forward + Vector3.left / 2f) * 5f, Color.blue);
            positionss.Add(positions[o] + (Vector3.back + Vector3.left / 2f) * 5f);
            positionss.Add(positions[o] + (Vector3.forward + Vector3.left / 2f) * 5f);

            Debug.DrawLine(positions[o] + (Vector3.back + Vector3.right / 2f) * 5f, positions[o] + (Vector3.forward + Vector3.right / 2f) * 5f, Color.blue);
            positionss.Add(positions[o] + (Vector3.back + Vector3.right / 2f) * 5f);
            positionss.Add(positions[o] + (Vector3.right + Vector3.forward / 2f) * 5f);
        }

        for (int o = 1; o < positions.Count; o++)
           Debug.DrawLine(positions[o], positions[o - 1]);
 

        Color color = new Color((float)randomGen.NextDouble(), (float)randomGen.NextDouble(), (float)randomGen.NextDouble());



for (int i = 0; i < positionss.Count; i++)
//"nodes" internal links....... no need to skip  8 ...
{
   
for (int o = 0; o < positionss.Count; o++)
  if( i != o)
  {

    Debug.DrawLine(positionss[i], positionss[o], colors[i]);
  }



}

















        for (int i = 0; i < positionss.Count; i++)
        {
            if(i == 0 || i % 4 == 0)
                color = new Color((float)randomGen.NextDouble(), (float)randomGen.NextDouble(), (float)randomGen.NextDouble());

            for (int o = 1; o < positionss.Count; o++)
            {
                Debug.DrawLine(positionss[o], positionss[i + 8 && positioned.Count], colors[i / 4]);// 4.
            }
        }
    }
}
// for each node I each non 1-8 node o != i


//List<Neuralnetwork[][]>// Cneurode    private List<int> neighboors; for depth???



public void DrawNetwork(List<Neuralnetwork[][]> networks)
{
  
for(int i = 0; i < networks.Count; i ++)
{
  //positions....


}






//neighbours??





}