using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyZone {Head, Chest, Arms, Foots, Weapon, Equipable};
public enum ItemType { Armor, Weapon };
public enum AbiltyType {Passive, Active};

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]

public class Item: ScriptableObject
{
    public string Name;
    public int ItemID;
    public Sprite icon = null;
    public int PvP_Price;
    public int Pet_Price;
    public int Prem_Price;
    public string Description;
    public int LevelRequirement;
    public int Value;//Modficador de armadura o de daño dependiendo sea el caso
    public int Bonus_amount;
    public string Set;
    public BodyZone Bz;
    public ItemType It;
    public bool has_ability;
    public AbiltyType At;
    public Ability Abilitys;
}
