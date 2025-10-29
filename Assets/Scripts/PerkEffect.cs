using UnityEngine;
using System.Linq;

//Minor code reworking from prof - Hisham Ata


[System.Serializable]
public abstract class PerkEffect
{
    public virtual void Execute(Player player)
    {
        Debug.Log("Null Effect Called");
    }

    public virtual void Undo(Player player)
    {
        Debug.Log("Null Effect Undone");
    }
}

[System.Serializable]
public class SteeringPerkEffect : PerkEffect
{
    public int change;

    public override void Execute(Player player)
    {
        player.turnRate += change;
    }

    public override void Undo(Player player)
    {
        player.turnRate -= change;
    }
}

[System.Serializable]
public class SpeedPerkEffect : PerkEffect
{
    public int change;

    public override void Execute(Player player)
    {
        player.speed += change;
    }

    public override void Undo(Player player)
    {
        player.speed -= change;
    }
}



[System.Serializable]
public class PointMultiplierEffect : PerkEffect
{
    public int multiplier;

    public override void Execute(Player player)
    {
        Manager.instance.scoreMult += multiplier;
    }

    public override void Undo(Player player)
    {
        Manager.instance.scoreMult -= multiplier;
    }
}

[System.Serializable]
public class TimeMultiplierEffect : PerkEffect
{
    public float multiplier;

    public override void Execute(Player player)
    {
        Manager.instance.timeMult *= multiplier;
    }

    public override void Undo(Player player)
    {
        Manager.instance.timeMult /= multiplier;
    }
}

[System.Serializable]
public class ClearPerkEffect : PerkEffect
{
    public override void Execute(Player player)
    {
        if (player.perks.Where(perk => perk.Effects.Length > 0).Count() > 0)
        {
            Perk target;
            do
            {
                target = player.perks[Random.Range(0, player.perks.Count)];
            } while (target.Effects.Length == 0);
            target.Remove();
            player.removedPerks.Add(target);
        }
    }

    public override void Undo(Player player)
    {
        if (player.removedPerks.Count() > 0)
        {
            Perk target = player.removedPerks[player.removedPerks.Count - 1];
            target.Gain();
            player.removedPerks.Remove(target);
        }
    }
}

[System.Serializable]
public class EdgeBehaviourPerkEffect : PerkEffect
{
    public EdgeBehaviour newBehaviour;

    public override void Execute(Player player)
    {
        foreach (Perk p in player.perks)
        {
            foreach (PerkEffect e in p.Effects)
            {
                if (e.GetType() == typeof(EdgeBehaviourPerkEffect))
                {
                    p.Remove();
                }
            }
        }

        player.edgeBehaviour = newBehaviour;
    }

    public override void Undo(Player player)
    {
        player.edgeBehaviour = EdgeBehaviour.Wrap;
    }
}

[System.Serializable]
public class ScalePlayerPerkEffect : PerkEffect
{
    public float change;

    public override void Execute(Player player)
    {
        player.transform.localScale *= change;
    }

    public override void Undo(Player player)
    {
        player.transform.localScale /= change;
    }
}

[System.Serializable]
public class ScaleCollectablePerkEffect : PerkEffect
{
    public float change;

    public override void Execute(Player player)
    {
        Manager.instance.ballScale *= change;
    }

    public override void Undo(Player player)
    {
        Manager.instance.ballScale /= change;
    }
}