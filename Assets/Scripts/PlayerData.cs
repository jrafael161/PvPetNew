using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerData
{    
    public Sprite PlayerSprite;
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
    public string timeUntilMissionCooldown;
    public Pet CompanionPet;
    public List<Item> EquipedGear;
    public List<Item> EquipedItems;
    public List<Pet> OwnedPets;
    public List<Item> Inventory;
}
