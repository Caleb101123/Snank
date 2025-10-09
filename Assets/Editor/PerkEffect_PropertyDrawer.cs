using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Linq;

//Major code reworking from prof - Hisham Ata

[CustomPropertyDrawer(typeof(PerkEffect), true)]
public class PerkEffect_PropertyDrawer : PropertyDrawer
{
    private static List<Type> perkEffectTypes;
    private static List<string> perkEffectTypeNames;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Initialize type lists if not already done
        if (perkEffectTypes == null)
        {
            InitializePerkEffectTypes();
        }

        var container = new VisualElement();

        // Get the current type name from the managedReferenceFullTypename
        string currentTypeName = GetTypeNameFromProperty(property);
        int currentIndex = perkEffectTypeNames.IndexOf(currentTypeName);
        if (currentIndex < 0) currentIndex = 0;

        var typeDropdown = new DropdownField("Type", perkEffectTypeNames, currentIndex);
        typeDropdown.RegisterValueChangedCallback(evt => OnTypeChanged(evt, property, container));
        container.Add(typeDropdown);

        DrawFieldsForCurrentType(property, container);

        return container;
    }

    private void InitializePerkEffectTypes()
    {
        perkEffectTypes = new List<Type>();
        perkEffectTypeNames = new List<string>();

        // Get all types that inherit from PerkEffect (excluding PerkEffect itself)
        var allTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(PerkEffect)))
            .OrderBy(t => t.Name)
            .ToList();

        foreach (var type in allTypes)
        {
            perkEffectTypes.Add(type);
            perkEffectTypeNames.Add(type.Name);
        }

        // Add base PerkEffect as an option
        perkEffectTypes.Insert(0, typeof(PerkEffect));
        perkEffectTypeNames.Insert(0, "PerkEffect");
    }

    private string GetTypeNameFromProperty(SerializedProperty property)
    {
        if (!string.IsNullOrEmpty(property.managedReferenceFullTypename))
        {
            // Format is "AssemblyName TypeName"
            var parts = property.managedReferenceFullTypename.Split(' ');
            if (parts.Length > 1)
            {
                return parts[1];
            }
        }
        return "Default";
    }

    private void OnTypeChanged(ChangeEvent<string> evt, SerializedProperty property, VisualElement container)
    {
        int selectedIndex = perkEffectTypeNames.IndexOf(evt.newValue);
        if (selectedIndex < 0) return;

        Type selectedType = perkEffectTypes[selectedIndex];

        // Create a new instance of the selected type
        object newInstance = Activator.CreateInstance(selectedType);

        // Set the managed reference to the new instance
        property.managedReferenceValue = newInstance;
        property.serializedObject.ApplyModifiedProperties();

        // Rebuild the UI to show the new fields
        RebuildFieldsUI(property, container);
    }

    private void RebuildFieldsUI(SerializedProperty property, VisualElement container)
    {
        // Remove all children except the dropdown (first element)
        while (container.childCount > 1)
        {
            container.RemoveAt(1);
        }

        // Redraw fields for the new type
        DrawFieldsForCurrentType(property, container);
    }

    private void DrawFieldsForCurrentType(SerializedProperty property, VisualElement container)
    {
        // Iterate through all child properties and create fields for them
        SerializedProperty iterator = property.Copy();
        SerializedProperty endProperty = iterator.GetEndProperty();

        // Enter the first child
        if (iterator.NextVisible(true))
        {
            do
            {
                // Stop if we've reached the end of this property
                if (SerializedProperty.EqualContents(iterator, endProperty))
                    break;

                // Create a property field for each child property
                var field = new PropertyField(iterator.Copy());
                field.Bind(property.serializedObject);
                container.Add(field);
            }
            while (iterator.NextVisible(false));
        }
    }
}