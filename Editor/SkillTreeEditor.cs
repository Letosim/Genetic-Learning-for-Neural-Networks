using UnityEditor;
using UnityEngine;

public class SkillTreeEditor : EditorWindow
{
    private GraphNode[] skillTreeNodes;

    [MenuItem("Window/Skill Tree Editor")]
    public static void ShowWindow()
    {
        GetWindow<SkillTreeEditor>("Skill Tree Editor");
    }

    private void OnEnable()
    {
        // Initialize the skill tree nodes array
        skillTreeNodes = new GraphNode[5]; // Just an example, this can be dynamically              added GridEditorWindow

        // Example nodes setup
        for (int i = 0; i < skillTreeNodes.Length; i++)
        {
            skillTreeNodes[i] = new GraphNode
            {
                nodeID = i,
                isActive = false,
                isRoot = (i == 0)  // Setting the first node as root
            };
            //(int nodeID,bool isActive ,bool isRoot)
        }






    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        // Add some UI for the skill tree nodes (e.g., activating, deactivating)
        for (int i = 0; i < skillTreeNodes.Length; i++)
        {
            DrawNodeEditor(skillTreeNodes[i]);
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawNodeEditor(GraphNode node)//[[count][type 1-3]] <-link-> [[count][type 1-3]]                       [[[count][type 1-3]] <-link-> [[count][type 1-3]]]  <-link->  [[[count][type 1-3]] <-link-> [[count][type 1-3]]]
    {
        EditorGUILayout.BeginHorizontal();

        // Display node ID and activate/deactivate buttons
        //EditorGUILayout.LabelField("Node ID: " + node.nodeID);
        //node.activate = GUILayout.Toggle(node.activate, "Activate");
        //node.deactivate = GUILayout.Toggle(node.deactivate, "Deactivate");

        // Draw connected nodes (just a simple array for this example)
        //EditorGUILayout.LabelField("Connected Nodes: ");//                              Acces field!??
        //foreach (var connectedNode in node.connectedNodes)
        //{
        //    EditorGUILayout.LabelField(" - Node ID: " + connectedNode.nodeID);
        //}

        if (GUILayout.Button("Activate Node"))
        {
            //node.Activate();
        }

        if (GUILayout.Button("Deactivate Node"))
        {
            //node.Deactivate();
        }

        EditorGUILayout.EndHorizontal();
    }
}