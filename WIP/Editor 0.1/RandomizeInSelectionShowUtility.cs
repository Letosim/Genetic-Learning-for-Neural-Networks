// Simple script that randomizes the rotation of the selected GameObjects.
using UnityEditor.UIElements;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.ComponentModel.Design;

//[MenuItem("Examples/Ingredient Drawer Test")]
public class RandomizeInSelectionShowUtility : EditorWindow//       Inherits from:ScriptableObject
{

    private SerializedObject serializedContainer;
    private InterfaceNodeContainer container;

    [MenuItem("Window/Node Editor")]//init.....
    public static void ShowWindow()
    {
        GetWindow<RandomizeInSelectionShowUtility>("Node Editor");
        Debug.Log(".....");

    }

    //[MenuItem("Examples/Node Editor")]
    static void Init()
    {
        RandomizeInSelectionShowUtility window =
            EditorWindow.GetWindow<RandomizeInSelectionShowUtility>(true, "Node Editor");
        window.position = new Rect(50, 50, 400, 300);
        window.Show();




    }



    //static void Init()
    //{
    //    RandomizeInSelectionShowUtility window = EditorWindow.GetWindow<RandomizeInSelectionShowUtility>(
    //        true,
    //        "Randomize Objects"
    //    );
    //    window.position = new Rect(50, 50, 400, 300);
    //    window.ShowUtility(); // Use only this for a utility window nested!?
    //}

    private void OnEnable()
    {
        container = ScriptableObject.CreateInstance<InterfaceNodeContainer>();


        container.interfaceNode = new InterfaceNodeField
        {
            neurode = new Vector3Int[] { new Vector3Int(10,3), new Vector3Int(10, 3), new Vector3Int(10, 3) }  // count type depth
        };

        serializedContainer = new SerializedObject(container);
    }


    private void OnGUI()
    {
        // Update the SerializedObject
        serializedContainer.Update();

        container = CreateInstance<InterfaceNodeContainer>();

        Type ingredientType = typeof(InterfaceNodeField);
        FieldInfo[] fields = ingredientType.GetFields();


        // Access the SerializedProperty for the Ingredient     

        serializedContainer.FindProperty("interfaceNode");

        SerializedProperty ingredientProperty = serializedContainer.FindProperty("interfaceNode");//Link/Enum

        Debug.Log(ingredientProperty.boxedValue); // `boxedValue` can be used in newer Unity versions

        object array = fields[0].GetValue(ingredientProperty.boxedValue);

        Debug.Log(array);


        //ingredientProperty = ingredientProperty as Ingredient;
        SerializedProperty statsProperty = ingredientProperty.FindPropertyRelative("stats");


        if (statsProperty != null)
        {
            SerializedProperty strengthProperty = statsProperty.FindPropertyRelative("strength");

        }



        //if (ingredientProperty != null)
        //{
        //    // Use the custom PropertyDrawer to render the Ingredient
        //    var drawer = new IngredientDrawer();                                            //.............................
        //    var position = new Rect(10, 10, 200, 200);
        //    drawer.OnGUI(position, ingredientProperty, new GUIContent("1 Ingredient"));//Name
        //}
        //else
        //{
        //    EditorGUILayout.LabelField("SerializedProperty not found!");
        //}


        //var drawerB = new IngredientDrawer();
        //var positionB = new Rect(300, 10, 200, 200);
        //drawerB.OnGUI(positionB, ingredientPropertyB, new GUIContent("2 Ingredient"));//Name

        // Apply changes back to the SerializedObject
        serializedContainer.ApplyModifiedProperties();
    }


    //// IngredientDrawer
    //[CustomPropertyDrawer(typeof(Ingredient))]
    //public class IngredientDrawer : PropertyDrawer  //Costum drawer         Inherits from:GUIDrawer
    //{
    //    // Draw the property inside the given rect
    //    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //    {
    //        // Using BeginProperty / EndProperty on the parent property means that
    //        // prefab override logic works on the entire property.
    //        EditorGUI.BeginProperty(position, label, property);//------------

    //        // Draw label
    //        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

    //        // Don't make child fields be indented
    //        var indent = EditorGUI.indentLevel;
    //        EditorGUI.indentLevel = 0;

    //        // Calculate rects
    //        var amountRect = new Rect(position.x, position.y, 30, position.height);
    //        var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
    //        var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

    //        // Draw fields - pass GUIContent.none to each so they are drawn without labels
    //        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
    //        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
    //        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

    //        // Set indent back to what it was
    //        EditorGUI.indentLevel = indent;

    //        EditorGUI.EndProperty();//-------
    //    }
    //}


    public static class Arrays
    {

        //List<List<vector3Int[,] gridValues>>;



        Arrays()
        {
            grid = new Grid();
        }
        

        
        private class Grid
        {
                

                public Grid()
                {
                    vector3Int = new Vector3Int { new Vector3Int(10, 3, 3), new Vector3Int(10, 3, 3), new Vector3Int(10, 3, 3) };
                }
        }
    }


}
