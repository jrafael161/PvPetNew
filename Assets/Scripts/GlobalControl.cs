using System.IO;
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
    public AbilitiesDBmanager abilitiesDataBase = new AbilitiesDBmanager();
    public PetDBManager petDBManager = new PetDBManager();
    public AbilitiesHandler abilitiesHandler = new AbilitiesHandler();
    //public BattleController battleController;

    public int hp=0,xp=0,Lv=0,Str=0,Spd=0,Agl=0,Arm=0,PvpC=0,PetC=0,PremC=0;

    public static GlobalControl Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Debug.Log("Si, estamos en android");
        Debug.Log(Application.persistentDataPath);
        //if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        //Permission.RequestUserPermission(Permission.ExternalStorageWrite);
#endif
        //dataBaseManager.Initialize();
        itemDataBase.Set_ItemDatabase();
        Debug.Log("Paso de setear la bd de items");
        itemDataBase.Initialize();
        abilitiesDataBase.Set_AbilitiesDatabase();
        Debug.Log("Paso de setear la bd de habilidades");
        abilitiesHandler.Initialize();
        petDBManager.Set_PetDatabase();
        Debug.Log("Paso de setear la bd de mascotas");
        petDBManager.Initialize();
        //battleController = gameObject.AddComponent(typeof(BattleController)) as BattleController;
        //battleController.Initialize();
        //Si el dispositivo tiene conexion a internet, jala datos de firebase de lo contrario del save local.
        //SetPlayerData();
        //LoadPlayerData();                
        //ActiveScene = SceneManager.GetActiveScene();        
        //GetPlayerData();
    }

    /*
    private void Update()
    {
        if (ActiveScene != SceneManager.GetActiveScene())//Si la escena cambio, sacar esto del update y refactorizar el codigo para que el PlayerResume sea un objeto que tenga atacheado un script que manda a llamar el loadplayerdata
            LoadPlayerData();
    }*/

    private void Awake()//Funcion que mantiene el GlobalControl entre escenas
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
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
        playeProfile.EquipedGearIDs = new List<int>();
        playeProfile.EquipedItems = new List<Item>();
        playeProfile.EquipedItemsIDs = new List<int>();
        playeProfile.Inventory = new List<Item>();//Checar el inventario
        playeProfile.InventoryItemsIDs = new List<int>();
        playeProfile.CompanionPet = ScriptableObject.CreateInstance("Pet") as Pet;
        playeProfile.OwnedPets = new List<Pet>();
        playeProfile.OwnedPetsIDs = new List<int>();
    }

    public bool GetPlayerData()
    {
        string json = null;
        try
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (File.Exists(Application.persistentDataPath + "/playerProfile.json"))
        {
            json = File.ReadAllText(Application.dataPath + "/playerProfile.json");
        }        
#endif
#if UNITY_EDITOR           
            json = File.ReadAllText(Application.dataPath + "/playerProfile.json");
#endif

        }
        catch (Exception e)
        {
            print(e);
        }

        if (json == null)
        {
            return false;
        }
        else
        {
            playeProfile = JsonUtility.FromJson<PlayerData>(json);//Setea los dato obtenidos
            playeProfile.EquipedGear = new List<Item>();
            foreach (int itemID in playeProfile.EquipedGearIDs)
            {
                playeProfile.EquipedGear.Add(itemDataBase.ItemDB.Find(x => x.ItemID == itemID));
            }

            playeProfile.EquipedItems = new List<Item>();
            foreach (int itemID in playeProfile.EquipedItemsIDs)
            {
                playeProfile.EquipedItems.Add(itemDataBase.ItemDB.Find(x => x.ItemID == itemID));
            }

            playeProfile.Inventory = new List<Item>();
            foreach (int itemID in playeProfile.InventoryItemsIDs)
            {
                playeProfile.Inventory.Add(itemDataBase.ItemDB.Find(x => x.ItemID == itemID));
            }

            playeProfile.OwnedPets = new List<Pet>();
            for (int i = 0; i < playeProfile.OwnedPetsIDs.Count; i++)
            {
                PetForJson auxPet;
                string jsonPet = null;
#if UNITY_ANDROID && !UNITY_EDITOR
        if (File.Exists(Application.persistentDataPath + "/pet_" + i.ToString() + ".json"))
        {
            jsonPet = File.ReadAllText(Application.dataPath + "/pet_" + i.ToString() + ".json");
        }        
#endif
#if UNITY_EDITOR
                jsonPet = File.ReadAllText(Application.dataPath + "/pet_" + i.ToString() + ".json");
#endif
                auxPet = JsonUtility.FromJson<PetForJson>(jsonPet);
                Pet pet = ScriptableObject.CreateInstance("Pet") as Pet;
                
                pet.Clase = auxPet.Clase;
                pet.Mision = auxPet.Mision;
                pet.PetID = auxPet.PetID;
                pet.PetName = auxPet.PetName;
                pet.PetSprite = petDBManager.PetDB.Find(x => x.PetID == playeProfile.OwnedPetsIDs[i]).PetSprite;
                pet.Pt = auxPet.Pt;
                pet.HP = auxPet.HP;
                pet.Level = auxPet.Level;
                pet.XP = auxPet.XP;
                pet.Strength = auxPet.Strength;
                pet.Speed = auxPet.Speed;
                pet.Agility = auxPet.Agility;
                pet.Armor = auxPet.Armor;
                pet.critic_prob = auxPet.critic_prob;
                //falta el pet item
                if (i == playeProfile.CompanionPetSlot)
                {
                    playeProfile.CompanionPet = pet;
                }
                playeProfile.OwnedPets.Add(pet);
            }
            return true;
        }
    }

    public void SavePlayerData()
    {
        playeProfile.LocalSaveTimeStamp = DateTime.Now.ToString();
        Debug.Log(playeProfile.LocalSaveTimeStamp);
        string jsonstr = JsonUtility.ToJson(playeProfile);//Convierte los datos del jugador en un JASON                       
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!File.Exists(Application.persistentDataPath + "/playerProfile.json"))
        {
            File.WriteAllText(Application.persistentDataPath + "/playerProfile.json", jsonstr);
        }                
