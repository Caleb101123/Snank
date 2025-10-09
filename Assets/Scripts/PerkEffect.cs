using UnityEngine;

//Minor code reworking from prof - Hisham Ata


[System.Serializable]
public abstract class PerkEffect
{
    public virtual void Execute(Player player)
    {
        Debug.Log("Null Effect Called");
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
}

[System.Serializable]
public class SpeedPerkEffect : PerkEffect
{
    public int change;

    public override void Execute(Player player)
    {
        player.speed += change;
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
}

[System.Serializable]
public class TimeMultiplierEffect : PerkEffect
{
    public float multiplier;

    public override void Execute(Player player)
    {
        Manager.instance.timeMult *= multiplier;
    }
}