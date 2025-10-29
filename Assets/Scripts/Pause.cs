using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] InputActionAsset input;
    bool paused = false;
    [SerializeField] GameObject pausePanel;
    TMP_Text status;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input.FindAction("Pause").started += OnPause;
        input.FindAction("Quit").started += OnQuit;
        input.FindAction("Restart").started += OnReload;

        pausePanel.SetActive(false);
        status = pausePanel.transform.Find("StatsText").GetComponent<TMP_Text>();
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        paused = !paused;
        pausePanel.SetActive(paused);
        if (paused)
        {
            status.text = "Score: " + Manager.instance.score;
            status.text += "\nSpeed: " + Manager.instance.player.speed;
            status.text += "\nMultiplier: " + Manager.instance.scoreMult;
            status.text += "\nPerk Count: " + Manager.instance.player.perks.Count;
            status.text += "\nTimer: " + string.Format("{0:0.00}s", 10 / Manager.instance.timeMult);
        }
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

    public void OnReload(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene("GameScene");
    }
}
