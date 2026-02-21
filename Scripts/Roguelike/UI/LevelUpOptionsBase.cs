using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OptionType
{
    Stats,
    Skills,
    Weapons,
    Items,
    Consumables
}

public struct OptionDisplayProperties
{
    public string name;
    public string description;
    public OptionType type;
    public Sprite typeIcon;
    public Color color;
}

public abstract class LevelUpOptionsBase
{
    public int weight;
    public abstract void Execute();
    public abstract string GetDescription();
}

