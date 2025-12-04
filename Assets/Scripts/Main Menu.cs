using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject mainMenu, instructions, controls;
    [SerializeField] private TMP_Text score;

    void Start()
    {
        score.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMain()
    {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        controls.SetActive(false);
    }

    public void ToInstructions()
    {
        instructions.SetActive(true);
        mainMenu.SetActive(false);
        controls.SetActive(false);
    }

    public void ToControls()
    {
        controls.SetActive(true);
        mainMenu.SetActive(false);
        instructions.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        Debug.Log("Quitting.");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ToggleSimpleBackground(bool toggle)
    {
        Background.killOnLoad = toggle;
    }

    public void ToggleMusic(bool toggle)
    {
        AudioHandler.instance.ToggleMute("Music", !toggle);
    }

    public void ToggleSFX(bool toggle)
    {
        AudioHandler.instance.ToggleMute("SFX", !toggle);
    }
}
