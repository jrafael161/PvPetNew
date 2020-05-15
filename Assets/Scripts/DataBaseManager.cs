using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class DataBaseManager : MonoBehaviour
{
    private static DataBaseManager _instance;

    public static DataBaseManager Instance
    {
        get { return _instance; }
    }

    private InputField Battletag;

    public static Firebase.Auth.FirebaseAuth auth;
    public static Firebase.Auth.FirebaseUser user;

    private string displayName;
    private bool signedIn;
    private bool registered;
    public List<PlayerData> OponentList;
    static bool alredyFetchedFromDB = false;

    static bool firstTime = false;//Indica si se acaba de crear el usuario, por lo tanto no habia un json contra el que comparar datos

    public GameObject Panel;// panel para battletag

    public GameObject img1; //para crear user
    public GameObject img2;
    public GameObject img3;
    public GameObject img4;

    public GameObject imgpet1;//para crear pet de user
    public GameObject imgpet2;
    public GameObject imgpet3;
    public GameObject imgpet4;

    public Sprite sprite1;//para cargar imagen de personaje
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;

    public GameObject profilepic;

    [SerializeField]
    DatabaseReference reference;
    static bool internetConnection;
    CheckInternetConnection chkInt = new CheckInternetConnection();
    MainScene mainScene;

    void Start()
    {
        _instance = this;
        //this.gameObject.AddComponent<MainScene>();
        //mainScene = GetComponent<MainScene>();
        OponentList = new List<PlayerData>();
        internetConnection = chkInt.Check();
        if (internetConnection)
        {
            DB();
            //Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
            //textuserid.text = GameController.userid;
            if (!alredyFetchedFromDB)
            {
                Checkforbattletag(GameController.userid);//Sacar del start para que no se haga cada vez que se ingresa al main
            }
            else
            {
                //Cargar la main scene pero con datos del playerprofile
            }
        }
        else
        {
            if (GlobalControl.Instance.GetPlayerData())
            {
                //MainScene.Instance.InitializeScene();
                //Cargar la main scene pero con datos del playerprofile
            }
            else
            {
                OpenPanel();
            }
        }
    }

    private void Awake()
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

    void DB()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pvpet-f0b05.firebaseio.com/Players/Pa5UU16uCzt6X1E1DJ6a");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        _instance = this;
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Checkforbattletag(GameController.userid);
    }

    public void Checkforbattletag(string Userid)
    {
        Firebase.Database.FirebaseDatabase dbInstance = Firebase.Database.FirebaseDatabase.DefaultInstance;
        dbInstance.GetReference("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    if (user.Key == Userid)
                    {
                        IDictionary dictUser = (IDictionary)user.Value;
                        if (!firstTime)
                        {
                            if (false)//DateTime.Parse(dictUser["CloudSaveTimeStamp"].ToString()) < DateTime.Parse(GlobalControl.Instance.playeProfile.LocalSaveTimeStamp) no se esta guardando el time stamp en la nube
                            {
                                string usernameAux = GlobalControl.Instance.playeProfile.BattleTag;
                                string profilepicAux = GlobalControl.Instance.playeProfile.PlayerSprite.name;
                                string HPAux = GlobalControl.Instance.playeProfile.HP.ToString();
                                string LevelAux = GlobalControl.Instance.playeProfile.Level.ToString();
                                string LevelUpPointsAux = GlobalControl.Instance.playeProfile.Level.ToString();
                                string XPAux = GlobalControl.Instance.playeProfile.XP.ToString();
                                string StrengthAux = GlobalControl.Instance.playeProfile.Strength.ToString();
                                string SpeedAux = GlobalControl.Instance.playeProfile.Speed.ToString();
                                string AgilityAux = GlobalControl.Instance.playeProfile.Agility.ToString();
                                string ArmorvAux = GlobalControl.Instance.playeProfile.Armor.ToString();
                                string PvPCoinAux = GlobalControl.Instance.playeProfile.PvPCoin.ToString();
                                string PetCoinAux = GlobalControl.Instance.playeProfile.PetCoin.ToString();
                                string PremiumCoinAux = GlobalControl.Instance.playeProfile.PremiumCoin.ToString();
                                string CompanionPetAux = "Pet_" + GlobalControl.Instance.playeProfile.CompanionPetSlot.ToString();
                                string AvailableMissionsAux = GlobalControl.Instance.playeProfile.AvailableMissions.ToString();
                                string TimeUntilMissionCooldownAux = GlobalControl.Instance.playeProfile.timeUntilMissionCooldown;
                                string WinsAux = GlobalControl.Instance.playeProfile.Wins.ToString();
                                string LosesAux = GlobalControl.Instance.playeProfile.Loss.ToString();

                                User userUpdate = new User(usernameAux, profilepicAux, HPAux, LevelAux, LevelUpPointsAux, XPAux, StrengthAux, SpeedAux, AgilityAux, ArmorvAux, PvPCoinAux, PetCoinAux, PremiumCoinAux, CompanionPetAux, AvailableMissionsAux, TimeUntilMissionCooldownAux, DateTime.Now.ToString(), WinsAux, LosesAux);
                                string json = JsonUtility.ToJson(userUpdate);
                                reference.Child("users").Child(Userid).SetRawJsonValueAsync(json);
                                string PetName;
                                string PetID;
                                string PetHP;
                                string PetSTR;
                                string PetAGY;
                                string PetSPE;
                                string PetARM;
                                string PetLV;

                                for (int i = 1; i < GlobalControl.Instance.playeProfile.OwnedPets.Count; i++)
                                {
                                    PetName = GlobalControl.Instance.playeProfile.OwnedPets[i].PetName;
                                    PetID = GlobalControl.Instance.playeProfile.OwnedPets[i].PetID.ToString();
                                    PetHP = GlobalControl.Instance.playeProfile.OwnedPets[i].HP.ToString();
                                    PetSTR = GlobalControl.Instance.playeProfile.OwnedPets[i].Strength.ToString();
                                    PetAGY = GlobalControl.Instance.playeProfile.OwnedPets[i].Agility.ToString();
                                    PetSPE = GlobalControl.Instance.playeProfile.OwnedPets[i].Speed.ToString();
                                    PetARM = GlobalControl.Instance.playeProfile.OwnedPets[i].Armor.ToString();
                                    PetLV = GlobalControl.Instance.playeProfile.OwnedPets[i].Level.ToString();

                                    Initialpet iniatialpet = new Initialpet(PetName, PetID, PetHP, PetSTR, PetAGY, PetSPE, PetARM, PetLV);
                                    json = JsonUtility.ToJson(iniatialpet);
                                    reference.Child("users/" + Userid).Child("Pets").Child("Pet_" + i.ToString()).SetRawJsonValueAsync(json);
                                }
                                firstTime = false;
                                break;
                            }
                        }
                        Text textusername = GameObject.Find("Canvas/Lbl_Username").GetComponent<Text>();
                        textusername.text = dictUser["username"].ToString();

                        Debug.Log(dictUser["profilepic"].ToString());
                        profilepic.SetActive(true);

                        if (dictUser["profilepic"].ToString() == "Profile_1")
                            profilepic.GetComponent<Image>().sprite = sprite1;
                        if (dictUser["profilepic"].ToString() == "Profile_2")
                            profilepic.GetComponent<Image>().sprite = sprite2;
                        if (dictUser["profilepic"].ToString() == "Profile_3")
                            profilepic.GetComponent<Image>().sprite = sprite3;
                        if (dictUser["profilepic"].ToString() == "Profile_4")
                            profilepic.GetComponent<Image>().sprite = sprite4;

                        registered = true;
                        GlobalControl.Instance.playeProfile.PlayerID = Userid;
                        GlobalControl.Instance.playeProfile.BattleTag = dictUser["username"].ToString();
                        GlobalControl.Instance.playeProfile.PlayerSprite = profilepic.GetComponent<Image>().sprite;
                        GlobalControl.Instance.playeProfile.Level = int.Parse(dictUser["Level"].ToString());
                        GlobalControl.Instance.playeProfile.LevelUpPoints = int.Parse(dictUser["LevelUpPoints"].ToString());


                        GlobalControl.Instance.playeProfile.SpriteName = dictUser["profilepic"].ToString();
                        GlobalControl.Instance.playeProfile.HP = int.Parse(dictUser["HP"].ToString());
                        GlobalControl.Instance.playeProfile.XP = int.Parse(dictUser["XP"].ToString());
                        GlobalControl.Instance.playeProfile.Strength = int.Parse(dictUser["Strength"].ToString());
                        GlobalControl.Instance.playeProfile.Speed = int.Parse(dictUser["Speed"].ToString());
                        GlobalControl.Instance.playeProfile.Agility = int.Parse(dictUser["Agility"].ToString());
                        GlobalControl.Instance.playeProfile.Armor = int.Parse(dictUser["Armorv"].ToString());
                        GlobalControl.Instance.playeProfile.critic_prob = 0.1f;
                        GlobalControl.Instance.playeProfile.PvPCoin = int.Parse(dictUser["PvPCoin"].ToString());
                        GlobalControl.Instance.playeProfile.PetCoin = int.Parse(dictUser["PetCoin"].ToString());
                        GlobalControl.Instance.playeProfile.PremiumCoin = int.Parse(dictUser["PremiumCoin"].ToString());
                        GlobalControl.Instance.playeProfile.AvailableMissions = int.Parse(dictUser["AvailableMissions"].ToString());
                        GlobalControl.Instance.playeProfile.timeUntilMissionCooldown = dictUser["TimeUntilMissionCooldown"].ToString();
                        GlobalControl.Instance.playeProfile.LocalSaveTimeStamp = DateTime.Now.ToString();
                        GlobalControl.Instance.playeProfile.Wins = int.Parse(dictUser["Wins"].ToString());
                        GlobalControl.Instance.playeProfile.Loss = int.Parse(dictUser["Loss"].ToString());
                        GlobalControl.Instance.PrepareItems();//Crea las listas para poder hacer los add
<<<<<<< HEAD




                        Dictionary<string, System.Object> Inventory = (Dictionary<string, System.Object>)dictUser["Inventory"];                        
                        foreach (KeyValuePair<string, System.Object> InventoryItems in Inventory)
=======
                        Dictionary<string, System.Object> Inventory = (Dictionary<string, System.Object>)dictUser["Inventory"];
                        int itemid = 0;
                        foreach (KeyValuePair<string, System.Object> InventoryItemsaux in Inventory)
>>>>>>> 2bfe2ce24da34f2999cc6ad0f13f8cf93ce345a7
                        {
                            //Dictionary<string, System.Object> Inventory_lv2 = (Dictionary<string, System.Object>)Inventory[InventoryItemsaux.Key];
                            //GlobalControl.Instance.playeProfile.Inventory.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == int.Parse(Inventory_lv2["id"].ToString())));
                            Dictionary<string, System.Object> Inventory_lv2 = (Dictionary<string, System.Object>)Inventory[InventoryItemsaux.Key];
                            itemid = int.Parse((Inventory_lv2["id"].ToString()));
                            GlobalControl.Instance.playeProfile.Inventory.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == itemid));
                            GlobalControl.Instance.playeProfile.InventoryItemsIDs.Add(itemid);
                        }

                        Dictionary<string, System.Object> Equipedgear = (Dictionary<string, System.Object>)dictUser["Equipedgear"];
                        for (int i = 0; i < 5; i++)
                        {
                            itemid = int.Parse(Equipedgear["Item" + i.ToString()].ToString());
                            GlobalControl.Instance.playeProfile.EquipedGear.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == itemid));
                            GlobalControl.Instance.playeProfile.EquipedGearIDs.Add(itemid);
                        }

                        string id = "";
                        Dictionary<string, System.Object> EquipedItems = (Dictionary<string, System.Object>)dictUser["EquipedItems"];
                        for (int i = 0; i < EquipedItems.Count; i++)
                        {                            
                            if (EquipedItems["Item" + i.ToString()].ToString() != "")
                            {
                                itemid = int.Parse(EquipedItems["Item" + i.ToString()].ToString());
                                GlobalControl.Instance.playeProfile.EquipedItems.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == itemid));
                                GlobalControl.Instance.playeProfile.EquipedItemsIDs.Add(itemid);
                            }                            
                        }

                        Dictionary<string, System.Object> Ownedpets = (Dictionary<string, System.Object>)dictUser["Pets"];                        
                        foreach (KeyValuePair<string, System.Object> Ownedpetsaux in Ownedpets)
                        {
                            Pet AuxPet = ScriptableObject.CreateInstance("Pet") as Pet;
                            Dictionary<string, System.Object> Ownedpets_lv2 = (Dictionary<string, System.Object>)Ownedpets[Ownedpetsaux.Key];
                            if (Ownedpetsaux.Key == dictUser["CompanionPet"].ToString())
                            {
                                GlobalControl.Instance.playeProfile.CompanionPet.PetName = Ownedpets_lv2["Name"].ToString();
                                GlobalControl.Instance.playeProfile.CompanionPet.Level = int.Parse(Ownedpets_lv2["LVL"].ToString());
                                GlobalControl.Instance.playeProfile.CompanionPet.HP = float.Parse(Ownedpets_lv2["HP"].ToString());
                                GlobalControl.Instance.playeProfile.CompanionPet.Strength = int.Parse(Ownedpets_lv2["STR"].ToString());
                                GlobalControl.Instance.playeProfile.CompanionPet.Speed = int.Parse(Ownedpets_lv2["SPE"].ToString());
                                GlobalControl.Instance.playeProfile.CompanionPet.Agility = int.Parse(Ownedpets_lv2["AGY"].ToString());
                                GlobalControl.Instance.playeProfile.CompanionPet.Armor = int.Parse(Ownedpets_lv2["ARM"].ToString());
                            }
                                AuxPet.PetName = Ownedpets_lv2["Name"].ToString();
                                AuxPet.Level = int.Parse(Ownedpets_lv2["LVL"].ToString());
                                AuxPet.HP = float.Parse(Ownedpets_lv2["HP"].ToString());
                                AuxPet.Strength = int.Parse(Ownedpets_lv2["STR"].ToString());
                                AuxPet.Speed = int.Parse(Ownedpets_lv2["SPE"].ToString());
                                AuxPet.Agility = int.Parse(Ownedpets_lv2["AGY"].ToString());
                                AuxPet.Armor = int.Parse(Ownedpets_lv2["ARM"].ToString());
                                GlobalControl.Instance.playeProfile.OwnedPets.Add(AuxPet);
                                GlobalControl.Instance.playeProfile.OwnedPetsIDs.Add(int.Parse(Ownedpets_lv2["ID"].ToString()));//No tengo claro que guardar supongo que el slot?, pero si necesito el id para reconstruir la mascota apartir de eso
                        }
                        GlobalControl.Instance.SavePlayerData();
                        GlobalControl.Instance.SavePetsData();
                        Debug.Log("Termino de traer la info del usuario");
                        alredyFetchedFromDB = true;
                        firstTime = false;
                        break;
                    }
                }
                if (!registered)
                {
                    Debug.Log("sin battlet tag");
                    OpenPanel();
                }
            }
        });
    }
    public void OpenPanel()
    {
        bool isActive = Panel.activeSelf;
        Panel.SetActive(true);
    }
    public void GetOponentList(string Nivel)
    {        
        FirebaseDatabase.DefaultInstance.GetReference("Nivel").Child(Nivel).GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            Dictionary<string, System.Object> userids = (Dictionary<string, System.Object>)snapshot.Value;
            if (snapshot.Exists)
            {
                foreach (KeyValuePair<string, System.Object> users in userids)
                {
                    PlayerData PlayerAux = new PlayerData();
                    PlayerAux.Inventory = new List<Item>();
                    PlayerAux.EquipedGear = new List<Item>();
                    PlayerAux.EquipedItems = new List<Item>();
                    PlayerAux.OwnedPets = new List<Pet>();
                    Pet AuxPet = ScriptableObject.CreateInstance("Pet") as Pet;
                    string Enemyid = users.Key;
                    Debug.Log("Enemyid:" + Enemyid);
                    if (Enemyid == GlobalControl.Instance.playeProfile.PlayerID)
                    {
                        continue;
                    }
                    FirebaseDatabase.DefaultInstance.GetReference("users").Child(Enemyid).GetValueAsync().ContinueWith(task2 =>
                    {
                        DataSnapshot snapshot2 = task2.Result;
                        Dictionary<string, System.Object> enemstats = (Dictionary<string, System.Object>)snapshot2.Value;
                        if (snapshot2.Exists)
                        {
                            Dictionary<string, System.Object> EquipedGear = (Dictionary<string, System.Object>)enemstats["Equipedgear"];
                            Dictionary<string, System.Object> EquipedItems = (Dictionary<string, System.Object>)enemstats["EquipedItems"];
                            Dictionary<string, System.Object> Ownedpets = (Dictionary<string, System.Object>)enemstats["Pets"];

                            PlayerAux.BattleTag = enemstats["username"].ToString();
                            PlayerAux.PlayerID = Enemyid;
                            switch (enemstats["profilepic"].ToString())
                            {
                                case "Profile_1":
                                    PlayerAux.PlayerSprite = sprite1;
                                    break;
                                case "Profile_2":
                                    PlayerAux.PlayerSprite = sprite2;
                                    break;
                                case "Profile_3":
                                    PlayerAux.PlayerSprite = sprite3;
                                    break;
                                case "Profile_4":
                                    PlayerAux.PlayerSprite = sprite4;
                                    break;
                                default:
                                    break;
                            }
                            PlayerAux.Level = int.Parse(enemstats["Level"].ToString());
                            PlayerAux.HP = int.Parse(enemstats["HP"].ToString());
                            PlayerAux.Strength = int.Parse(enemstats["Strength"].ToString());
                            PlayerAux.Speed = int.Parse(enemstats["Speed"].ToString());
                            PlayerAux.Agility = int.Parse(enemstats["Agility"].ToString());
                            PlayerAux.Armor = int.Parse(enemstats["Armorv"].ToString());
                            PlayerAux.critic_prob = 0.1f;
                            foreach (KeyValuePair<string, System.Object> Ownedpetsaux in Ownedpets)
                            {
                                Dictionary<string, System.Object> Ownedpets_lv2 = (Dictionary<string, System.Object>)Ownedpets[Ownedpetsaux.Key];                                
                                if (Ownedpetsaux.Key == enemstats["CompanionPet"].ToString())
                                {
                                    AuxPet.PetName = Ownedpets_lv2["Name"].ToString();
                                    AuxPet.Level = int.Parse(Ownedpets_lv2["LVL"].ToString());
                                    AuxPet.HP = float.Parse(Ownedpets_lv2["HP"].ToString());
                                    AuxPet.Strength = int.Parse(Ownedpets_lv2["STR"].ToString());
                                    AuxPet.Speed = int.Parse(Ownedpets_lv2["SPE"].ToString());
                                    AuxPet.Agility = int.Parse(Ownedpets_lv2["AGY"].ToString());
                                    AuxPet.Armor = int.Parse(Ownedpets_lv2["ARM"].ToString());
                                }
                            }
                            PlayerAux.CompanionPet = AuxPet;
                            for (int i = 0; i < EquipedGear.Count; i++)
                            {
                                PlayerAux.EquipedGear.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == int.Parse(EquipedGear["Item" + i.ToString()].ToString())));
                            }
                            for (int i = 0; i < EquipedItems.Count; i++)
                            {
                                PlayerAux.EquipedItems.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == int.Parse(EquipedItems["Item" + i.ToString()].ToString())));
                            }
                            if(!OponentList.Exists(x=>x.PlayerID == Enemyid))
                                OponentList.Add(PlayerAux);
                        }                        
                    });                    
                }
            }
        });

        
    }
    public void AssignOponents()
    {
        PvPControl.Oponents = OponentList;
    }
    public void writeNewUser(string userId, string name)
    {
        firstTime = true;
        bool profile1 = img1.activeSelf;
        bool profile2 = img2.activeSelf;
        bool profile3 = img3.activeSelf;
        bool profile4 = img4.activeSelf;
        bool pet1 = imgpet1.activeSelf;
        bool pet2 = imgpet2.activeSelf;
        bool pet3 = imgpet3.activeSelf;
        bool pet4 = imgpet4.activeSelf;
        string profileimg = "Profile_1";
        string initialpet = "Mimic Sword";
        
        Text textHP = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_pv_v").GetComponent<Text>();
        Text textStrength = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_str_v").GetComponent<Text>();
        Text textAgility = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_agy_v").GetComponent<Text>();
        Text textSpeed = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_spe_v").GetComponent<Text>();
        Text textArmorv = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_arm_v").GetComponent<Text>();

        Text textHP_pet = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_pv_v_pet").GetComponent<Text>();
        Text textStrength_pet = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_str_v_pet").GetComponent<Text>();
        Text textAgility_pet = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_agy_v_pet").GetComponent<Text>();
        Text textSpeed_pet = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_spe_v_pet").GetComponent<Text>();
        Text textArmorv_pet = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_arm_v_pet").GetComponent<Text>();

        if (profile1)
            profileimg = "Profile_1";
        if (profile2)
            profileimg = "Profile_2";
        if (profile3)
            profileimg = "Profile_3";
        if (profile4)
            profileimg = "Profile_4";

        if (pet1)
            initialpet = "Mimic Sword";
        if (pet2)
            initialpet = "Toucan Panther";
        if (pet3)
            initialpet = "Gunblin";
        if (pet4)
            initialpet = "Rock golem";

        string userhp = textHP.text.ToString();
        string userstr = textStrength.text.ToString();
        string useragy = textAgility.text.ToString();
        string userspe = textSpeed.text.ToString();
        string userarm = textArmorv.text.ToString();

        string userhp_pet = textHP_pet.text.ToString();
        string userid_pet = PetDBManager.Instance.PetDB.Find(x => x.PetName == initialpet).PetID.ToString();
        string userstr_pet = textStrength_pet.text.ToString();
        string useragy_pet = textAgility_pet.text.ToString();
        string userspe_pet = textSpeed_pet.text.ToString();
        string userarm_pet = textArmorv_pet.text.ToString();

        
        User user = new User(name, profileimg, userhp, "5", "5", "1", userstr, userspe, useragy, userarm, "0", "0","0","Pet_1","10","1990/01/01", DateTime.Now.ToString(),"0","0", DateTime.Now.ToString());
        string json = JsonUtility.ToJson(user);
        reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
        
        EquipedItems equipedItems = new EquipedItems("5","","","","","");
        json = JsonUtility.ToJson(equipedItems);
        reference.Child("users/" + userId).Child("EquipedItems").SetRawJsonValueAsync(json);

        for (int i = 0; i < 6; i++)
        {
            Inventory inventory = new Inventory(i.ToString());
            json = JsonUtility.ToJson(inventory);
            reference.Child("users/" + userId).Child("Inventory").Child("item_" + i.ToString()).SetRawJsonValueAsync(json);
        }
        
        Equipedgear Equipedgear = new Equipedgear("0","1","2","3","4");
        json = JsonUtility.ToJson(Equipedgear);
        reference.Child("users/" + userId).Child("Equipedgear").SetRawJsonValueAsync(json);

        Initialpet iniatialpet = new Initialpet(initialpet, userid_pet, userhp_pet, userstr_pet, useragy_pet, userspe_pet, userarm_pet,"1");
        json = JsonUtility.ToJson(iniatialpet);
        reference.Child("users/" + userId).Child("Pets").Child("Pet_1").SetRawJsonValueAsync(json);

        Nivel nivel = new Nivel("5");
        json = JsonUtility.ToJson(nivel);
        reference.Child("Nivel").Child("5").Child(userId).SetRawJsonValueAsync(json);
    }

    public void writeNewUserOffline()
    {        
        bool profile1 = img1.activeSelf;
        bool profile2 = img2.activeSelf;
        bool profile3 = img3.activeSelf;
        bool profile4 = img4.activeSelf;
        bool pet1 = imgpet1.activeSelf;
        bool pet2 = imgpet2.activeSelf;
        bool pet3 = imgpet3.activeSelf;
        bool pet4 = imgpet4.activeSelf;
        string profileimg = "Profile_1";
        string initialpet = "Mimic Sword";
        Text textHP = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_pv_v").GetComponent<Text>();
        Text textStrength = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_str_v").GetComponent<Text>();
        Text textAgility = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_agy_v").GetComponent<Text>();
        Text textSpeed = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_spe_v").GetComponent<Text>();
        Text textArmorv = GameObject.Find("Canvas/Pn_Character/bg_character/bg_character_2/txt_arm_v").GetComponent<Text>();
        Text textHP_pet = GameObject.Find("Canvas/Pn_pet/bg_pet/bg_pet_2/txt_pv_v_pet").GetComponent<Text>();
        Text textStrength_pet = GameObject.Find("Canvas/bg_pet/bg_pet_2/txt_str_v_pet").GetComponent<Text>();
        Text textAgility_pet = GameObject.Find("Canvas/bg_pet/bg_pet_2/txt_agy_v_pet").GetComponent<Text>();
        Text textSpeed_pet = GameObject.Find("Canvas/bg_pet/bg_pet_2/txt_spe_v_pet").GetComponent<Text>();
        Text textArmorv_pet = GameObject.Find("Canvas/bg_pet/bg_pet_2/txt_arm_v_pet").GetComponent<Text>();

        if (profile1)
            profileimg = "Profile_1";
        if (profile2)
            profileimg = "Profile_2";
        if (profile3)
            profileimg = "Profile_3";
        if (profile4)
            profileimg = "Profile_4";

        if (pet1)
            initialpet = "Mimic Sword";
        if (pet2)
            initialpet = "Toucan Panther";
        if (pet3)
            initialpet = "Gunblin";
        if (pet4)
            initialpet = "Rock golem";

        string userhp = textHP.text.ToString();
        string userstr = textStrength.text.ToString();
        string useragy = textAgility.text.ToString();
        string userspe = textSpeed.text.ToString();
        string userarm = textArmorv.text.ToString();

        string userhp_pet = textHP_pet.text.ToString();
        string userstr_pet = textStrength_pet.text.ToString();
        string useragy_pet = textAgility_pet.text.ToString();
        string userspe_pet = textSpeed_pet.text.ToString();
        string userarm_pet = textArmorv_pet.text.ToString();

        GlobalControl.Instance.playeProfile.PlayerID = "0";
        GlobalControl.Instance.playeProfile.BattleTag = Battletag.text;
        switch (profileimg)
        {
            case "Profile_1":
                GlobalControl.Instance.playeProfile.PlayerSprite = img1.GetComponentInChildren<Image>().sprite;
                break;
            case "Profile_2":
                GlobalControl.Instance.playeProfile.PlayerSprite = img2.GetComponentInChildren<Image>().sprite;
                break;
            case "Profile_3":
                GlobalControl.Instance.playeProfile.PlayerSprite = img3.GetComponentInChildren<Image>().sprite;
                break;
            case "Profile_4":
                GlobalControl.Instance.playeProfile.PlayerSprite = img4.GetComponentInChildren<Image>().sprite;
                break;
            default:
                break;
        }

        GlobalControl.Instance.playeProfile.Level = 5;
        GlobalControl.Instance.playeProfile.AvailableMissions = 10;
        GlobalControl.Instance.playeProfile.HP = int.Parse(userhp);
        GlobalControl.Instance.playeProfile.XP = 0;
        GlobalControl.Instance.playeProfile.LevelUpPoints = 5;//agregar al write new user
        GlobalControl.Instance.playeProfile.Strength = int.Parse(userstr);
        GlobalControl.Instance.playeProfile.Speed = int.Parse(userspe);
        GlobalControl.Instance.playeProfile.Agility = int.Parse(useragy);
        GlobalControl.Instance.playeProfile.Armor = int.Parse(userarm);
        GlobalControl.Instance.playeProfile.critic_prob = 0.1f;
        GlobalControl.Instance.playeProfile.PvPCoin = 0;
        GlobalControl.Instance.playeProfile.PetCoin = 0;
        GlobalControl.Instance.playeProfile.PremiumCoin = 0;
        GlobalControl.Instance.PrepareItems();                

        for (int i = 0; i < 5; i++)//Se seteal el inventario
        {
            GlobalControl.Instance.playeProfile.Inventory.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == i));
            GlobalControl.Instance.playeProfile.InventoryItemsIDs.Add(i);
        }

        //Se equipa la armadura
        for (int i = 0; i < 5; i++)
        {
            GlobalControl.Instance.playeProfile.EquipedGear.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == i));
            GlobalControl.Instance.playeProfile.EquipedGearIDs.Add(i);
        }
        //GlobalControl.Instance.playeProfile.EquipedGear[(int)BodyZone.Head] = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == 0);
        //GlobalControl.Instance.playeProfile.EquipedGear[(int)BodyZone.Chest] = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == 1);
        //GlobalControl.Instance.playeProfile.EquipedGear[(int)BodyZone.Arms] = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == 2);
        //GlobalControl.Instance.playeProfile.EquipedGear[(int)BodyZone.Foots] = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == 3);
        //GlobalControl.Instance.playeProfile.EquipedGear[(int)BodyZone.Weapon] = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == 4);

        //Se equipan los items
        GlobalControl.Instance.playeProfile.EquipedItems.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == 52));
        GlobalControl.Instance.playeProfile.EquipedItemsIDs.Add(52);

        //Se asigna la mascota
        Pet auxPet = PetDBManager.Instance.PetDB.Find(x => x.PetName == initialpet);
        auxPet.HP = int.Parse(userhp_pet);
        auxPet.Strength = int.Parse(userstr_pet);
        auxPet.Agility = int.Parse(useragy_pet);
        auxPet.Speed = int.Parse(userspe_pet);
        auxPet.Armor = int.Parse(userarm_pet);
        GlobalControl.Instance.playeProfile.CompanionPet = auxPet;
        GlobalControl.Instance.playeProfile.OwnedPets.Add(auxPet);
        GlobalControl.Instance.playeProfile.OwnedPetsIDs.Add(auxPet.PetID);

        GlobalControl.Instance.SavePlayerData();
        GlobalControl.Instance.SavePetsData();
    }
    /*public void createpets()
    {
        string json;


        Pets Pets1 = new Pets("Bull", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets1); reference.Child("Pets").Child("Bull").SetRawJsonValueAsync(json);
        Pets Pets2 = new Pets("Lunar Butterfly", "Raro", "0", "0", "13", "12,5", "17,5", "25", "25", "35"); json = JsonUtility.ToJson(Pets2); reference.Child("Pets").Child("Lunar Butterfly").SetRawJsonValueAsync(json);
        Pets Pets3 = new Pets("Mantis", "Poco comun", "0", "10", "12", "12", "20", "30", "30", "45"); json = JsonUtility.ToJson(Pets3); reference.Child("Pets").Child("Mantis").SetRawJsonValueAsync(json);
        Pets Pets4 = new Pets("Roach", "Normal", "34", "30", "25", "25", "20", "10", "10", "0"); json = JsonUtility.ToJson(Pets4); reference.Child("Pets").Child("Roach").SetRawJsonValueAsync(json);
        Pets Pets5 = new Pets("Scarab", "Normal", "33", "30", "25", "25", "20", "15", "15", "0"); json = JsonUtility.ToJson(Pets5); reference.Child("Pets").Child("Scarab").SetRawJsonValueAsync(json);
        Pets Pets6 = new Pets("Tick", "Normal", "33", "30", "25", "25", "20", "15", "10", "0"); json = JsonUtility.ToJson(Pets6); reference.Child("Pets").Child("Tick").SetRawJsonValueAsync(json);
        Pets Pets7 = new Pets("Caterpillar", "Normal", "33", "30", "25", "25", "20", "15", "15", "0"); json = JsonUtility.ToJson(Pets7); reference.Child("Pets").Child("Caterpillar").SetRawJsonValueAsync(json);
        Pets Pets8 = new Pets("Giant Bug Centipede", "Normal", "33", "30", "25", "25", "20", "15", "10", "0"); json = JsonUtility.ToJson(Pets8); reference.Child("Pets").Child("Giant Bug Centipede").SetRawJsonValueAsync(json);
        Pets Pets9 = new Pets("Giant Bug Death Worm", "Poco comun", "0", "10", "12", "12", "20", "20", "20", "45"); json = JsonUtility.ToJson(Pets9); reference.Child("Pets").Child("Giant Bug Death Worm").SetRawJsonValueAsync(json);
        Pets Pets10 = new Pets("Insects Dragon", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets10); reference.Child("Pets").Child("Insects Dragon").SetRawJsonValueAsync(json);
        Pets Pets11 = new Pets("Red Ant Knight", "Raro", "0", "0", "13", "12,5", "17,5", "25", "25", "35"); json = JsonUtility.ToJson(Pets11); reference.Child("Pets").Child("Red Ant Knight").SetRawJsonValueAsync(json);
        Pets Pets12 = new Pets("Waterstrider", "Normal", "34", "30", "25", "25", "20", "20", "20", "0"); json = JsonUtility.ToJson(Pets12); reference.Child("Pets").Child("Waterstrider").SetRawJsonValueAsync(json);
        Pets Pets13 = new Pets("Black Ant Archer", "Normal", "50", "50", "20", "19,9", "18", "15", "10", "0"); json = JsonUtility.ToJson(Pets13); reference.Child("Pets").Child("Black Ant Archer").SetRawJsonValueAsync(json);
        Pets Pets14 = new Pets("Black Ant Berserker", "Raro", "0", "0", "20", "19,9", "18", "20", "20", "35"); json = JsonUtility.ToJson(Pets14); reference.Child("Pets").Child("Black Ant Berserker").SetRawJsonValueAsync(json);
        Pets Pets15 = new Pets("Black Ant Knight", "Normal", "30", "20", "20", "19,9", "17,5", "15", "15", "0"); json = JsonUtility.ToJson(Pets15); reference.Child("Pets").Child("Black Ant Knight").SetRawJsonValueAsync(json);
        Pets Pets16 = new Pets("Black Ant Mage", "Poco comun", "0", "10", "20", "19,9", "26", "30", "30", "45"); json = JsonUtility.ToJson(Pets16); reference.Child("Pets").Child("Black Ant Mage").SetRawJsonValueAsync(json);
        Pets Pets17 = new Pets("Black Ant Protector", "Normal", "20", "20", "20", "19,9", "18", "15", "15", "0"); json = JsonUtility.ToJson(Pets17); reference.Child("Pets").Child("Black Ant Protector").SetRawJsonValueAsync(json);
        Pets Pets18 = new Pets("Golem", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets18); reference.Child("Pets").Child("Golem").SetRawJsonValueAsync(json);
        Pets Pets19 = new Pets("Death Worm", "Normal", "35", "35", "25", "25", "18", "15", "15", "0"); json = JsonUtility.ToJson(Pets19); reference.Child("Pets").Child("Death Worm").SetRawJsonValueAsync(json);
        Pets Pets20 = new Pets("Giant Bug Hercules", "Normal", "15", "15", "25", "25", "21,5", "15", "10", "0"); json = JsonUtility.ToJson(Pets20); reference.Child("Pets").Child("Giant Bug Hercules").SetRawJsonValueAsync(json);
        Pets Pets21 = new Pets("Hell Mantis", "Poco comun", "0", "10", "13", "12,5", "20", "30", "30", "45"); json = JsonUtility.ToJson(Pets21); reference.Child("Pets").Child("Hell Mantis").SetRawJsonValueAsync(json);
        Pets Pets22 = new Pets("Swarm", "Raro", "0", "0", "12", "12", "20", "25", "25", "35"); json = JsonUtility.ToJson(Pets22); reference.Child("Pets").Child("Swarm").SetRawJsonValueAsync(json);
        Pets Pets23 = new Pets("Titan Tellia", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets23); reference.Child("Pets").Child("Titan Tellia").SetRawJsonValueAsync(json);
        Pets Pets24 = new Pets("Tridentpupa", "Normal", "50", "40", "25", "25", "18", "10", "10", "0"); json = JsonUtility.ToJson(Pets24); reference.Child("Pets").Child("Tridentpupa").SetRawJsonValueAsync(json);
        Pets Pets25 = new Pets("Dryad Mini", "Raro", "0", "0", "6", "5,5", "17,5", "25", "25", "25"); json = JsonUtility.ToJson(Pets25); reference.Child("Pets").Child("Dryad Mini").SetRawJsonValueAsync(json);
        Pets Pets26 = new Pets("Earth Dragon", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets26); reference.Child("Pets").Child("Earth Dragon").SetRawJsonValueAsync(json);
        Pets Pets27 = new Pets("Forest Spider", "Normal", "100", "80", "50", "50", "30", "25", "20", "0"); json = JsonUtility.ToJson(Pets27); reference.Child("Pets").Child("Forest Spider").SetRawJsonValueAsync(json);
        Pets Pets28 = new Pets("Imperial Widow", "Poco comun", "0", "20", "39", "39", "40", "20", "20", "30"); json = JsonUtility.ToJson(Pets28); reference.Child("Pets").Child("Imperial Widow").SetRawJsonValueAsync(json);
        Pets Pets29 = new Pets("Six-Wing Fairy", "Raro", "0", "0", "5", "5", "10", "25", "25", "25"); json = JsonUtility.ToJson(Pets29); reference.Child("Pets").Child("Six-Wing Fairy").SetRawJsonValueAsync(json);
        Pets Pets30 = new Pets("Feral Kitsune", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets30); reference.Child("Pets").Child("Feral Kitsune").SetRawJsonValueAsync(json);
        Pets Pets31 = new Pets("Rabbit Warriors Archer", "Poco comun", "0", "20", "20", "20", "17,5", "30", "30", "45"); json = JsonUtility.ToJson(Pets31); reference.Child("Pets").Child("Rabbit Warriors Archer").SetRawJsonValueAsync(json);
        Pets Pets32 = new Pets("Rabbit Warriors Bandit", "Normal", "25", "25", "20", "20", "20", "10", "10", "0"); json = JsonUtility.ToJson(Pets32); reference.Child("Pets").Child("Rabbit Warriors Bandit").SetRawJsonValueAsync(json);
        Pets Pets33 = new Pets("Rabbit Warriors Knight", "Normal", "25", "25", "20", "20", "20", "15", "10", "0"); json = JsonUtility.ToJson(Pets33); reference.Child("Pets").Child("Rabbit Warriors Knight").SetRawJsonValueAsync(json);
        Pets Pets34 = new Pets("Seven Sins Greed", "Raro", "0", "0", "20", "19,5", "20", "25", "25", "35"); json = JsonUtility.ToJson(Pets34); reference.Child("Pets").Child("Seven Sins Greed").SetRawJsonValueAsync(json);
        Pets Pets35 = new Pets("Wind Snake", "Normal", "50", "30", "20", "20", "20", "15", "15", "0"); json = JsonUtility.ToJson(Pets35); reference.Child("Pets").Child("Wind Snake").SetRawJsonValueAsync(json);
        Pets Pets36 = new Pets("Deer", "Normal", "50", "30", "30", "30", "30", "15", "15", "0"); json = JsonUtility.ToJson(Pets36); reference.Child("Pets").Child("Deer").SetRawJsonValueAsync(json);
        Pets Pets37 = new Pets("Elf_Assasin", "Normal", "20", "35", "25", "25", "20", "10", "10", "0"); json = JsonUtility.ToJson(Pets37); reference.Child("Pets").Child("Elf_Assasin").SetRawJsonValueAsync(json);
        Pets Pets38 = new Pets("Elves Rapier", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets38); reference.Child("Pets").Child("Elves Rapier").SetRawJsonValueAsync(json);
        Pets Pets39 = new Pets("Elves Rogue Elf", "Normal", "30", "30", "25", "25", "20", "15", "10", "0"); json = JsonUtility.ToJson(Pets39); reference.Child("Pets").Child("Elves Rogue Elf").SetRawJsonValueAsync(json);
        Pets Pets40 = new Pets("Elves Spellcaster", "Raro", "0", "0", "10", "9,5", "12", "25", "25", "35"); json = JsonUtility.ToJson(Pets40); reference.Child("Pets").Child("Elves Spellcaster").SetRawJsonValueAsync(json);
        Pets Pets41 = new Pets("Fairy Filia", "Poco comun", "0", "5", "10", "10", "15,5", "30", "30", "45"); json = JsonUtility.ToJson(Pets41); reference.Child("Pets").Child("Fairy Filia").SetRawJsonValueAsync(json);
        Pets Pets42 = new Pets("Arcane Golem", "Legendario", "0", "0", "0", "0,8", "4", "5", "10", "20"); json = JsonUtility.ToJson(Pets42); reference.Child("Pets").Child("Arcane Golem").SetRawJsonValueAsync(json);
        Pets Pets43 = new Pets("Gemstone Fire", "Poco comun", "0", "10", "12,5", "12,4", "12", "15", "15", "20"); json = JsonUtility.ToJson(Pets43); reference.Child("Pets").Child("Gemstone Fire").SetRawJsonValueAsync(json);
        Pets Pets44 = new Pets("Gemstone Thunder", "Poco comun", "0", "0", "12,5", "12,4", "12", "15", "15", "20"); json = JsonUtility.ToJson(Pets44); reference.Child("Pets").Child("Gemstone Thunder").SetRawJsonValueAsync(json);
        Pets Pets45 = new Pets("Gemstone Water", "Poco comun", "0", "10", "12,5", "12,4", "12", "15", "15", "20"); json = JsonUtility.ToJson(Pets45); reference.Child("Pets").Child("Gemstone Water").SetRawJsonValueAsync(json);
        Pets Pets46 = new Pets("Gemstone Wind", "Poco comun", "", "0", "12,5", "12,4", "12", "15", "15", "20"); json = JsonUtility.ToJson(Pets46); reference.Child("Pets").Child("Gemstone Wind").SetRawJsonValueAsync(json);
        Pets Pets47 = new Pets("Orb Fire", "Normal", "25", "20", "12,5", "12,4", "12", "5", "5", "0"); json = JsonUtility.ToJson(Pets47); reference.Child("Pets").Child("Orb Fire").SetRawJsonValueAsync(json);
        Pets Pets48 = new Pets("Orb Frost", "Normal", "25", "20", "12,5", "12,4", "12", "10", "5", "0"); json = JsonUtility.ToJson(Pets48); reference.Child("Pets").Child("Orb Frost").SetRawJsonValueAsync(json);
        Pets Pets49 = new Pets("Orb Thunder", "Normal", "25", "20", "12,5", "12,4", "12", "10", "10", "0"); json = JsonUtility.ToJson(Pets49); reference.Child("Pets").Child("Orb Thunder").SetRawJsonValueAsync(json);
        Pets Pets50 = new Pets("Orb Wind", "Normal", "25", "20", "12,5", "12,4", "12", "10", "10", "0"); json = JsonUtility.ToJson(Pets50); reference.Child("Pets").Child("Orb Wind").SetRawJsonValueAsync(json);
        Pets Pets51 = new Pets("Elemental Earth Spirit Tellia", "Normal", "100", "80", "25", "24,5", "22,5", "0", "0", "0"); json = JsonUtility.ToJson(Pets51); reference.Child("Pets").Child("Elemental Earth Spirit Tellia").SetRawJsonValueAsync(json);
        Pets Pets52 = new Pets("Elemental Goddess Airi", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets52); reference.Child("Pets").Child("Elemental Goddess Airi").SetRawJsonValueAsync(json);
        Pets Pets53 = new Pets("Elemental Goddess Flora", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets53); reference.Child("Pets").Child("Elemental Goddess Flora").SetRawJsonValueAsync(json);
        Pets Pets54 = new Pets("Elemental Goddess imp", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets54); reference.Child("Pets").Child("Elemental Goddess imp").SetRawJsonValueAsync(json);
        Pets Pets55 = new Pets("Elemental Goddess Yukia", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets55); reference.Child("Pets").Child("Elemental Goddess Yukia").SetRawJsonValueAsync(json);
        Pets Pets56 = new Pets("Elemental Ice Spirit Helida", "Poco comun", "0", "20", "25", "24,5", "22,5", "30", "10", "5"); json = JsonUtility.ToJson(Pets56); reference.Child("Pets").Child("Elemental Ice Spirit Helida").SetRawJsonValueAsync(json);
        Pets Pets57 = new Pets("Elemental Spirit Fire Blazia", "Raro", "0", "0", "25", "24,5", "22,5", "25", "25", "5"); json = JsonUtility.ToJson(Pets57); reference.Child("Pets").Child("Elemental Spirit Fire Blazia").SetRawJsonValueAsync(json);
        Pets Pets58 = new Pets("Elemental Wind Spirit Tempestia", "Raro", "0", "0", "25", "24,5", "22,5", "25", "25", "10"); json = JsonUtility.ToJson(Pets58); reference.Child("Pets").Child("Elemental Wind Spirit Tempestia").SetRawJsonValueAsync(json);
        Pets Pets59 = new Pets("Dragon King Blue", "Normal", "50", "33", "25", "25", "25", "25", "25", "25"); json = JsonUtility.ToJson(Pets59); reference.Child("Pets").Child("Dragon King Blue").SetRawJsonValueAsync(json);
        Pets Pets60 = new Pets("Dragon King Brown", "Normal", "0", "33", "25", "25", "25", "25", "25", "25"); json = JsonUtility.ToJson(Pets60); reference.Child("Pets").Child("Dragon King Brown").SetRawJsonValueAsync(json);
        Pets Pets61 = new Pets("Dragon King Green", "Normal", "0", "0", "25", "25", "25", "25", "25", "25"); json = JsonUtility.ToJson(Pets61); reference.Child("Pets").Child("Dragon King Green").SetRawJsonValueAsync(json);
        Pets Pets62 = new Pets("Dragon King Red", "Normal", "50", "34", "25", "25", "25", "25", "25", "25"); json = JsonUtility.ToJson(Pets62); reference.Child("Pets").Child("Dragon King Red").SetRawJsonValueAsync(json);
        Pets Pets63 = new Pets("Egypt Archer", "Normal", "20", "25", "20", "19,9", "15", "15", "15", "0"); json = JsonUtility.ToJson(Pets63); reference.Child("Pets").Child("Egypt Archer").SetRawJsonValueAsync(json);
        Pets Pets64 = new Pets("Egypt Axe", "Poco comun", "0", "25", "20", "19,9", "29,5", "30", "30", "45"); json = JsonUtility.ToJson(Pets64); reference.Child("Pets").Child("Egypt Axe").SetRawJsonValueAsync(json);
        Pets Pets65 = new Pets("Egypt Chariot", "Raro", "0", "0", "20", "19,9", "23", "25", "25", "35"); json = JsonUtility.ToJson(Pets65); reference.Child("Pets").Child("Egypt Chariot").SetRawJsonValueAsync(json);
        Pets Pets66 = new Pets("Egypt Knight", "Normal", "50", "25", "20", "19,9", "15", "10", "10", "0"); json = JsonUtility.ToJson(Pets66); reference.Child("Pets").Child("Egypt Knight").SetRawJsonValueAsync(json);
        Pets Pets67 = new Pets("Egypt Mage", "Normal", "30", "25", "20", "19,9", "15", "15", "10", "0"); json = JsonUtility.ToJson(Pets67); reference.Child("Pets").Child("Egypt Mage").SetRawJsonValueAsync(json);
        Pets Pets68 = new Pets("Hieracosphinx", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets68); reference.Child("Pets").Child("Hieracosphinx").SetRawJsonValueAsync(json);
        Pets Pets69 = new Pets("Cobra", "Poco comun", "0", "50", "40", "39,5", "40", "40", "40", "45"); json = JsonUtility.ToJson(Pets69); reference.Child("Pets").Child("Cobra").SetRawJsonValueAsync(json);
        Pets Pets70 = new Pets("Crocodile", "Normal", "100", "50", "40", "40", "25", "25", "20", "0"); json = JsonUtility.ToJson(Pets70); reference.Child("Pets").Child("Crocodile").SetRawJsonValueAsync(json);
        Pets Pets71 = new Pets("Mummy", "Raro", "0", "0", "20", "20", "32,5", "30", "30", "35"); json = JsonUtility.ToJson(Pets71); reference.Child("Pets").Child("Mummy").SetRawJsonValueAsync(json);
        Pets Pets72 = new Pets("Sphinx", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets72); reference.Child("Pets").Child("Sphinx").SetRawJsonValueAsync(json);
        Pets Pets73 = new Pets("Pirate Bandit", "Normal", "50", "30", "20", "19,9", "15", "15", "15", "0"); json = JsonUtility.ToJson(Pets73); reference.Child("Pets").Child("Pirate Bandit").SetRawJsonValueAsync(json);
        Pets Pets74 = new Pets("Pirate Captain", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets74); reference.Child("Pets").Child("Pirate Captain").SetRawJsonValueAsync(json);
        Pets Pets75 = new Pets("Pirate Magic Scimitar", "Raro", "0", "0", "20", "19,4", "23", "23", "23", "25"); json = JsonUtility.ToJson(Pets75); reference.Child("Pets").Child("Pirate Magic Scimitar").SetRawJsonValueAsync(json);
        Pets Pets76 = new Pets("Pirate Monkey", "Poco comun", "0", "20", "20", "19,9", "29,5", "30", "30", "35"); json = JsonUtility.ToJson(Pets76); reference.Child("Pets").Child("Pirate Monkey").SetRawJsonValueAsync(json);
        Pets Pets77 = new Pets("Pirate Parrot", "Normal", "30", "30", "20", "19,9", "15", "10", "2", "0"); json = JsonUtility.ToJson(Pets77); reference.Child("Pets").Child("Pirate Parrot").SetRawJsonValueAsync(json);
        Pets Pets78 = new Pets("Pirate Skeleton", "Normal", "20", "20", "20", "19,9", "12,5", "12", "10", "0"); json = JsonUtility.ToJson(Pets78); reference.Child("Pets").Child("Pirate Skeleton").SetRawJsonValueAsync(json);
        Pets Pets79 = new Pets("Turtle Golem", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets79); reference.Child("Pets").Child("Turtle Golem").SetRawJsonValueAsync(json);
        Pets Pets80 = new Pets("Mermaid", "Poco comun", "0", "33", "25", "24,5", "25", "30", "30", "15"); json = JsonUtility.ToJson(Pets80); reference.Child("Pets").Child("Mermaid").SetRawJsonValueAsync(json);
        Pets Pets81 = new Pets("Mermaid Warrior Arliette", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets81); reference.Child("Pets").Child("Mermaid Warrior Arliette").SetRawJsonValueAsync(json);
        Pets Pets82 = new Pets("Mermaid Warrior Sasha", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets82); reference.Child("Pets").Child("Mermaid Warrior Sasha").SetRawJsonValueAsync(json);
        Pets Pets83 = new Pets("Mermaid Warrior Sion", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets83); reference.Child("Pets").Child("Mermaid Warrior Sion").SetRawJsonValueAsync(json);
        Pets Pets84 = new Pets("Octopus", "Normal", "50", "34", "25", "24,5", "20", "10", "0", "0"); json = JsonUtility.ToJson(Pets84); reference.Child("Pets").Child("Octopus").SetRawJsonValueAsync(json);
        Pets Pets85 = new Pets("Piranos", "Normal", "50", "33", "25", "24,5", "20", "15", "5", "0"); json = JsonUtility.ToJson(Pets85); reference.Child("Pets").Child("Piranos").SetRawJsonValueAsync(json);
        Pets Pets86 = new Pets("Shark", "Raro", "0", "0", "25", "24,5", "25", "25", "25", "5"); json = JsonUtility.ToJson(Pets86); reference.Child("Pets").Child("Shark").SetRawJsonValueAsync(json);
        Pets Pets87 = new Pets("Titan Aquos", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets87); reference.Child("Pets").Child("Titan Aquos").SetRawJsonValueAsync(json);
        Pets Pets88 = new Pets("Skeleton Archer", "Normal", "25", "25", "25", "25", "20", "15", "15", "0"); json = JsonUtility.ToJson(Pets88); reference.Child("Pets").Child("Skeleton Archer").SetRawJsonValueAsync(json);
        Pets Pets89 = new Pets("Skeleton Dragon", "Raro", "0", "0", "5", "4,5", "15", "25", "25", "35"); json = JsonUtility.ToJson(Pets89); reference.Child("Pets").Child("Skeleton Dragon").SetRawJsonValueAsync(json);
        Pets Pets90 = new Pets("Skeleton Knight", "Normal", "50", "50", "30", "30", "20", "15", "10", "0"); json = JsonUtility.ToJson(Pets90); reference.Child("Pets").Child("Skeleton Knight").SetRawJsonValueAsync(json);
        Pets Pets91 = new Pets("Skeleton Knight Baron", "Poco comun", "0", "0", "20", "20", "27,5", "30", "30", "45"); json = JsonUtility.ToJson(Pets91); reference.Child("Pets").Child("Skeleton Knight Baron").SetRawJsonValueAsync(json);
        Pets Pets92 = new Pets("Skeleton Mage", "Normal", "25", "25", "20", "20", "15", "10", "10", "0"); json = JsonUtility.ToJson(Pets92); reference.Child("Pets").Child("Skeleton Mage").SetRawJsonValueAsync(json);
        Pets Pets93 = new Pets("Skull Knight Xoer", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets93); reference.Child("Pets").Child("Skull Knight Xoer").SetRawJsonValueAsync(json);
        Pets Pets94 = new Pets("Black Cat", "Raro", "0", "0", "10", "10", "15", "25", "25", "35"); json = JsonUtility.ToJson(Pets94); reference.Child("Pets").Child("Black Cat").SetRawJsonValueAsync(json);
        Pets Pets95 = new Pets("Pumpkin", "Normal", "50", "35", "20", "20", "20", "15", "10", "0"); json = JsonUtility.ToJson(Pets95); reference.Child("Pets").Child("Pumpkin").SetRawJsonValueAsync(json);
        Pets Pets96 = new Pets("Pumpkin Gentleman", "Poco comun", "0", "5", "20", "20", "27,5", "30", "30", "45"); json = JsonUtility.ToJson(Pets96); reference.Child("Pets").Child("Pumpkin Gentleman").SetRawJsonValueAsync(json);
        Pets Pets97 = new Pets("Pumpkin mini", "Normal", "40", "40", "30", "29,5", "15", "10", "10", "0"); json = JsonUtility.ToJson(Pets97); reference.Child("Pets").Child("Pumpkin mini").SetRawJsonValueAsync(json);
        Pets Pets98 = new Pets("Stein Monster", "Normal", "10", "20", "20", "20", "20", "15", "15", "0"); json = JsonUtility.ToJson(Pets98); reference.Child("Pets").Child("Stein Monster").SetRawJsonValueAsync(json);
        Pets Pets99 = new Pets("Ultra Stein", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets99); reference.Child("Pets").Child("Ultra Stein").SetRawJsonValueAsync(json);
        Pets Pets100 = new Pets("Carnivorous Plant", "Normal", "70", "50", "30", "29,5", "20", "15", "15", "0"); json = JsonUtility.ToJson(Pets100); reference.Child("Pets").Child("Carnivorous Plant").SetRawJsonValueAsync(json);
        Pets Pets101 = new Pets("Dryads Archer", "Normal", "0", "20", "20", "20", "20", "15", "10", "0"); json = JsonUtility.ToJson(Pets101); reference.Child("Pets").Child("Dryads Archer").SetRawJsonValueAsync(json);
        Pets Pets102 = new Pets("Dryads Mage", "Normal", "30", "30", "20", "20", "15", "10", "10", "0"); json = JsonUtility.ToJson(Pets102); reference.Child("Pets").Child("Dryads Mage").SetRawJsonValueAsync(json);
        Pets Pets103 = new Pets("Dryads Warrior", "Raro", "0", "0", "10", "10", "15", "25", "25", "35"); json = JsonUtility.ToJson(Pets103); reference.Child("Pets").Child("Dryads Warrior").SetRawJsonValueAsync(json);
        Pets Pets104 = new Pets("Hydra", "Poco comun", "0", "0", "20", "20", "27,5", "30", "30", "45"); json = JsonUtility.ToJson(Pets104); reference.Child("Pets").Child("Hydra").SetRawJsonValueAsync(json);
        Pets Pets105 = new Pets("Yggdrasil", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets105); reference.Child("Pets").Child("Yggdrasil").SetRawJsonValueAsync(json);
        Pets Pets106 = new Pets("Toxic Root", "Normal", "33", "25", "20", "19,9", "12,5", "10", "5", "0"); json = JsonUtility.ToJson(Pets106); reference.Child("Pets").Child("Toxic Root").SetRawJsonValueAsync(json);
        Pets Pets107 = new Pets("Undead Claw Knight", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets107); reference.Child("Pets").Child("Undead Claw Knight").SetRawJsonValueAsync(json);
        Pets Pets108 = new Pets("Undead Gigaraven", "Raro", "0", "0", "20", "19,4", "20", "25", "25", "25"); json = JsonUtility.ToJson(Pets108); reference.Child("Pets").Child("Undead Gigaraven").SetRawJsonValueAsync(json);
        Pets Pets109 = new Pets("Undead Skull Tree", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets109); reference.Child("Pets").Child("Undead Skull Tree").SetRawJsonValueAsync(json);
        Pets Pets110 = new Pets("Undead Walker", "Normal", "34", "25", "20", "19,9", "20", "10", "10", "0"); json = JsonUtility.ToJson(Pets110); reference.Child("Pets").Child("Undead Walker").SetRawJsonValueAsync(json);
        Pets Pets111 = new Pets("Undead Warrior", "Normal", "33", "25", "20", "19,9", "15", "15", "10", "0"); json = JsonUtility.ToJson(Pets111); reference.Child("Pets").Child("Undead Warrior").SetRawJsonValueAsync(json);
        Pets Pets112 = new Pets("Undead Wolf", "Poco comun", "0", "25", "20", "19,9", "27,5", "30", "30", "35"); json = JsonUtility.ToJson(Pets112); reference.Child("Pets").Child("Undead Wolf").SetRawJsonValueAsync(json);
        Pets Pets113 = new Pets("Banshee", "Poco comun", "5", "10", "10", "10", "15", "15", "15", "20"); json = JsonUtility.ToJson(Pets113); reference.Child("Pets").Child("Banshee").SetRawJsonValueAsync(json);
        Pets Pets114 = new Pets("Dark Axe Warrior", "Normal", "40", "40", "40", "40", "25", "15", "10", "0"); json = JsonUtility.ToJson(Pets114); reference.Child("Pets").Child("Dark Axe Warrior").SetRawJsonValueAsync(json);
        Pets Pets115 = new Pets("Dark Healer", "Raro", "0", "0", "5", "5", "10", "25", "25", "30"); json = JsonUtility.ToJson(Pets115); reference.Child("Pets").Child("Dark Healer").SetRawJsonValueAsync(json);
        Pets Pets116 = new Pets("Darkness Dullahan", "Normal", "55", "40", "40", "39,5", "32,5", "15", "15", "0"); json = JsonUtility.ToJson(Pets116); reference.Child("Pets").Child("Darkness Dullahan").SetRawJsonValueAsync(json);
        Pets Pets117 = new Pets("Darkness Reaper", "Raro", "0", "10", "5", "5", "15", "25", "25", "30"); json = JsonUtility.ToJson(Pets117); reference.Child("Pets").Child("Darkness Reaper").SetRawJsonValueAsync(json);
        Pets Pets118 = new Pets("Shadow Knight", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets118); reference.Child("Pets").Child("Shadow Knight").SetRawJsonValueAsync(json);
        Pets Pets119 = new Pets("Dark Monk", "Normal", "50", "40", "30", "30", "20", "20", "15", "0"); json = JsonUtility.ToJson(Pets119); reference.Child("Pets").Child("Dark Monk").SetRawJsonValueAsync(json);
        Pets Pets120 = new Pets("Dragon Emperor Zalaras", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets120); reference.Child("Pets").Child("Dragon Emperor Zalaras").SetRawJsonValueAsync(json);
        Pets Pets121 = new Pets("Ghost Knight", "Normal", "10", "10", "20", "20", "10", "10", "10", "0"); json = JsonUtility.ToJson(Pets121); reference.Child("Pets").Child("Ghost Knight").SetRawJsonValueAsync(json);
        Pets Pets122 = new Pets("Great witch", "Raro", "0", "0", "9", "9", "15", "15", "15", "25"); json = JsonUtility.ToJson(Pets122); reference.Child("Pets").Child("Great witch").SetRawJsonValueAsync(json);
        Pets Pets123 = new Pets("Succubus", "Poco comun", "0", "0", "10", "10", "20", "20", "20", "30"); json = JsonUtility.ToJson(Pets123); reference.Child("Pets").Child("Succubus").SetRawJsonValueAsync(json);
        Pets Pets124 = new Pets("Vampire", "Raro", "0", "10", "11", "10,5", "20", "20", "20", "25"); json = JsonUtility.ToJson(Pets124); reference.Child("Pets").Child("Vampire").SetRawJsonValueAsync(json);
        Pets Pets125 = new Pets("Witch", "Normal", "40", "40", "20", "20", "12,5", "10", "10", "0"); json = JsonUtility.ToJson(Pets125); reference.Child("Pets").Child("Witch").SetRawJsonValueAsync(json);
        Pets Pets126 = new Pets("Eldritch Eyes", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "100"); json = JsonUtility.ToJson(Pets126); reference.Child("Pets").Child("Eldritch Eyes").SetRawJsonValueAsync(json);
        Pets Pets127 = new Pets("Eldritch slime type A", "Normal", "33", "25", "20", "19,9", "19,5", "19", "18", "0"); json = JsonUtility.ToJson(Pets127); reference.Child("Pets").Child("Eldritch slime type A").SetRawJsonValueAsync(json);
        Pets Pets128 = new Pets("Eldritch slime type B", "Normal", "33", "25", "20", "19,9", "19,5", "19", "18", "0"); json = JsonUtility.ToJson(Pets128); reference.Child("Pets").Child("Eldritch slime type B").SetRawJsonValueAsync(json);
        Pets Pets129 = new Pets("Eldritch slime type C", "Normal", "34", "25", "20", "19,9", "19,5", "19", "18", "0"); json = JsonUtility.ToJson(Pets129); reference.Child("Pets").Child("Eldritch slime type C").SetRawJsonValueAsync(json);
        Pets Pets130 = new Pets("Eldritch slime type D", "Normal", "0", "25", "20", "19,9", "19,5", "19", "18", "0"); json = JsonUtility.ToJson(Pets130); reference.Child("Pets").Child("Eldritch slime type D").SetRawJsonValueAsync(json);
        Pets Pets131 = new Pets("Eldritch slime type F", "Normal", "0", "0", "20", "19,9", "19,5", "19", "18", "0"); json = JsonUtility.ToJson(Pets131); reference.Child("Pets").Child("Eldritch slime type F").SetRawJsonValueAsync(json);
        Pets Pets132 = new Pets("Kobold Paladin", "Poco comun", "0", "25", "10", "9,5", "27,5", "30", "30", "45"); json = JsonUtility.ToJson(Pets132); reference.Child("Pets").Child("Kobold Paladin").SetRawJsonValueAsync(json);
        Pets Pets133 = new Pets("Kobolds Dagger Kobold", "Normal", "33", "25", "25", "25", "15", "10", "10", "0"); json = JsonUtility.ToJson(Pets133); reference.Child("Pets").Child("Kobolds Dagger Kobold").SetRawJsonValueAsync(json);
        Pets Pets134 = new Pets("Kobolt Rogue", "Raro", "0", "0", "15", "15", "15", "25", "25", "35"); json = JsonUtility.ToJson(Pets134); reference.Child("Pets").Child("Kobolt Rogue").SetRawJsonValueAsync(json);
        Pets Pets135 = new Pets("kobolt ultra knight", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets135); reference.Child("Pets").Child("kobolt ultra knight").SetRawJsonValueAsync(json);
        Pets Pets136 = new Pets("Mage Kobold", "Normal", "33", "25", "25", "25", "20", "15", "10", "0"); json = JsonUtility.ToJson(Pets136); reference.Child("Pets").Child("Mage Kobold").SetRawJsonValueAsync(json);
        Pets Pets137 = new Pets("Spear Kobold", "Normal", "34", "25", "25", "25", "20", "15", "15", "0"); json = JsonUtility.ToJson(Pets137); reference.Child("Pets").Child("Spear Kobold").SetRawJsonValueAsync(json);
        Pets Pets138 = new Pets("Knight Axe Elite", "Normal", "25", "20", "15", "15", "15", "10", "10", "0"); json = JsonUtility.ToJson(Pets138); reference.Child("Pets").Child("Knight Axe Elite").SetRawJsonValueAsync(json);
        Pets Pets139 = new Pets("Knight Blunderbuss Elite", "Poco comun", "0", "20", "15", "15", "25", "30", "30", "60"); json = JsonUtility.ToJson(Pets139); reference.Child("Pets").Child("Knight Blunderbuss Elite").SetRawJsonValueAsync(json);
        Pets Pets140 = new Pets("Knight Spear Elite", "Normal", "25", "20", "20", "20", "15", "10", "10", "0"); json = JsonUtility.ToJson(Pets140); reference.Child("Pets").Child("Knight Spear Elite").SetRawJsonValueAsync(json);
        Pets Pets141 = new Pets("Red Guard knuckles", "Normal", "25", "20", "20", "20", "15", "15", "15", "0"); json = JsonUtility.ToJson(Pets141); reference.Child("Pets").Child("Red Guard knuckles").SetRawJsonValueAsync(json);
        Pets Pets142 = new Pets("Red Guard sniper", "Raro", "0", "0", "15", "15", "20", "25", "25", "40"); json = JsonUtility.ToJson(Pets142); reference.Child("Pets").Child("Red Guard sniper").SetRawJsonValueAsync(json);
        Pets Pets143 = new Pets("Red Guard warrior", "Normal", "25", "20", "15", "15", "10", "10", "10", "0"); json = JsonUtility.ToJson(Pets143); reference.Child("Pets").Child("Red Guard warrior").SetRawJsonValueAsync(json);
        Pets Pets144 = new Pets("Book Master", "Legendario", "0", "0", "0", "0,5", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets144); reference.Child("Pets").Child("Book Master").SetRawJsonValueAsync(json);
        Pets Pets145 = new Pets("Innova", "Raro", "0", "33", "21", "20,5", "28,5", "25", "25", "40"); json = JsonUtility.ToJson(Pets145); reference.Child("Pets").Child("Innova").SetRawJsonValueAsync(json);
        Pets Pets146 = new Pets("Novus", "Normal", "50", "33", "30", "30", "20", "20", "20", "0"); json = JsonUtility.ToJson(Pets146); reference.Child("Pets").Child("Novus").SetRawJsonValueAsync(json);
        Pets Pets147 = new Pets("Red Guard Knight", "Normal", "50", "34", "30", "30", "20", "20", "10", "0"); json = JsonUtility.ToJson(Pets147); reference.Child("Pets").Child("Red Guard Knight").SetRawJsonValueAsync(json);
        Pets Pets148 = new Pets("Red guard Reaper", "Raro", "0", "0", "19", "19", "29", "30", "30", "40"); json = JsonUtility.ToJson(Pets148); reference.Child("Pets").Child("Red guard Reaper").SetRawJsonValueAsync(json);
        Pets Pets149 = new Pets("Abomination Hound", "Poco comun", "0", "25", "20", "20", "30", "30", "30", "35"); json = JsonUtility.ToJson(Pets149); reference.Child("Pets").Child("Abomination Hound").SetRawJsonValueAsync(json);
        Pets Pets150 = new Pets("Abomination Tyrant", "Raro", "0", "0", "20", "19,4", "25", "25", "25", "30"); json = JsonUtility.ToJson(Pets150); reference.Child("Pets").Child("Abomination Tyrant").SetRawJsonValueAsync(json);
        Pets Pets151 = new Pets("Abominations Scout", "Normal", "20", "25", "20", "20", "10", "10", "8", "0"); json = JsonUtility.ToJson(Pets151); reference.Child("Pets").Child("Abominations Scout").SetRawJsonValueAsync(json);
        Pets Pets152 = new Pets("Cultist", "Normal", "50", "25", "20", "20", "20,5", "17", "10", "0"); json = JsonUtility.ToJson(Pets152); reference.Child("Pets").Child("Cultist").SetRawJsonValueAsync(json);
        Pets Pets153 = new Pets("God Yoggoth", "Epico", "0", "0", "0", "0,1", "1", "1", "2", "5"); json = JsonUtility.ToJson(Pets153); reference.Child("Pets").Child("God Yoggoth").SetRawJsonValueAsync(json);
        Pets Pets154 = new Pets("King Yoggoth", "Legendario", "0", "0", "0", "0,2", "1", "2", "5", "10"); json = JsonUtility.ToJson(Pets154); reference.Child("Pets").Child("King Yoggoth").SetRawJsonValueAsync(json);
        Pets Pets155 = new Pets("Queen Yoggoth", "Legendario", "0", "0", "0", "0,3", "2,5", "5", "10", "20"); json = JsonUtility.ToJson(Pets155); reference.Child("Pets").Child("Queen Yoggoth").SetRawJsonValueAsync(json);
        Pets Pets156 = new Pets("Abomination Gazer", "Normal", "30", "25", "20", "20", "10", "10", "10", "0"); json = JsonUtility.ToJson(Pets156); reference.Child("Pets").Child("Abomination Gazer").SetRawJsonValueAsync(json);
    }*/
}


