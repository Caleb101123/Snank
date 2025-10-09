using UnityEngine;

[System.Serializable]
public class PerkEffect
{
    public PerkEffect()
    {

    }

    public virtual void Execute(Player player)
    {
        Debug.Log("Null Effect Called");
    }
}

[System.Serializable]
public class SteeringPerkEffect : PerkEffect
{
    public int change;

    public SteeringPerkEffect(int val) : base()
    {
        change = val;
    }

    public override void Execute(Player player)
    {
        player.turnRate += change;
    }
}

[System.Serializable]
public class SpeedPerkEffect : PerkEffect
{
    public int change;

    public SpeedPerkEffect(int val) : base()
    {
        change = val;
    }

    public override void Execute(Player player)
    {
        player.speed += change;
    }
}



[System.Serializable]
public class PointMultiplierEffect : PerkEffect
{
    public int multiplier;

    public PointMultiplierEffect(int val) : base()
    {
        multiplier = val;
    }

    public override void Execute(Player player)
    {
        Manager.instance.multiplier += multiplier;
    }
}