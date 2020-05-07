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
    public int PvPCoin;
    public int PetCoin;
    public int PremiumCoin;
    public int AvailableMissions;
    public string timeUntilMissionCooldown;
    public string LocalSaveTimeStamp;
    public string DateOfUserRegistration;
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

    public List<Item> PlayerActiveAbilities;//Para el battle controlle, no se necesita almacenar
    public PlayerData playerPetasPlayer;//Para el battle controlle, no se necesita almacenar
    public int Wins;
    public int Loss;

}
