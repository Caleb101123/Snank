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
        Manager.instance.multiplier += multiplier;
    }
}

[System.Serializable]
public class TimeMultiplierEffect : PerkEffect
{
    public int multiplier;
    public float duration;

    public override void Execute(Player player)
    {
        Manager.instance.multiplier += multiplier;
    }
}