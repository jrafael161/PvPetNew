using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerData
{    
    public Sprite PlayerSprite;
    public string PlayerID;
    public string BattleTag;
    public int Level;
    public int HP;
    public int XP;
    public int Strength;
    public int Speed;
    public int Agility;
    public int Armor;
    public float critic_prob;
    public int PvPCoin;
    public int PetCoin;
    public int PremiumCoin;
    public List<Item> EquipedGear;
    public List<Item> EquipedItems;
    public List<Pet> OwnedPets;
    public List<Item> Inventory;
}
