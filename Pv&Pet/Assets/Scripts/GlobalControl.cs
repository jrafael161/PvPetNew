using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public PlayerData playeProfile = new PlayerData();
    public ItemsDBmanager itemDataBase = new ItemsDBmanager();
    public Scene ActiveScene;

    public int hp=0,xp=0,Lv=0,Str=0,Spd=0,Agl=0,Arm=0,PvpC=0,PetC=0,PremC=0;

    private void Start()
    {
        itemDataBase.Set_ItemDatabase();
        SetPlayerData();
        SavePlayerData();
        GetPlayerData();//Asigna los valores del jason a la variable playerProfile
        LoadPlayerData();        
        ActiveScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (ActiveScene != SceneManager.GetActiveScene())//Si la escena cambio
            LoadPlayerData();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadPlayerData()//Se asegura que nos econtremos en una escena valida en la que se pueda cargar la informacion del jugador
    {
        ActiveScene = SceneManager.GetActiveScene();
        if (ActiveScene.name == "PvPScreen")
        {
            InitializePlayerData();
        }            
        else if (ActiveScene.name == "ShopScreen")
        {
            InitializePlayerData();
            SetShopItems();
        }
            
    }

    public void InitializePlayerData()//Se muestra la informacion del jugador segun lo requerido
    {
        Slider sli;
        Text hp_Text;
        GameObject hpStat;
        GameObject [] stats = new GameObject[3];
        stats[0] = GameObject.Find("StrengthStat");
        stats[1] = GameObject.Find("AgilityStat");
        stats[2] = GameObject.Find("SpeedStat");
        hpStat = GameObject.Find("HPStat");
        hp_Text = hpStat.GetComponentInChildren<Text>();
        hp_Text.text = "HP:" + playeProfile.HP.ToString();
        for (int i = 0; i <= 2; i++)
        {
            sli = stats[i].GetComponentInChildren<Slider>();            
            switch (i)
            {
                case 0:
                    sli.value = playeProfile.Strength;
                break;
                case 1:
                    sli.value = playeProfile.Agility;                    
                    break;
                case 2:
                    sli.value = playeProfile.Speed;                    
                    break;
                default:
                    break;
            }
        }
    }

    public void SetPlayerData()
    {
        /*
        playeProfile.HP = hp;
        playeProfile.XP = xp;
        playeProfile.Level = Lv;
        playeProfile.Strength = Str;
        playeProfile.Speed = Spd;
        playeProfile.Agility = Agl;
        playeProfile.Armor = Arm;
        playeProfile.PvPCoin = PvpC;
        playeProfile.PetCoin = PetC;
        PrepareItems();
        playeProfile.PremiumCoin = PremC;
        Debug.Log("fuerza: " + playeProfile.Strength);
        */
        playeProfile.HP = 100;
        playeProfile.XP = 1;
        playeProfile.Level = 1;
        playeProfile.Strength = 20;
        playeProfile.Speed = 30;
        playeProfile.Agility = 25;
        playeProfile.Armor = 0;
        playeProfile.PvPCoin = PvpC;
        playeProfile.PetCoin = PetC;
        playeProfile.PremiumCoin = PremC;
        PrepareItems();        
        Debug.Log("fuerza: " + playeProfile.Strength);
    }

    public void PrepareItems()
    {
        //Inicializar los items que el jugador tiene equipados
    }

    private void GetPlayerData()
    {             
        string json = File.ReadAllText(Application.dataPath + "/playerProfile.json");//Obtiene los datos del jugador desde un JASON
        playeProfile = JsonUtility.FromJson<PlayerData>(json);//Setea los dato obtenidos
    }

    private void SavePlayerData()
    {
        string jsonstr = JsonUtility.ToJson(playeProfile);//Convierte los datos del jugador en un JASON
        File.WriteAllText(Application.dataPath + "/playerProfile.json", jsonstr);//Guarda los datos del jugador en un archivo JASON
    }

    public void SetShopItems()
    {
        GameObject ShopItem, ShopItemAux;
        Text[] Texto;
        ShopItem = GameObject.Find("ShopItem");        
        foreach  (Item item in itemDataBase.ItemDB)
        {
            ShopItemAux = Instantiate(ShopItem) as GameObject;
            ShopItemAux.SetActive(true);
            ShopItemAux.transform.SetParent(ShopItem.transform.parent, false);
            Texto = ShopItemAux.GetComponentsInChildren<Text>();
            Texto[0].text = item.Description.ToString();
            Texto[1].text = item.PvP_Price.ToString();
            Texto[2].text = item.Pet_Price.ToString();
            Texto[3].text = item.Prem_Price.ToString();
        }        
    }
}
