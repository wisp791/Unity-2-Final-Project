using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpOptionStat : LevelUpOptionsBase
{
    StatType statType;
    float amount;

    public LevelUpOptionStat(int weight, StatType statType, float amount)
    {
        this.weight = weight;
        this.statType = statType;
        this.amount = amount;
    }

    public override void Execute()
    {
        PlayerStats playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        playerStats.AddStat(statType, amount);
        Debug.Log("Added " + GetDescription());
    }

    public override string GetDescription()
    {
        return statType.ToString() + " UP +" + amount;
    }
}
