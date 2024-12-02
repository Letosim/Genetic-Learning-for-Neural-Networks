// Simple script that randomizes the rotation of the selected GameObjects.
using UnityEditor.UIElements;
using UnityEngine;
using UnityEditor;
using System;


//[MenuItem("Examples/Ingredient Drawer Test")]
public class RandomizeInSelectionShowUtility : EditorWindow//       Inherits from:ScriptableObject
{

    private SerializedObject serializedContainer;
    private IngredientContainer container;

    [MenuItem("Examples/Randomize Objects")]
    public static void ShowWindow()
    {
        GetWindow<RandomizeInSelectionShowUtility>("Randomize Objects");
    }

    private void OnEnable()
    {
        // Load or create the ScriptableObject container
        container = CreateInstance<IngredientContainer>();

        container.testIngredients = new Ingredient[10];

        for(int i = 0; i < 10; i++)
            container.testIngredients[i] = new Ingredient
            {
                name = "Sugar",
                amount = 2,
                unit = IngredientUnit.Spoon,
                //potionIngredients = new int[,] { { 0, 0 }, { 0, 0 } }
                //potionIngredients = new int[] { 0, 0, 0, 0 }
            };

        // Assign a test value for the Ingredient
        container.testIngredient = new Ingredient
        {
            name = "Sugar",
            amount = 2,
            unit = IngredientUnit.Spoon,
            //potionIngredients = new int[,] { { 0, 0 }, { 0, 0 } }
            potionIngredients = new int[] {  0, 0 ,  0, 0  }
        };

        // Wrap the ScriptableObject in a SerializedObject
        serializedContainer = new SerializedObject(container);







    }

    private void OnGUI()
    {
        // Update the SerializedObject
        serializedContainer.Update();

        // Access the SerializedProperty for the Ingredient
        SerializedProperty ingredientProperty = serializedContainer.FindProperty("testIngredient");//Link/Enum
        SerializedProperty ingredientPropertyB = serializedContainer.FindProperty("testIngredients");//Link/Enum


        

        if (ingredientProperty != null)
        {
            // Use the custom PropertyDrawer to render the Ingredient
            var drawer = new IngredientDrawer();
            var position = new Rect(10, 10, 200, 200);
            drawer.OnGUI(position, ingredientProperty, new GUIContent("1 Ingredient"));//Name
        }
        else
        {
            EditorGUILayout.LabelField("SerializedProperty not found!");
        }


        var drawerB = new IngredientDrawer();
        var positionB = new Rect(300, 10, 200, 200);
        drawerB.OnGUI(positionB, ingredientPropertyB[0], new GUIContent("2 Ingredient"));//Name

        // Apply changes back to the SerializedObject
        serializedContainer.ApplyModifiedProperties();
    }

    [CreateAssetMenu(menuName = "Test/IngredientContainer")]
    public class IngredientContainer : ScriptableObject
    {
        public Ingredient testIngredient;

        public Ingredient[] testIngredients;
    }
}












    //System.Random random = new System.Random();
    //public float rotationAmount;

    //[SerializeField]
    //public string selected = "";

    //RandomizeInSelectionShowUtility rndWindow;

    //[MenuItem("Examples/Randomize Objects")]
    //static void Init()
    //{
    //    //this.UIElementsModule.EventBase

    //    //FocusEvent

    //    RandomizeInSelectionShowUtility window =
    //        EditorWindow.GetWindow<RandomizeInSelectionShowUtility>(true, "Randomize Objects");
    //    window.position = new Rect(50, 50, 400, 300);
    //    window.Show();
    //    window.ShowUtility();
    //}

    //private WindowReference windowReference;


    //public void Hmmm()
    //{
    //    Debug.Log("?");
    //}

    //private Ingredient testIngredient;
    //private SerializedObject serializedObject;

    //private void OnEnable()
    //{
    //    //testIngredient...
    //    testIngredient = new Ingredient();
    //    serializedObject = new SerializedObject(this);
    //}

    //private void OnGUI()
    //{
    //    serializedObject.Update();

    //    // Create a temporary SerializedProperty for testing
    //    SerializedProperty ingredientProperty = serializedObject.FindProperty("selected");

    //    // Use the IngredientDrawer
    //    var drawer = new IngredientDrawer();
    //    var position = new Rect(10, 10, 40 - 20, 20);
    //    drawer.OnGUI(position, ingredientProperty, new GUIContent("Test Ingredient"));

    //    serializedObject.ApplyModifiedProperties();
    //}



    //void CreateGUI()
    //{

    //    //rootVisualElement.Add(IngredientDrawer);


    //    //GUILayout.Label("This is a label", EditorStyles.boldLabel);

    //    //sliderValue = EditorGUILayout.Slider("Slider", sliderValue, 0f, 10f);
    //    //if (GUILayout.Button("Print Value"))
    //    //{
    //    //    Debug.Log($"Slider Value: {sliderValue}");
    //    //}


    //    //var objectField = new ObjectField("Window Reference")
    //    //{
    //    //    objectType = typeof(WindowReference),
    //    //    allowSceneObjects = false
    //    //};


    //    //var objectFieldB = new ObjectField("Window Reference")
    //    //{
    //    //    objectType = typeof(WindowReference),
    //    //    allowSceneObjects = false
    //    //};


    //    //rootVisualElement.Add("This is a label");

    //    //var label = objectField.Q<UnityEditor.UIElements.Label>();
    //    //if (label != null)
    //    //{
    //    //    label.RegisterCallback<FocusEvent>(evt => Hmmm());
    //    //}
    //    //else
    //    //{
    //    //    Debug.LogWarning("Label not found in ObjectField.");
    //    //}

    //    //objectField.label.RegisterCallback<FocusEvent>(evt => Hmmm());

    //    //CreatePropertyGUI(...)
    //    //Node myElement = new Node();
    //    //myElement.CreatePropertyGUI
    //    //rootVisualElement.Add(myElement);
    //    //myElement.RegisterCallback<FocusEvent>(evt => Hmmm());

    //    //position = new Rect(position.x, position.y, 600, 400);

    //    // Example: Resize linked windowReferenceA's linked window
    //    //if (windowReferenceA != null && windowReferenceA.linkedWindow != null)
    //    //{
    //    //    windowReferenceA.linkedWindow.position = new Rect(100, 100, 500, 300);
    //    //}

    //    //// Example: Resize linked windowReferenceB's linked window
    //    //if (windowReferenceB != null && windowReferenceB.linkedWindow != null)
    //    //{
    //    //    windowReferenceB.linkedWindow.position = new Rect(700, 100, 500, 300);
    //    //}

    //    //        windowA.ShowUtility();

    //    //Debug.Log(objectFieldB[0].style);
    //    //objectFieldB[0].linkedWindow.position = new Rect(30, 30, 500, 300); + parent

    //    //objectFieldB = objectFieldB as WindowReference;

    //    //objectFieldB.linkedWindow.position = new Rect(30, 30, 500, 300);

    //    //rootVisualElement.Add("This is a label");
    //    //rootVisualElement.Add(objectFieldB);


    //}

    //void RandomizeSelected()
    //{
    //    //foreach (var transform in Selection.transforms)
    //    //{
    //    //    Quaternion rotation = Random.rotation;
    //    //    rotationAmount = (float)random.NextDouble();
    //    //    transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, rotationAmount);
    //    //}
    //}
//}