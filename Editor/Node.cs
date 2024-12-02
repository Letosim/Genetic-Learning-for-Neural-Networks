//Certainly! There are a few issues and enhancements to address in your script:

//    CreatePropertyGUI Setup Issue: The PropertyField initialization for amountField is incorrect. PropertyField is a UIElement and requires a SerializedProperty to initialize.
//    Hmmm Class: If Hmmm is a Serializable class, it should be defined outside the Node class to avoid confusion, especially when using custom drawers.
//    Field Labels and Accessibility: The use of custom labels for fields like "Fancy Name" can be reintroduced and clarified.
//    Best Practices: Use proper namespaces and ensure the fields in Hmmm align with Unity's serialization requirements.

//Here's the corrected and enhanced version:

using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

// eventbase >> random >> prop

// IngredientDrawerUIE
[CustomPropertyDrawer(typeof(Hmmm))]
public class Node : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var amountField = new PropertyField(property.FindPropertyRelative("amount"), "Amount");
        //var unitField = new PropertyField(property.FindPropertyRelative("unit"));
        //var nameField = new PropertyField(property.FindPropertyRelative("name"), "Fancy Name");

        // Add fields to the container.
        container.Add(amountField);
        //container.Add(unitField);
        //container.Add(nameField);
        
        return container;
    }


 
}
public static class VisualElementExtensions
{
    public static void SendEvent<T>(this VisualElement element, T eventInstance) where T : EventBase
    {
        if (element == null || eventInstance == null)
            throw new System.ArgumentNullException("Element or event instance cannot be null.");

        element.SendEvent(eventInstance);
    }
}
[Serializable]
public class Hmmm
{
    public int amount = 1;

}