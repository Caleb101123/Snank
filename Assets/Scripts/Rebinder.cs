using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rebinder : MonoBehaviour
{
    public InputActionAsset input;
    public static Rebinder instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ResetInput();
        }
        DebugTest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugTest()
    {
        /*foreach (var map in input.actionMaps.Where((map) => map.name != "UI"))
        {
            foreach (var bind in map.bindings)
            {
                Debug.Log(bind.action + ", " + bind.effectivePath);
            }
        }*/

        foreach (InputAction action in input)
        {
            string debug = action.name + "\nBindings: " + action.bindings.Count;

            for (int i = 0; i < action.bindings.Count; i++)
            {
                debug += "\n " + action.bindings[i].ToDisplayString(); 
            }
            Debug.Log(debug);
        }
    }

    public void SetInput(string[] keys, InputAction action)
    {

    }

    public void SetInput_old(string[] keys, InputAction action)
    {
        foreach (InputAction act in input)
        {
            for (int i = 0; i < act.bindings.Count; i++)
            {
                for (int j = 0; j < keys.Length; j++)
                {
                    if (act.bindings[i].effectivePath == "</Keyboards>/" + keys[j])
                    {
                        act.ApplyBindingOverride(i, action.bindings[j].effectivePath);
                        Debug.Log("Replaced action.");
                    }
                }
            }
        }
        Debug.Log("Replacements complete");

        Debug.Log(action.bindings.Count + " actions found");
        /*for (int i = 0; i < action.bindings.Count; i++)
        {
            action.ChangeBinding(i).Erase();
        }
        Debug.Log("Actions cleared");

        foreach (string k in keys)
        {
            action.AddBinding("<Keyboard>/" + k);
            Debug.Log("Added " + k);
        }*/

        for (int i = 0; i < keys.Length; i++)
        {
            if (action.bindings.Count < i)
            {
                action.AddBinding("<Keyboard>/" + keys[i]);
                continue;
            }
            action.ApplyBindingOverride(i, "</Keyboards>/" + keys[i]);
        }
    }

    public void ResetInput()
    {
        input.RemoveAllBindingOverrides();
        foreach (RebindHolder holder in GetComponentsInChildren<RebindHolder>())
        {
            holder.RefreshKeys();
        }
        Debug.Log("Reset Complete");
    }
}
