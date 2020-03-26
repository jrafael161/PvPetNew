using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class PvPControl : MonoBehaviour
{
    GlobalControl gc;
    private bool active;
    PlayerData Opo;
    private List<PlayerData> Oponents;
    

    void Start()
    {
        gc = GameObject.FindObjectOfType<GlobalControl>();
        Oponents = new List<PlayerData>();
        Opo = new PlayerData();        
        GetOponents();
        SetOponents();
    }

    public void SetOponents()//Pobla el contenedor de los oponentes con sus perfiles
    {
        GameObject OponentProfile, OponentProfileAux;
        Text[] Texto;
        Image profile;

        OponentProfile = GameObject.Find("OponentProfile");
        foreach (PlayerData Oponent in Oponents)
        {
            OponentProfileAux = Instantiate(OponentProfile) as GameObject;
            OponentProfileAux.SetActive(true);
            OponentProfileAux.transform.SetParent(OponentProfile.transform.parent, false);
            profile = OponentProfileAux.GetComponentInChildren<Image>();
            profile = Opo.PlayerSprite;
            Texto = OponentProfileAux.GetComponentsInChildren<Text>();            
            Texto[0].text = Opo.BattleTag.ToString();
            Texto[1].text = Opo.Level.ToString();
            Texto[2].text = Opo.HP.ToString();
            Texto[3].text = Opo.Strength.ToString();
            Texto[4].text = Opo.Speed.ToString();
            Texto[5].text = Opo.Agility.ToString();
        }
        Destroy(OponentProfile);
    }

    public void GetOponents()
    {
        //Obtener oponentes con la query de firebase
        for (int i = 0; i < 10; i++)
        {
            Random.InitState(i);
            Opo.BattleTag = "Pepe";
            Opo.HP = Random.Range(0,100);
            Opo.XP = 1;
            Opo.Level = 1;
            Opo.Strength = Random.Range(0, 30);
            Opo.Speed = Random.Range(0, 30);
            Opo.Agility = Random.Range(0, 30);
            Opo.Armor = 0;
            Opo.PvPCoin = 50;
            Opo.PetCoin = 25;
            Opo.PremiumCoin = 1;
            Oponents.Add(Opo);
        }        

    }

    public void TransitionToBattle()
    {
        gc.oponentProfile = Oponents.Find(x => x.BattleTag==Opo.BattleTag);
        SceneManager.LoadScene("CombatScreen");
    }

    public void ActivatePreview()
    {
        active = true;
    }

    public void DeactivatePreview(GameObject preview)
    {
        preview.SetActive(false);        
    }

}