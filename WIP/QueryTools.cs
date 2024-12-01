using UnityEngine;

public class QueryTools
{
    public static T[,] Map1DTo2D<T>(T[] oneDimensionalArray)
    {
        int vector = (int)Mathf.Ceil(Mathf.Sqrt(oneDimensionalArray.Length)); // Calculate square dimensions

        T[,] twoDimensionalArray = new T[vector, vector]; // Create square 2D array

        for (int i = 0; i < oneDimensionalArray.Length; i++)
        {
            int row = i / vector; // Determine row index
            int col = i % vector; // Determine column index

            twoDimensionalArray[row, col] = oneDimensionalArray[i]; // Assign value
        }

        return twoDimensionalArray;
    }
}
