using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.Linq;
using Codice.Client.BaseCommands.Merge.Xml;

//TODO: Finish This

[CustomPropertyDrawer(typeof(PerkEffect))]
public class PerkEffect_PropertyDrawer : PropertyDrawer
{
    Type[] types = Assembly.GetAssembly(typeof(PerkEffect)).GetTypes();
    List<string> typeNames = new List<string>();
    Dictionary<string, Type> typeDict = new Dictionary<string, Type>();

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var popup = new UnityEngine.UIElements.PopupWindow();
        popup.text = "Perk Effects";

        foreach (Type type in types)
        {
            if (typeNames.Contains(type.Name)) continue;
            typeNames.Add(type.Name);
            typeDict.Add(type.Name, type);
        }
        DropdownField typeChoice = new DropdownField("Type", typeNames, typeNames.IndexOf(property.type));

        typeChoice.RegisterValueChangedCallback(type => { ChangeType(type, ref property); } );

        popup.Add(typeChoice);
        return popup;
    }

    private void ChangeType (ChangeEvent<string> _event, ref SerializedProperty prop)
    {
        Type type = typeDict[_event.newValue];
        ConstructorInfo construct = type.GetConstructor(new[] { typeof(int) });
        prop = (SerializedProperty)Convert.ChangeType(construct.Invoke(null, new object[] { 0 }), type);
    }
}
