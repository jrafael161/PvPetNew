using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPet", menuName = "Pets")]

public class Pet: ScriptableObject
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
    public BonusType Bt;
    public int Level;
    public int Health;
    public int Hunger;
    public string Mood;
    public int Energy;
    public int Speed;
    public float Intelligence;
    public float Timer;
    public bool Hatch;
}
