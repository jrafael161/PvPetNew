using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{    
    private static MainScene _instance;

    public static MainScene Instance
    {
        get { return _instance; }
    }

    void Start()
    {
        _instance = this;
    }

    public void InitializeScene()
    {
        Text textusername = GameObject.Find("Canvas/bg_main/Lbl_Username").GetComponent<Text>();
        GlobalControl.Instance.GetPlayerData();
        Image profilepic = GameObject.Find("ProfileSprite").GetComponent<Image>();
        textusername.text = GlobalControl.Instance.playeProfile.BattleTag;

        profilepic.gameObject.SetActive(true);

        if (GlobalControl.Instance.playeProfile.PlayerSprite.name == "Profile_1")
            profilepic.GetComponent<Image>().sprite = DataBaseManager.Instance.sprite1;
        if (GlobalControl.Instance.playeProfile.PlayerSprite.name == "Profile_2")
            profilepic.GetComponent<Image>().sprite = DataBaseManager.Instance.sprite2;
        if (GlobalControl.Instance.playeProfile.PlayerSprite.name == "Profile_3")
            profilepic.GetComponent<Image>().sprite = DataBaseManager.Instance.sprite3;
        if (GlobalControl.Instance.playeProfile.PlayerSprite.name == "Profile_4")
            profilepic.GetComponent<Image>().sprite = DataBaseManager.Instance.sprite4;
    }
}
