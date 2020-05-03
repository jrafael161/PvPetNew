﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GlobalControl : MonoBehaviour
{    
    public static GlobalControl _instance;
    public PlayerData playeProfile = new PlayerData();
    public PlayerData oponentProfile = new PlayerData();
    public ItemsDBmanager itemDataBase = new ItemsDBmanager();
    public DataBaseManager dataBaseManager = new DataBaseManager();
    public AbilitiesDBmanager abilitiesDataBase = new AbilitiesDBmanager();
    public PetDBManager petDBManager = new PetDBManager();
    AbilitiesHandler abilitiesHandler = new AbilitiesHandler();
    BattleController battleController = new BattleController();

    public Scene ActiveScene;
    int NumberOfStats;
    int NumberOfCoins;

    public int hp=0,xp=0,Lv=0,Str=0,Spd=0,Agl=0,Arm=0,PvpC=0,PetC=0,PremC=0;

    public static GlobalControl Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        NumberOfStats = 2;//3 por que se cuenta el 0
        NumberOfCoins = 2;
        //dataBaseManager.Initialize();
        itemDataBase.Set_ItemDatabase();
        itemDataBase.Initialize();
        abilitiesDataBase.Set_AbilitiesDatabase();
        abilitiesHandler.Initialize();
        petDBManager.Set_PetDatabase();
        petDBManager.Initialize();
        battleController.Initialize();
        //Si el dispositivo tiene conexion a internet, jala datos de firebase de lo contrario del save local.
        //SetPlayerData();
        LoadPlayerData();                
        ActiveScene = SceneManager.GetActiveScene();        
    }

    private void Update()
    {
        if (ActiveScene != SceneManager.GetActiveScene())//Si la escena cambio
            LoadPlayerData();
    }

    private void Awake()//Funcion que mantiene el GlobalControl entre escenas
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
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
        }
            
    }

    public void InitializePlayerData(PlayerData player = null)//Se muestra la informacion del jugador segun lo requerido
    {
        if (player == null)
        {
            player = playeProfile;
        }
        bool leveled=false;
        Slider sli;       
        Text hp_Text;
        GameObject hpStat,currency;
        GameObject [] stats = new GameObject[3];        
        Text[] PlayerCoins = new Text[3];
        stats[0] = GameObject.Find("StrengthStat");
        stats[1] = GameObject.Find("AgilityStat");
        stats[2] = GameObject.Find("SpeedStat");
        hpStat = GameObject.Find("HPStat");
        currency = GameObject.Find("Currency");
        PlayerCoins = currency.GetComponentsInChildren<Text>();
        hp_Text = hpStat.GetComponentInChildren<Text>();
        hp_Text.text = "HP:" + player.HP.ToString();
        for (int i = 0; i <= NumberOfStats; i++)
        {
            sli = stats[i].GetComponentInChildren<Slider>();            
            switch (i)
            {
                case 0:
                    sli.value = player.Strength;
                break;
                case 1:
                    sli.value = player.Agility;                    
                    break;
                case 2:
                    sli.value = player.Speed;                    
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i <= NumberOfCoins; i++)
        {
            switch (i)
            {
                case 0:
                    PlayerCoins[i].text = player.PetCoin.ToString();
                    break;
                case 1:
                    PlayerCoins[i].text = player.PvPCoin.ToString();
                    break;
                case 2:
                    PlayerCoins[i].text = player.PremiumCoin.ToString();
                    break;
                default:
                    break;
            }
        }
        if (player.LevelUpPoints >= 1)
        {
            foreach (GameObject stat in stats)
            {
                Button[] levelControlers = stat.GetComponentsInChildren<Button>(true);
                foreach (Button but in levelControlers)
                {
                    but.gameObject.SetActive(true);
                }
            }
            leveled = true;
        }
        else if(player == null && player.LevelUpPoints == 0)
        {
            if (leveled)
            {
                foreach (GameObject stat in stats)
                {
                    Button[] levelControlers = stat.GetComponentsInChildren<Button>(true);
                    foreach (Button but in levelControlers)
                    {
                        but.gameObject.SetActive(false);
                    }
                }
                leveled = false;
            }                
        }
    }

    public void SetPlayerData()//Se tendran que obtener estos datos de preferencia del save en la nube de lo contrario del save local del dispositivo del jugador
    {
        /*
        playeProfile.BattleTag = "Raizen8";
        playeProfile.Level = 1;
        playeProfile.HP = 100;
        playeProfile.XP = 1;
        playeProfile.Strength = 20;
        playeProfile.Speed = 30;
        playeProfile.Agility = 25;
        playeProfile.Armor = 5;
        playeProfile.critic_prob = 0.1f;
        playeProfile.PvPCoin = 50;
        playeProfile.PetCoin = 25;
        playeProfile.PremiumCoin = 1;
        PrepareItems();        
        */
        //GetPlayerData();//Sustituir por la query de firebase
    }

    public void SetOponentData()
    {
        oponentProfile = null;
    }

    public void PrepareItems()//Inicializar los items que el jugador tiene equipados
    {
        playeProfile.EquipedGear = new List<Item>(4);
        playeProfile.EquipedItems = new List<Item>();
        playeProfile.Inventory = new List<Item>();//Checar el inventario
        playeProfile.CompanionPet = new Pet();
        playeProfile.OwnedPets = new List<Pet>();
    }

    public bool GetPlayerData()
    {
        string json = null;
        try
        {
            json = File.ReadAllText(Application.dataPath + "/playerProfile.json");
        }
        catch (Exception)
        {
            throw;
        }
        if (json == null)
        {
            return false;
        }
        else
        {
            playeProfile = JsonUtility.FromJson<PlayerData>(json);//Setea los dato obtenidos
            if (playeProfile.EquipedGear == null)
            {
                playeProfile.EquipedGear = new List<Item>();
                foreach (int itemID in playeProfile.EquipedGearIDs)
                {
                    playeProfile.EquipedGear.Add(itemDataBase.ItemDB.Find(x => x.ItemID == itemID));
                }
            }
            if (playeProfile.EquipedItems == null)
            {
                playeProfile.EquipedItems = new List<Item>();
                foreach (int itemID in playeProfile.EquipedItemsIDs)
                {
                    playeProfile.EquipedItems.Add(itemDataBase.ItemDB.Find(x => x.ItemID == itemID));
                }
            }
            if (playeProfile.Inventory == null)
            {
                playeProfile.Inventory = new List<Item>();
                foreach (int itemID in playeProfile.InventoryItemsIDs)
                {                    
                    playeProfile.Inventory.Add(itemDataBase.ItemDB.Find(x => x.ItemID == itemID));
                }
            }
            if (playeProfile.OwnedPets == null)
            {
                playeProfile.OwnedPets = new List<Pet>();
                for (int i = 0; i < playeProfile.OwnedPetsIDs.Count; i++)
                {
                    Pet auxPet;
                    string jsonPet = File.ReadAllText(Application.dataPath + "/pet_" + i + ".json");
                    auxPet = JsonUtility.FromJson<Pet>(jsonPet);
                    if (i == playeProfile.CompaninoPetSlot)
                    {
                        playeProfile.CompanionPet = auxPet;
                    }
                    playeProfile.OwnedPets.Add(auxPet);
                }
            }
            return true;
        }
    }

    public void SavePlayerData()
    {
        string jsonstr = JsonUtility.ToJson(playeProfile);//Convierte los datos del jugador en un JASON
        File.WriteAllText(Application.dataPath + "/playerProfile.json", jsonstr);//Guarda los datos del jugador en un archivo JASON
    }   

    public void SavePetsData()
    {
        for (int i = 0; i < playeProfile.OwnedPets.Count; i++)
        {
            string jsonstr = JsonUtility.ToJson(playeProfile.OwnedPets[i]);
            File.WriteAllText(Application.dataPath + "/pet_" + i + ".json", jsonstr);
        }        
    }

    public PlayerData GetPlayer(bool whichPlayer)//1 -> Player, 0 -> Oponent
    {
        if (whichPlayer)
        {
            return playeProfile;
        }
        else if(!whichPlayer)
        {
            return oponentProfile;
        }
        else
        {
            Debug.Log("Cual jugador?");
            return playeProfile;
        }
    }

    public void CopyPlayer(PlayerData player)
    {
        player.PlayerID = playeProfile.PlayerID;
        player.BattleTag = playeProfile.BattleTag;
        player.Level = playeProfile.Level;
        player.LevelUpPoints = playeProfile.LevelUpPoints;
        player.HP = playeProfile.HP;
        player.XP = playeProfile.XP;
        player.Strength = playeProfile.Strength;
        player.Speed = playeProfile.Speed;
        player.Agility = playeProfile.Agility;
        player.Armor = playeProfile.Armor;
        player.critic_prob = playeProfile.critic_prob;
        player.EquipedGear = playeProfile.EquipedGear;
        player.EquipedItems = playeProfile.EquipedItems;
        player.CompanionPet = playeProfile.CompanionPet;
    }

    public void CheckIfLevelUP()
    {
        while (playeProfile.XP >= 100)
        {
            playeProfile.LevelUpPoints += 1;
            playeProfile.XP = playeProfile.XP - 100;
        }
        SavePlayerData();
    }
}
