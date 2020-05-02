﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerData
{    
    public Sprite PlayerSprite;//
    public string PlayerID;
    public string BattleTag;
    public float Level;
    public int LevelUpPoints;
    public float HP;
    public float XP;
    public float Strength;
    public float Speed;
    public float Agility;
    public float Armor;
    public float critic_prob;
    public float PvPCoin;
    public float PetCoin;
    public float PremiumCoin;
    public int AvailableMissions;
    public Time timeUntilMissionCooldown;
    public int CompaninoPetSlot;
    public Pet CompanionPet;
    public List<int> EquipedGearIDs;
    public List<Item> EquipedGear;
    public List<int> EquipedItemsIDs;
    public List<Item> EquipedItems;
    public List<int> OwnedPetsIDs;
    public Dictionary<string, int> PetStats;
    public List<Pet> OwnedPets;
    public List<int> InventoryItemsIDs;
    public List<Item> Inventory;   
}
