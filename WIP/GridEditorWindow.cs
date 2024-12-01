using UnityEditor;
using UnityEngine;

public class GridEditorWindow : EditorWindow
{
    // Define the grid size
    [SerializeField]
    private int rows = 3;
    [SerializeField]
    private int columns = 3;

    // Define a 2D array to store the values
    private int[,] gridValues;

    [MenuItem("Window/Grid Editor")]
    public static void ShowWindow()
    {
        GetWindow<GridEditorWindow>("Grid Editor");
    }

    private void OnEnable()
    {
        // Initialize the grid with some default values
        gridValues = new int[rows, columns];
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Grid of IntFields", EditorStyles.boldLabel);

        // Draw the grid using nested for loops
        for (int row = 0; row < rows; row++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int col = 0; col < columns; col++)  //                                                           in/out?
            {                                      //                                                               |
                // Display an IntField for each element in the grid                                                 |
                gridValues[row, col] = EditorGUILayout.IntField(gridValues[row, col], GUILayout.Width(50));//[[count][type 1-3]] <-link-> [[count][type 1-3]]
            }

            EditorGUILayout.EndHorizontal();
        }

        // Optionally, add buttons to update the grid size or reset
        if (GUILayout.Button("Update Grid"))
        {
            // Handle logic to update the grid, e.g., changing rows or columns
        }
    }

    //if changed...
}