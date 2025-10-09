using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


[CustomEditor(typeof(Perk))]
public class Perk_Inspector : Editor
{

    public VisualTreeAsset inspectorUXML;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement perkView = new VisualElement();

        perkView.Add(new Label("Perk Inspector"));

        if (inspectorUXML != null)
        {
            VisualElement uxml = inspectorUXML.CloneTree();
            perkView.Add(uxml);
        }

        return perkView;
    }

    public override void OnInspectorGUI()
    {

    }
}
