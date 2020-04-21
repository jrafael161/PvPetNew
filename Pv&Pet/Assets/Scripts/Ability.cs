using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { Damage, Healing, Buff, Debuff };
public enum ThresholdType {Turns,Health};
public enum BuffType {Health, Strength, Speed, Agility, Critic, Armor,NA};
public enum Effect {Oneself,Opponent};

[CreateAssetMenu(fileName = "New Ability", menuName = "Inventory/Abilities")]

public class Ability : ScriptableObject
{
    public string Name;
    public int AbilityID;
    public int Bonus;    
    public int minThreshold;
    public int maxThreshold;  
    public ThresholdType Tt;
    public EffectType Et;
    public BuffType Bt;
    public Effect Eff;
}
