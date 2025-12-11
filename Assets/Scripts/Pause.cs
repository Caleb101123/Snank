using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] InputActionAsset input;
    bool paused = false;
    [SerializeField] GameObject pausePanel;
    TMP_Text status;


    [SerializeField] GameObject prefab;
    [SerializeField] GameObject perkDisplay;
    [SerializeField] Player player;
    List<GameObject> old = new List<GameObject>();

    [SerializeField] Toggle sfxToggle, musicToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input.FindAction("Pause").started += OnPause;
        input.FindAction("Quit").started += OnQuit;
        input.FindAction("Restart").started += OnReload;

        pausePanel.SetActive(false);
        status = pausePanel.transform.Find("StatsText").GetComponent<TMP_Text>();

        sfxToggle.isOn = !AudioHandler.instance.GetMute("SFX");
        musicToggle.isOn = !AudioHandler.instance.GetMute("Music");
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (GameOver.go) 
            return;

        if (LevelUp.pause)
            return;

        paused = !paused;
        pausePanel.SetActive(paused);


        if (paused)
        {
            DisplayPerks();
            status.text = "Score: " + Manager.instance.score;
            status.text += "\nSpeed: " + Manager.instance.player.speed;
            status.text += "\nMultiplier: " + Manager.instance.scoreMult;
            status.text += "\nPerk Count: " + Manager.instance.player.perks.Count;
            status.text += "\nTimer: " + string.Format("{0:0.00}s", 10 / Manager.instance.timeMult);
            if (Manager.instance.hyperCooldown > 0)
            {
                status.text += "\nHypertime Cooldown: " + string.Format("{0:0.00}s", Manager.instance.hyperCooldown);
            }
        }
        float unpauseScale = Manager.instance.hyper ? 1.0f / Manager.instance.hyperMult : 1.0f;
        Time.timeScale = paused ? 0.0f : unpauseScale;
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
        Reload();
    }

    public void Reload()
    {
        input.FindAction("Pause").started -= OnPause;
        input.FindAction("Quit").started -= OnQuit;
        input.FindAction("Restart").started -= OnReload;

        GameOver.go = false;
        LevelUp.pause = false;
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        SceneManager.LoadScene("GameScene");
    }

    public void Return()
    {
        input.FindAction("Pause").started -= OnPause;
        input.FindAction("Quit").started -= OnQuit;
        input.FindAction("Restart").started -= OnReload;

        GameOver.go = false;
        LevelUp.pause = false;
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = 0.02f;
        SceneManager.LoadScene("StartScene");
    }

    public void ToggleMusic(bool toggle)
    {
        AudioHandler.instance.ToggleMute("Music", !toggle);
    }

    public void ToggleSFX(bool toggle)
    {
        AudioHandler.instance.ToggleMute("SFX", !toggle);
    }


    private void DisplayPerks()
    {
        int i = 0;

        for (int n = old.Count - 1; n >= 0; n--)
        {
            Destroy(old[n]);
        }

        old = new List<GameObject>();

        foreach (Perk p in player.perks.Distinct())
        {
            GameObject perk = Instantiate(prefab, perkDisplay.transform);
            perk.GetComponent<Image>().sprite = player.perks[i].Img;
            RectTransform rect = perk.GetComponent<RectTransform>();
            rect.localPosition = new Vector2((120 * (i % 8)) - 420, 100 - (120 * (i / 8)));

            perk.GetComponentInChildren<TMP_Text>().text = "x" + player.perks.Where(x => x.name == p.name).Count();
            old.Add(perk);
            i++;
        }

        /*
        for (int i = 0; i < player.perks.Count; i++)
        {
            perk.GetComponent<Image>().sprite = player.perks[i].Img;
            RectTransform rect = perk.GetComponent<RectTransform>();
            rect.localPosition = new Vector2((120 * (i % 8)) - 420, 100 - (120 * (i / 8)));
            Debug.Log("Added perk " + i);
        }
        Debug.Log("Finished adding perks");*/
    }
}
