using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class PvPControl : MonoBehaviour
{
    private bool active;
    public List<PlayerData> Oponents;
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