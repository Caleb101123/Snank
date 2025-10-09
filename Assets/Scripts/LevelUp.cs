using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public static LevelUp instance;

    [SerializeField] GameObject[] choice;
    Dictionary<string, Perk> perks = new Dictionary<string, Perk>();
    Perk[] perkChoice;

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

        foreach (Perk p in Resources.LoadAll("Perks"))
        {
            perks.Add(p.name, p);
        }
        perkChoice = new Perk[3];

        gameObject.SetActive(false);
    }

    public void Activate()
    {
        //Select Perks
        switch (Random.Range(0, 3))
        {
            case 0:
                perkChoice[0] = perks["IncreaseTurnRate"];
                perkChoice[1] = perks["DecreaseTurnRate"];
                break;

            case 1:
                perkChoice[0] = perks["IncreaseSpeed"];
                perkChoice[1] = perks["DecreaseSpeed"];
                break;

            case 2:
                perkChoice[0] = perks["IncreaseTimer"];
                perkChoice[1] = perks["DecreaseTimer"];
                break;
        }
        perkChoice[2] = perks["Pass"];

        for (int i = 0; i < choice.Length; i++)
        {
            choice[i].GetComponent<Image>().sprite = perkChoice[i].Img;
            choice[i].GetComponent<Button>().onClick.RemoveAllListeners();
            choice[i].GetComponent<Button>().onClick.AddListener(perkChoice[i].Gain);
            choice[i].GetComponent<Button>().onClick.AddListener(Deactivate);
        }
    }

    private void Deactivate()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
