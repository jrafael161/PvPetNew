﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerData
{    
    public Image PlayerSprite;
    public string PlayerID;
    public string BattleTag;
    public int HP;    
    public int Level;
    public int XP;
    public int Strength;
    public int Speed;
    public int Agility;
    public int Armor;
    public int PvPCoin;
    public int PetCoin;
    public int PremiumCoin;
    public Item HeadGear;
    public Item ChestGear;
    public Item ArmsGear;
    public Item FootsGear;
    public Item Weapon;    
    public List<Item> EquipedItems;
    public List<Item> Inventory;
    public List<int> ItemInventoryID;
}