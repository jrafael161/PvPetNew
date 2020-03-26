using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyZone {Head, Chest, Arms, Legs, Foots, Weapon, Equipable};
public enum ItemType {Armor, Weapon, Pasive, Active};
public enum BonusType {Health, Strength, Speed, Agility, Armor, NA};

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]

public class Item: ScriptableObject
{
    public string Name;
    public Sprite icon = null;
    public int PvP_Price;
    public int Pet_Price;
    public int Prem_Price;
    public string Description;
    public int damage;
    public int bonus;
    public string Set;
    public BodyZone Bz;
    public ItemType It;
    public BonusType Bt;
}