public class Initialpet
{
    public string Name;
    public string ID;
    public string HP;
    public string STR;
    public string AGY;
    public string SPE;
    public string ARM;
    public string LVL;

    public Initialpet()
    {
    }
    public Initialpet(string Name, string ID,string HP, string STR, string AGY, string SPE, string ARM, string LVL)
    {
        this.Name = Name;
        this.ID = ID;
        this.HP = HP;
        this.STR = STR;
        this.AGY = AGY;
        this.SPE = SPE;
        this.ARM = ARM;
        this.LVL = LVL;
    }
}

public class EquipedItems
{
    public string Item0;
    public string Item1;
    public string Item2;
    public string Item3;
    public string Item4;
    public string Item5;


    public EquipedItems()
    {
    }
    public EquipedItems(string item0, string item1, string item2, string item3, string item4, string item5)
    {
        this.Item0 = item0;
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
        this.Item4 = item4;
        this.Item5 = item5;
    }
}
public class Inventory
{
    public string id;

    public Inventory()
    {
    }
    public Inventory(string id)
    {
        this.id= id;
    }
}

public class Equipedgear
{
    public string Item0;
    public string Item1;
    public string Item2;
    public string Item3;
    public string Item4;

    public Equipedgear()
    {
    }
    public Equipedgear(string Head, string Chest, string Arms, string Foots, string Weapon)
    {
        this.Item0 = Head;
        this.Item1 = Chest;
        this.Item2 = Arms;
        this.Item3 = Foots;
        this.Item4 = Weapon;
    }
}

