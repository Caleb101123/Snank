using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    int score = 0;
    float timer = 10.0f;
    [SerializeField] TMP_Text gameOver;
    [SerializeField] SpriteRenderer[] playerSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        gameOver.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        foreach (SpriteRenderer r in playerSprite)
        {
            r.color = new Color((10.0f - timer) / 10.0f, timer / 10.0f, 0);
        }
        if (timer <= 0)
        {
            gameOver.text = "Game Over\nFinal Score: " + score;
            Time.timeScale = 0.0f;
        }
    }

    public void Score()
    {
        score++;
        timer = 10.0f;
    }
}
