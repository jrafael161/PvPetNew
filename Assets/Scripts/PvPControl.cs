using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PvPControl : MonoBehaviour
{
    private bool active;
    public static List<PlayerData> Oponents;
    private string SelectedOponentBt;

    private static PvPControl _instance;

    public static PvPControl Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }


    void Start()
    {       
        Oponents = new List<PlayerData>();
                
        GetOponents();
        //SetOponents();
    }

    public void GetOponents()
    {
        DataBaseManager.Instance.GetOponentList(GlobalControl.Instance.playeProfile.Level.ToString());
    }

    public void GetOponentBattleTag(Text bt)
    {
        SelectedOponentBt = bt.text;
    }

    public void ShowOponents()
    {
        Oponents.Clear();
        DataBaseManager.Instance.AssignOponents();
        GameObject OponentProfile, OponentProfileAux;
        Text[] Texto;
        Image profileSprite;
        OponentProfile = GameObject.Find("OponentProfile");
        foreach (PlayerData Oponent in Oponents)
        {
            OponentProfileAux = Instantiate(OponentProfile) as GameObject;
            OponentProfileAux.SetActive(true);
            OponentProfileAux.transform.SetParent(OponentProfile.transform.parent, false);
            profileSprite = OponentProfileAux.GetComponentInChildren<Image>();
            profileSprite.sprite = Oponent.PlayerSprite;
            Texto = OponentProfileAux.GetComponentsInChildren<Text>();
            Texto[0].text = Oponent.BattleTag.ToString();
            Texto[1].text = Oponent.Level.ToString();
            Texto[2].text = Oponent.HP.ToString();
            Texto[3].text = Oponent.Strength.ToString();
            Texto[4].text = Oponent.Speed.ToString();
            Texto[5].text = Oponent.Agility.ToString();
        }
        Destroy(OponentProfile);
    }

    public void TransitionToBattle()
    {
        GlobalControl.Instance.oponentProfile = Oponents.Find(x => x.BattleTag==SelectedOponentBt);
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