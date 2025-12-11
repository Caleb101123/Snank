using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public static LevelUp instance;

    [SerializeField] GameObject[] choice;
    [SerializeField] TMP_Text descBox;
    List<Perk> perkList = new List<Perk>();
    Perk[] perkChoice;
    [SerializeField] Perk debug;

    public static bool pause = false;

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
            perkList.Add(p);
        }
        perkChoice = new Perk[3];

        gameObject.SetActive(false);
    }

    public void Activate()
    {
        pause = true;
        Dictionary<string, Perk> perks = new Dictionary<string, Perk>();
        foreach (Perk p in perkList.Where((p) => !Manager.instance.player.AtCap(p)))
        {
            perks.Add(p.name, p);
        }

        int[] selections = SelectThreeInts(perks.Count);

        //Select Perks
        perkChoice[0] = perks.Values.ElementAt(selections[0]);
        perkChoice[1] = perks.Values.ElementAt(selections[1]);
        perkChoice[2] = perks.Values.ElementAt(selections[2]);
        //Debug
        if (debug && !Manager.instance.player.AtCap(debug))
        {
            perkChoice[2] = debug;
        }
        /*
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
        */
        for (int i = 0; i < choice.Length; i++)
        {
            choice[i].GetComponent<Image>().sprite = perkChoice[i].Img;
            choice[i].GetComponent<Button>().onClick.RemoveAllListeners();
            choice[i].GetComponent<Button>().onClick.AddListener(perkChoice[i].Gain);
            choice[i].GetComponent<Button>().onClick.AddListener(Deactivate);
        }
    }

    public void SetText(int choice)
    {
        if (choice == -1)
        {
            descBox.text = string.Empty;
        }
        else
        {
            descBox.text = perkChoice[choice].description;
        }
    }

    private void Deactivate()
    {
        Time.timeScale = 1;
        pause = false;
        gameObject.SetActive(false);
    }

    private int[] SelectThreeInts(int size)
    {
        if (size < 3)
            return null;
        int[] val = new int[3];

        val[0] = Random.Range(0, size);
        do
        {
            val[1] = Random.Range(0, size);
        } while (val[1] == val[0]);

        do
        {
            val[2] = Random.Range(0, size);
        } while (val[2] == val[0] ||  val[2] == val[1]);

        return val;
    }
}