public class User
{
    public string username;
    public string profilepic;
    public string HP;
    public string Level;
    public string LevelUpPoints;
    public string XP;
    public string Strength;
    public string Speed;
    public string Agility;
    public string Armorv;
    public string PvPCoin;
    public string PetCoin;
    public string PremiumCoin;
    public string CompanionPet;
    public string AvailableMissions;
    public string TimeUntilMissionCooldown;
    public string CloudSaveTimeStamp;
    public string Wins;
    public string Loss;
    public string UserCreationTimeStamp;
    public User()
    {
    }
    public User(string username,string profilepic, string HP, string Level, string LevelUpPoints, string XP, string Strength, string Speed, string Agility, string Armorv, string PvPCoin, string PetCoin, string PremiumCoin,string CompanionPet, string AvailableMissions, string TimeUntilMissionCooldown, string cloudSaveTimeStamp, string Wins, string Loss, string creationDate = null)
    {
        this.username = username;
        this.profilepic = profilepic;
        this.HP = HP;
        this.Level = Level;
        this.LevelUpPoints = LevelUpPoints;
        this.XP = XP;
        this.Strength = Strength;
        this.Speed = Speed;
        this.Agility = Agility;
        this.Armorv = Armorv;
        this.PvPCoin = PvPCoin;
        this.PetCoin = PetCoin;
        this.PremiumCoin = PremiumCoin;
        this.CompanionPet = CompanionPet;
        this.AvailableMissions = AvailableMissions;
        this.TimeUntilMissionCooldown = TimeUntilMissionCooldown;
        this.CloudSaveTimeStamp = cloudSaveTimeStamp;
        if (creationDate != null)
        {
            this.CloudSaveTimeStamp = UserCreationTimeStamp;
        }
        this.Wins = Wins;
        this.Loss = Loss;
    }
}


public class Pets
{
    public string Nombre;
    public string Tipo;
    public string Facil;
    public string Normal;
    public string Dificil;
    public string Extremo;
    public string Tormento1;
    public string Tormento2;
    public string Tormento3;
    public string PetHunter;

    public Pets()
    {
    }
    public Pets(string Nombre, string Tipo, string Facil, string Normal, string Dificil, string Extremo, string Tormento1, string Tormento2, string Tormento3, string PetHunter)
    {
        this.Nombre = Nombre;
        this.Tipo = Tipo;
        this.Facil = Facil;
        this.Normal = Normal;
        this.Dificil = Dificil;
        this.Extremo = Extremo;
        this.Tormento1 = Tormento1;
        this.Tormento2 = Tormento2;
        this.Tormento3 = Tormento3;
        this.PetHunter = PetHunter;
    }
}

public class Nivel
{
    public string Userlvl;

    public Nivel()
    {
    }
    public Nivel(string Userlvl)
    {
        this.Userlvl = Userlvl;
    }
}