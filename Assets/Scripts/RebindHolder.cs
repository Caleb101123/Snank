using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class RebindHolder : MonoBehaviour
{
    [SerializeField] string action;
    [SerializeField] TMP_Text actName, display1, display2, inputRequest;
    [SerializeField] InputActionAsset inputAction;

    [SerializeReference] string[] keys;
    InputAction act;

    InputActionRebindingExtensions.RebindingOperation rebind;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        act = inputAction.FindAction(action);
        //Debug.Log(act.name);
        keys = new string[2];
        RefreshKeys();
        actName.text = act.name + ":";
    }

    // Update is called once per frame
    void Update()
    {
        if (act.bindings[0].isComposite)
        {
            display1.text = act.GetBindingDisplayString(1);
            display2.text = act.GetBindingDisplayString(2);
        }
        else
        {
            display1.text = act.GetBindingDisplayString(0);
            if (act.bindings.Count > 1)
            {
                display2.text = act.GetBindingDisplayString(1);
            }
            else
            {
                display2.text = "";
            }
        }
    }

    public void Rebind (int val)
    {
        act.Disable();
        rebind = act.PerformInteractiveRebinding()
            .WithTargetBinding(val)
            .WithControlsHavingToMatchPath("<Keyboard>")
            .WithControlsExcluding("<Keyboard>/q")
            .WithControlsExcluding("<Keyboard>/escape")
            .OnComplete(_ => RebindComplete());
        rebind.Start();

        Debug.Log("Rebind start");
    }

    public void RebindComplete()
    {
        act.Enable();
        rebind.Dispose();
        rebind = null;
        Debug.Log("Rebind end");
    }
    /*
    public void PrepareToUpdate(int val)
    {
        inputAction.Disable();
        inputRequest.gameObject.SetActive(true);
        InputSystem.onAnyButtonPress.CallOnce(ctrl => UpdateAction(ctrl, val));
    }

    private void UpdateAction(InputControl ctrl, int val)
    {
        inputRequest.gameObject.SetActive(false);
        inputAction.Enable();
        keys[val] = ctrl.displayName;
        Rebinder.instance.SetInput(keys, act);
        RefreshKeys();
    }
    */
    public void RefreshKeys()
    {
        if (act.bindings.Count > 0)
        {
            keys[0] = act.bindings[0].ToDisplayString();
        }
        if (act.bindings.Count > 1)
        {
            keys[1] = act.bindings[1].ToDisplayString();
        }
    }
}
