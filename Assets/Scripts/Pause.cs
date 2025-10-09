using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] InputActionAsset input;
    bool paused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input.FindAction("Pause").started += OnPause;
        input.FindAction("Quit").started += OnQuit;
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        paused = !paused;
        Time.timeScale = paused ? 0.0f : 1.0f;
    }

    public void OnQuit(InputAction.CallbackContext ctx)
    {
#if UNITY_EDITOR
        Debug.Log("Quitting.");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