#endif
#if UNITY_EDITOR
        Debug.Log("Esta en el editor");
        File.WriteAllText(Application.dataPath + "/playerProfile.json", jsonstr);//Guarda los datos del jugador en un archivo JASON
#endif
    }

    public void SavePetsData()//Se tuvo que crear una con los mismos datos de pet por que las clases que heredan de serialazable object no pueden serializarse con json
    {
        for (int i = 0; i < playeProfile.OwnedPets.Count; i++)
        {
            PetForJson AuxPet = new PetForJson();
            AuxPet.Clase = playeProfile.OwnedPets[i].Clase;
            AuxPet.Mision = playeProfile.OwnedPets[i].Mision;
            AuxPet.PetSprite = playeProfile.OwnedPets[i].PetSprite;
            AuxPet.PetID = playeProfile.OwnedPets[i].PetID;
            AuxPet.PetName = playeProfile.OwnedPets[i].PetName;
            AuxPet.Pt = playeProfile.OwnedPets[i].Pt;
            AuxPet.HP = playeProfile.OwnedPets[i].HP;
            AuxPet.Level = playeProfile.OwnedPets[i].Level;
            AuxPet.XP = playeProfile.OwnedPets[i].XP;
            AuxPet.Strength = playeProfile.OwnedPets[i].Strength;
            AuxPet.Speed = playeProfile.OwnedPets[i].Speed;
            AuxPet.Agility = playeProfile.OwnedPets[i].Agility;
            AuxPet.Armor = playeProfile.OwnedPets[i].Armor;
            AuxPet.critic_prob = playeProfile.OwnedPets[i].critic_prob;
            /*
            if(playeProfile.OwnedPets[i].PetItem != null)
            {
                AuxPet.PetItemID = playeProfile.OwnedPets[i].PetItem.ItemID;
            }*/           
            string jsonstr = JsonUtility.ToJson(AuxPet);
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!File.Exists(Application.persistentDataPath + "/pet_" + i.ToString() + ".json"))
        {
            File.WriteAllText(Application.persistentDataPath + "/pet_" + i.ToString() + ".json", jsonstr);
        }
        
#endif
#if UNITY_EDITOR
            Debug.Log("Esta en el editor");
            File.WriteAllText(Application.dataPath + "/pet_" + i.ToString() + ".json", jsonstr);
#endif
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
        player.PlayerSprite = playeProfile.PlayerSprite;
        player.PlayerSpriteName = playeProfile.PlayerSpriteName;
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
        player.CompanionPetSlot = playeProfile.CompanionPetSlot;
        //Checar en PvP y PvE
        player.OwnedPets = playeProfile.OwnedPets;
        player.OwnedPetsIDs = playeProfile.OwnedPetsIDs;
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
