using UnityEngine;

public class NeurodeTree : MonoBehaviour
{
    //network
    //network net
    //greedy
    //grid.... :/

    public int NeurodeCount;
    //creep     (rays[])    (proixmity)[]   (stats[])   (Core)            gets "angry" on respawn!? XD
    //List<array[]>;
//                                                                                                           in/out
//                                                                                                             |
//                                                                                                     [       |          ]
//[[count][type 1-3]] <-link-> [[count][type 1-3]]                       [[[count][type 1-3]] <-link-> [[count][type 1-3]]]  <-link->  [[[count][type 1-3]] <-link-> [[count][type 1-3]]]



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

}
