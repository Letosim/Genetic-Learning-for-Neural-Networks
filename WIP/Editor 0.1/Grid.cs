using UnityEngine;
using UnityEditor;

public class Grid : EditorWindow
{
    // Define the grid size
    [SerializeField]
    private int rows = 3;
    [SerializeField]
    private int columns = 3;

    // Define a 2D array to store the values
    private int[,] gridValues;

    [MenuItem("Window/Grid Editor")]
    public void ShowWindow()
    {
        GetWindow<Grid>("Grid");
    }


    //IngredientDrawer drawer;

    private void OnEnable()
    {
        //drawer = new IngredientDrawer();
    }

    private void OnGUI()
    {

        //drawer.OnGUI(new Rect(position.x, position.y, 30, position.height), null, GUILayout.Label("This is a label", EditorStyles.boldLabel));

        //EditorGUILayout.ObjectField("Grid", EditorStyles.boldLabel);

        // Draw the grid using nested for loops
        for (int row = 0; row < rows; row++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int col = 0; col < columns; col++)  //                                                           in/out?
            {                                        //                                                             |
                // Display an IntField for each element in the grid                                                 |
                gridValues[row, col] = EditorGUILayout.IntField(gridValues[row, col], GUILayout.Width(50));//[[count][type 1-3]] <-link-> [[count][type 1-3]]                       [[[count][type 1-3]] <-link-> [[count][type 1-3]]]  <-link->  [[[count][type 1-3]] <-link-> [[count][type 1-3]]]
            }

            EditorGUILayout.EndHorizontal();
        }

        // Optionally, add buttons to update the grid size or reset
        if (GUILayout.Button("Update Grid"))
        {
            // Handle logic to update the grid, e.g., changing rows or columns
        }
    }
}
