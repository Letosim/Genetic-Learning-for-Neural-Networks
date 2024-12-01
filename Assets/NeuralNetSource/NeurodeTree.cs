using UnityEngine;

public class NeurodeTree : MonoBehaviour
{
    //network
    //network net
    //greedy
    //grid.... :/

    private int neurodeCount;

    public int[]Vector2Int neurodeDistribution;   //Count Type



    private List<CNeurode[]> tree;

    //creep     (rays[])    (proximity)[]   (stats[])   (Core)          gets "angry" on respawn!? XD



    int[] oneDArray = { 1, 2, 3, 4, 5, 6, 3, 4, 5, 6, 3, 4, 5, 6, 3, 4,
        5, 6, 3, 4, 5, 6, 3, 4, 5, 6, 3, 4, 5, 6, 3, 4, 5, 6, 3, 4, 5, 6, 3, 4, 5, 6 };

    public NeurodeTree()
    {
        string text = "";

        int[,] array = QueryTools.Map1DTo2D(oneDArray);

        for (int x = 0; x < array.GetLength(0); x++)
        {
            text += "\n";

            for (int y = 0; y < array.GetLength(0); y++)
            {
                text += " " + array[x, y] + " ";
            }

        }

        Debug.Log(text);
    }




    public void Draw()
    {
        
    }



}
