using UnityEngine;

//Minor code changes from prof - Hisham Ata

[CreateAssetMenu(fileName = "NewPerk", menuName = "ScriptableObjects/Perk")]
public class Perk : ScriptableObject
{
    [SerializeField] private Sprite img;
    public Sprite Img { get { return img; } private set { img = value; } }

    [SerializeReference] private PerkEffect[] effects;
    public PerkEffect[] Effects { get { return effects; } private set { effects = value; } }

    //[SerializeField] public bool repeatable;
    [SerializeField] public int cap = -1;

    public void Gain()
    {
        if (effects.Length > 0)
        {
            foreach (PerkEffect effect in Effects)
            {
                effect.Execute(Manager.instance.player);
            }
        }
        Manager.instance.player.perks.Add(this);
    }

    public void Remove()
    {
        if (effects.Length > 0)
        {
            foreach (PerkEffect effect in Effects)
            {
                effect.Undo(Manager.instance.player);
            }
        }
        Manager.instance.player.perks.Remove(this);
    }
}