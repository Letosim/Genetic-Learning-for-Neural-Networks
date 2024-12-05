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
    private InterfaceNodeField[,] gridValues;

    // Menu item to open the editor window
    [MenuItem("Window/Grid Editor")]
    public static void ShowWindow()
    {
        GetWindow<GridEditorWindow>("Grid Editor");
    }

    private void OnEnable()
    {
        //EditorGUILayout.ObjectField("Grid", EditorStyles.boldLabel);


        // Initialize the grid with default values
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        // Reinitialize the gridValues array to match the updated rows and columns
        gridValues = new int[rows, columns];//INode
    }

    private WindowReference windowReference;

 
    private void OnGUI()
    {
        windowReference = (WindowReference)EditorGUILayout.ObjectField(
        "Window Reference",
        windowReference,
        typeof(WindowReference),
        false
    );

        if (windowReference != null && windowReference.linkedWindow != null)
        {
            //Rect newPosition = new Rect(30, 30, 500, 300); // (x, y, width, height)
            //windowReference.linkedWindow.position = newPosition;
            EditorGUILayout.LabelField($"Linked to: {windowReference.linkedWindow.titleContent.text}");
        }

        // Label at the top of the window
        EditorGUILayout.LabelField("Grid Editor", EditorStyles.boldLabel);

        // Input fields for rows and columns
        EditorGUILayout.BeginHorizontal();
        rows = EditorGUILayout.Vector3IntField("Rows", rows);
        columns = EditorGUILayout.Vector3IntField("Columns", columns);     //Vector3IntField
        EditorGUILayout.EndHorizontal();

        // Update grid if size changes
        if (GUILayout.Button("Update Grid"))
        {
            InitializeGrid();
        }

        // Draw the grid using nested loops
        EditorGUILayout.Space();
        for (int row = 0; row < rows; row++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int col = 0; col < columns; col++)
            {
                // Display an IntField for each grid cell
                gridValues[row, col] = EditorGUILayout.IntField(gridValues[row, col], GUILayout.Width(50));
            }

            EditorGUILayout.EndHorizontal();
        }

        // Optional reset button
        if (GUILayout.Button("Reset Grid"))
        {
            InitializeGrid();
        }
    }
}