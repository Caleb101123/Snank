using UnityEngine;

//Minor code changes from prof - Hisham Ata

[CreateAssetMenu(fileName = "NewPerk", menuName = "ScriptableObjects/Perk")]
public class Perk : ScriptableObject
{
    [SerializeField] private Sprite img;
    public Sprite Img { get { return img; } private set { img = value; } }

    [SerializeReference] private PerkEffect[] effects;
    public PerkEffect[] Effects { get { return effects; } private set { effects = value; } }

    [SerializeField] private bool repeatable;

    public void Gain()
    {
        if (effects.Length > 0)
        {
            foreach (PerkEffect effect in Effects)
            {
                effect.Execute(Manager.instance.player);
            }
        }
    }
}