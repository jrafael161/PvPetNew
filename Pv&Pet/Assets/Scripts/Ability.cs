using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { Damage, Healing, Buff, Debuff };
public enum ThresholdType {Turns,Health};
public enum BuffType {Health, Strength, Speed, Agility, Critic, Armor}
public enum Effect {Oneself,Opponent};

[CreateAssetMenu(fileName = "New Ability", menuName = "Inventory/Abilities")]

public class Ability : ScriptableObject
{
    public string Name;
    public int AbilityID;
    public Sprite icon = null;
    public int PvP_Price;
    public int Pet_Price;
    public int Prem_Price;
    public string Description;
    public int LevelRequirement;
    public int cuantity;    
    public int minThreshold;
    public int maxThreshold;
    public BodyZone Bz;
    public ItemType It;
    public ThresholdType Tt;
    public EffectType Et;
    public BuffType Bt;
    public Effect Eff;
}
