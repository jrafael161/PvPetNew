using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum PetTier { Normal, PocoComun , Raro, Legendario, Epico };

[CreateAssetMenu(fileName = "New Pet", menuName = "Inventory/Pet")]

public class Pet : ScriptableObject
{
    public int Clase;
    public int Mision;
    public Image PetSprite;
    public int PetID;
    public string PetName;
    public float ProbFacil;
    public float ProbNormal;
    public float ProbDificil;
    public float ProbExtremo;
    public float ProbPurgatorio;
    public float ProbAgonia;
    public float ProbTormento;
    public float ProbPetHunter;
    public PetTier Pt;
    public int Level;
    public int Strength;
    public int Speed;
    public int Agility;
    public int Armor;
    public float critic_prob;
    public Item PetItem;
}
