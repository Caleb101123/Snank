using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public static LevelUp instance;

    [SerializeField] GameObject[] choice;
    List<Perk> perkList = new List<Perk>();
    Perk[] perkChoice;
    [SerializeField] Perk debug;

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
        Dictionary<string, Perk> perks = new Dictionary<string, Perk>();
        foreach (Perk p in perkList.Where((p) => p.repeatable || !Manager.instance.player.perks.Contains(p)))
        {
            perks.Add(p.name, p);
        }

        int[] selections = SelectThreeInts(perks.Count);

        //Select Perks
        perkChoice[0] = perks.Values.ElementAt(selections[0]);
        perkChoice[1] = perks.Values.ElementAt(selections[1]);
        perkChoice[2] = perks.Values.ElementAt(selections[2]);
        //Debug
        if (debug && (debug.repeatable || !Manager.instance.player.perks.Contains(debug)))
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

    private void Deactivate()
    {
        Time.timeScale = 1;
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
