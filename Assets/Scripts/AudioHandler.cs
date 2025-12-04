using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioClip menuMusic, gameMusic, collectSFX, gameOverSFX;
    [SerializeField] AudioSource music, sfx;

    public static AudioHandler instance;

    float masterVol = 1.0f, sfxVol = 1.0f, musicVol = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += SetMusic;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMusic(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartScene")
        {
            music.clip = menuMusic;
            music.Play();
        }
        else if (scene.name == "GameScene")
        {
            music.clip = gameMusic;
            music.Play();
        }
        else
        {
            music.enabled = false;
        }
    }

    public void PlaySFX(string call)
    {
        switch (call)
        {
            case "Collect":
                sfx.clip = collectSFX;
                break;

            case "Game Over":
                sfx.clip = gameOverSFX;
                break;
        }

        sfx.Play();
    }

    public void SetVolume(string type, float volume)
    {
        if (type == "Music")
        {
            musicVol = volume;
        }

        if (type == "SFX")
        {
            sfxVol = volume;
        }

        if (type == "Master")
        {
            masterVol = volume;
        }

        music.volume = musicVol * masterVol;
        sfx.volume = sfxVol * masterVol;
    }

    public void ToggleMute(string type, bool mute)
    {
        if (type == "Music")
        {
            music.mute = mute;
        }

        if (type == "SFX")
        {
            sfx.mute = mute;
        }
    }

    public bool GetMute(string type)
    {
        if (type == "Music")
        {
            return music.mute;
        }

        if (type == "SFX")
        {
            return sfx.mute;
        }

        return false;
    }
}
