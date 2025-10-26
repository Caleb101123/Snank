using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject perkDisplay;
    [SerializeField] GameObject prefab;
    [SerializeField] Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = "";
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute()
    {
        text.text = "Game Over\nFinal Score: " + Manager.instance.score;
        DisplayPerks();
        Time.timeScale = 0.0f;
    }

    private void DisplayPerks()
    {
        for (int i = 0; i < player.perks.Count; i++)
        {
            GameObject perk = Instantiate(prefab, perkDisplay.transform);
            perk.GetComponent<Image>().sprite = player.perks[i].Img;
            RectTransform rect = perk.GetComponent<RectTransform>();
            rect.localPosition = new Vector2((120*(i%8))-420, 100-(120*(i/8)));
            Debug.Log("Added perk " + i);
        }
        Debug.Log("Finished adding perks");
    }
}
