using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{    
    private static MainScene _instance;

    public InputField Battletag;

    public GameObject Panel;
    public GameObject Panel_character;
    public GameObject Panel_pet;

    public Sprite spritepet1;
    public Sprite spritepet2;
    public Sprite spritepet3;
    public Sprite spritepet4;

    public GameObject img1; //para crear user
    public GameObject img2;
    public GameObject img3;
    public GameObject img4;

    public GameObject imgpet1;//para crear pet de user
    public GameObject imgpet2;
    public GameObject imgpet3;
    public GameObject imgpet4;

    static bool internetConnection;
    CheckInternetConnection chkInt = new CheckInternetConnection();

    public static MainScene Instance
    {
        get { return _instance; }
    }

    public void Initialize()
    {
        _instance = this;
    }

    private void Start()
    {    
        Text textusername = GameObject.Find("Canvas/Lbl_Username").GetComponent<Text>();
        GlobalControl.Instance.GetPlayerData();
        Image profilepic = GameObject.Find("ProfileSprite").GetComponent<Image>();
        textusername.text = GlobalControl.Instance.playeProfile.BattleTag;

        profilepic.gameObject.SetActive(true);

        if (GlobalControl.Instance.playeProfile.PlayerSpriteName == "Profile_1")
            profilepic.GetComponent<Image>().sprite = DataBaseManager.Instance.sprite1;
        if (GlobalControl.Instance.playeProfile.PlayerSpriteName  == "Profile_2")
            profilepic.GetComponent<Image>().sprite = DataBaseManager.Instance.sprite2;
        if (GlobalControl.Instance.playeProfile.PlayerSpriteName == "Profile_3")
            profilepic.GetComponent<Image>().sprite = DataBaseManager.Instance.sprite3;
        if (GlobalControl.Instance.playeProfile.PlayerSpriteName == "Profile_4")
            profilepic.GetComponent<Image>().sprite = DataBaseManager.Instance.sprite4;
    }

    public void OpenPanel()
    {
        bool isActive = Panel.activeSelf;
        Panel.SetActive(true);
    }
    public void Opencharacter()
    {
        Panel_character.SetActive(true);
    }
    public void Closecharacter()
    {
        Panel_character.SetActive(false);
    }
    public void Openpet()
    {
        Panel_pet.SetActive(true);
    }

    public void Closepet()
    {
        Panel_pet.SetActive(false);
    }
    public void Cargadatos_user(string hp, string str, string agy, string spe, string arm)
    {
        Text textHP = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_pv_v").GetComponent<Text>();
        Text textStrength = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_str_v").GetComponent<Text>();
        Text textAgility = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_agy_v").GetComponent<Text>();
        Text textSpeed = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_spe_v").GetComponent<Text>();
        Text textArmorv = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_arm_v").GetComponent<Text>();
        textHP.text = hp;
        textStrength.text = str;
        textAgility.text = agy;
        textSpeed.text = spe;
        textArmorv.text = arm;
    }
    public void Cargadatos_pet(string hp, string str, string agy, string spe, string arm)
    {
        Text textHP = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_pv_v_pet").GetComponent<Text>();
        Text textStrength = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_str_v_pet").GetComponent<Text>();
        Text textAgility = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_agy_v_pet").GetComponent<Text>();
        Text textSpeed = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_spe_v_pet").GetComponent<Text>();
        Text textArmorv = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_arm_v_pet").GetComponent<Text>();
        textHP.text = hp;
        textStrength.text = str;
        textAgility.text = agy;
        textSpeed.text = spe;
        textArmorv.text = arm;
    }
    public void Checkprofileimg1()
    {
        Cargadatos_user("10", "5", "1", "1", "0");
        img1.SetActive(true);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(false);
    }
    public void Checkprofileimg2()
    {
        Cargadatos_user("7", "4", "2", "2", "0");
        img1.SetActive(false);
        img2.SetActive(true);
        img3.SetActive(false);
        img4.SetActive(false);
    }
    public void Checkprofileimg3()
    {
        Cargadatos_user("15", "3", "1", "1", "0");
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(true);
        img4.SetActive(false);
    }
    public void Checkprofileimg4()
    {
        Cargadatos_user("10", "3", "1", "4", "0");
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(true);
    }
    public void Checkprofileimg1_pet()
    {
        Cargadatos_pet("5", "7", "2", "2", "1");
        img1.SetActive(true);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(false);
    }
    public void Checkprofileimg2_pet()
    {
        Cargadatos_pet("10", "3", "4", "3", "1");
        img1.SetActive(false);
        img2.SetActive(true);
        img3.SetActive(false);
        img4.SetActive(false);
    }
    public void Checkprofileimg3_pet()
    {
        Cargadatos_pet("10", "4", "3", "3", "1");
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(true);
        img4.SetActive(false);
    }
    public void Checkprofileimg4_pet()
    {
        Cargadatos_pet("15", "5", "1", "1", "1");
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(true);
    }
    public void Btn_go()
    {
        string Userid;
        string Battletaguser = Battletag.text;
        internetConnection = chkInt.Check();
        if (internetConnection)
        {

            Userid = GameController.userid;
            DataBaseManager.Instance.writeNewUser(Userid, Battletaguser);
            DataBaseManager.Instance.Checkforbattletag(Userid);
        }
        else
        {
            DataBaseManager.Instance.writeNewUserOffline();
        }
        Panel.SetActive(false);
        Panel_character.SetActive(false);
        Panel_pet.SetActive(false);
    }
    public void SignOut()
    {
        SceneManager.LoadScene("00-Login");
    }



}
