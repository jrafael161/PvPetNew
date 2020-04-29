using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;
using System;
using Random2 = System.Random;

public class PVE : MonoBehaviour
{
    DatabaseReference reference;

    public GameObject Panelact1;
    public GameObject Panelact2;
    public GameObject Panelact3;
    public GameObject Panelact4;
    public GameObject Panelact5;
    public GameObject Panelpreparativos;
    public GameObject Panelmision;
    public Transform dropdownMenu;
    public GameObject btncaptura;

    public int Acto;
    public int Capitulo;
    public string titulo;

    private int hpuser;
    private int struser;
    private int aguser;
    private int speuser;
    private int armuser;
    private int expuser;
    private int available;

    public GameObject petimg;


    void Start()
    {
        DB();
        GetUserStats();
        //GlobalControl gcReal = GameObject.FindObjectOfType<GlobalControl>();
        //gc = gcReal.get_Instance();
    }

  
   
    public void showact1()
    {
        Panelact1.SetActive(true);
        Panelact2.SetActive(false);
        Panelact3.SetActive(false);
        Panelact4.SetActive(false);
        Panelact5.SetActive(false);
    }
    public void showact2()
    {
        Panelact1.SetActive(false);
        Panelact2.SetActive(true);
        Panelact3.SetActive(false);
        Panelact4.SetActive(false);
        Panelact5.SetActive(false);
    }
    public void showact3()
    {
        Panelact1.SetActive(false);
        Panelact2.SetActive(false);
        Panelact3.SetActive(true);
        Panelact4.SetActive(false);
        Panelact5.SetActive(false);
    }
    public void showact4()
    {
        Panelact1.SetActive(false);
        Panelact2.SetActive(false);
        Panelact3.SetActive(false);
        Panelact4.SetActive(true);
        Panelact5.SetActive(false);
    }
    public void showact5()
    {
        Panelact1.SetActive(false);
        Panelact2.SetActive(false);
        Panelact3.SetActive(false);
        Panelact4.SetActive(false);
        Panelact5.SetActive(true);
    }
    void DB()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pvpet-f0b05.firebaseio.com/Players/Pa5UU16uCzt6X1E1DJ6a");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    void GetUserStats()
    {
        string uid;
        //OBTIENE ID DE USUARIO
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        uid = textuserid.text.ToString();
        //uid = "8xLUp3Df6tW4wOOQOICsmmUswiq1";
        //OBTIENE LABES PARA PANTALLA
        Text textUsername = GameObject.Find("Canvas/lbl_username").GetComponent<Text>();
        Text textHP = GameObject.Find("Canvas/lbl_pv").GetComponent<Text>();
        Text textAgility = GameObject.Find("Canvas/lbl_ag").GetComponent<Text>();
        Text textSpeed = GameObject.Find("Canvas/lbl_sp").GetComponent<Text>();
        Text textStrength = GameObject.Find("Canvas/lbl_str").GetComponent<Text>();
        Text textArmorv = GameObject.Find("Canvas/lbl_arm").GetComponent<Text>();

        //reference.Child("users").Child(uid).Child("PVE").Child("available").SetValueAsync("1000");
        //CONECTA CON BASE
        FirebaseDatabase.DefaultInstance.GetReference("users").Child(uid).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("DataManager: read database is faulted with error: " + task.Exception.ToString());
                return;
            }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Dictionary<string, System.Object> attributes = (Dictionary<string, System.Object>)snapshot.Value;
                if (snapshot.Exists)
                {
                    //GUARDA TODOS LOS DATOS DE USUARIO, LLENA LAS VARIABLES DE USUARIO Y CAMBIA LOS LABELS PARA LA PANTALLA
                    Dictionary<string, System.Object> PVE = (Dictionary<string, System.Object>)attributes["PVE"];

                    hpuser = int.Parse(attributes["HP"].ToString());
                    struser = int.Parse(attributes["Strength"].ToString());
                    aguser = int.Parse(attributes["Agility"].ToString());
                    speuser = int.Parse(attributes["Speed"].ToString());
                    armuser = int.Parse(attributes["Armorv"].ToString());
                    expuser = int.Parse(attributes["Armorv"].ToString());

                    available = int.Parse(PVE["available"].ToString());
                    
                    textUsername.text = attributes["username"].ToString();
                    textHP.text = "HP:" + attributes["HP"].ToString();
                    textAgility.text = "AGY:" + attributes["Agility"].ToString();
                    textSpeed.text = "SPE:" + attributes["Speed"].ToString();
                    textStrength.text = "STR:" + attributes["Strength"].ToString();
                    textArmorv.text = "ARM:" + attributes["Armorv"].ToString();
                }
                else
                {
                    Debug.LogError("DataManager: Database for the user not available.");
                }
            }
        });
    }
    public void preparativosmision()
    {
        Panelpreparativos.SetActive(true);
        Text Act = GameObject.Find("Canvas/pnl_preparativos/lbl_act_v").GetComponent<Text>();
        Text Cap = GameObject.Find("Canvas/pnl_preparativos/lbl_cap_v").GetComponent<Text>();
        Text Tit = GameObject.Find("Canvas/pnl_preparativos/lbl_tit_v").GetComponent<Text>();
        Act.text = Acto.ToString();
        Cap.text = Capitulo.ToString();
        Tit.text = titulo;
       
    }
    public void gomision()
    {

        Text Act = GameObject.Find("Canvas/pnl_preparativos/lbl_act_v").GetComponent<Text>();
        Text Cap = GameObject.Find("Canvas/pnl_preparativos/lbl_cap_v").GetComponent<Text>();
        Text Tit = GameObject.Find("Canvas/pnl_preparativos/lbl_tit_v").GetComponent<Text>();
        int Mcla = int.Parse(Act.text);
        int Mcap = int.Parse(Cap.text);
        int VO = 0;
        int LE = 0;
        int MUL = 0;
        int ddDificultad = dropdownMenu.GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuDificultad = dropdownMenu.GetComponent<Dropdown>().options;
        string Diff = menuDificultad[ddDificultad].text;
        DB();
        GetUserStats();
        available = available - 1;
        string uid;
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        uid = textuserid.text.ToString();
        //uid = "8xLUp3Df6tW4wOOQOICsmmUswiq1";

        reference.Child("users").Child(uid).Child("PVE").Child("available").SetValueAsync(available);


        Debug.Log(Diff);
        switch (Diff)
        {
            case "Facil":
                LE = 1;VO = 10; MUL = 1;
                break;
            case "Normal":
                LE = 10; VO = 20; MUL = 2;
                break;
            case "Dificil":
                LE = 20; VO = 30; MUL = 3;
                break;
            case "Extremo":
                LE = 30; VO = 40; MUL = 4;
                break;
            case "Tormento1":
                LE = 40; VO = 60; MUL = 5;
                break;
            case "Tormento2":
                LE = 60; VO = 90; MUL = 6;
                break;
            case "Tormento3":
                LE = 90; VO = 150; MUL = 7;
                break;
            case "PetHunter":
                LE = 150; VO = 300; MUL = 8;
                break;
        };

        VO = (VO + Mcap) * MUL;
        LE = (LE + Mcap) * MUL;
        List<Itemc> items = new List<Itemc>();
        List<Pet> MissionPets = new List<Pet>();
        MissionPets = PetDBManager.Instance.PetDB.FindAll(x => x.Mision == Mcap);
        for (int i = 0; i < MissionPets.Count; i++)
        {
            string Petnombre = MissionPets[i].name;
            string Pettipo = MissionPets[i].Pt.ToString();
            int Petchance = 0;
            switch (Diff)
            {
                case "Facil":
                    Petchance = int.Parse(MissionPets[i].ProbFacil.ToString());
                    break;
                case "Normal":
                    Petchance = int.Parse(MissionPets[i].ProbNormal.ToString());
                    break;
                case "Dificil":
                    Petchance = int.Parse(MissionPets[i].ProbDificil.ToString());
                    break;
                case "Extremo":
                    Petchance = int.Parse(MissionPets[i].ProbExtremo.ToString());
                    break;
                case "Purgatorio":
                    Petchance = int.Parse(MissionPets[i].ProbPurgatorio.ToString());
                    break;
                case "Agonia":
                    Petchance = int.Parse(MissionPets[i].ProbAgonia.ToString());
                    break;
                case "Tormento":
                    Petchance = int.Parse(MissionPets[i].ProbTormento.ToString());
                    break;
                case "PetHunter":
                    Petchance = int.Parse(MissionPets[i].ProbPetHunter.ToString());
                    break;
            };
            items.Add(
                new Itemc()
                {
                    name = Petnombre,
                    chance = Petchance,
                    type = Pettipo,
                    PetSprite = MissionPets[i].PetSprite
                }
            );
        }
        Itemc Petichooseyou = ProportionalWheelSelection.SelectItem(items);
        int pethp = UnityEngine.Random.Range(LE, VO);
        int petstr = UnityEngine.Random.Range(LE, VO);
        int petagy = UnityEngine.Random.Range(LE, VO);
        int petspe = UnityEngine.Random.Range(LE, VO);
        int petarm = UnityEngine.Random.Range(LE, VO);

        petimg.GetComponent<Image>().sprite = Petichooseyou.PetSprite;
        Panelpreparativos.SetActive(false);
        Panelmision.SetActive(true);
        Text petnametxt = GameObject.Find("Canvas/pnl_mision/txt_name").GetComponent<Text>();
        Text petpvtxt = GameObject.Find("Canvas/pnl_mision/txt_pv_v").GetComponent<Text>();
        Text petstrtxt = GameObject.Find("Canvas/pnl_mision/txt_str_v").GetComponent<Text>();
        Text petspetxt = GameObject.Find("Canvas/pnl_mision/txt_spe_v").GetComponent<Text>();
        Text petagytxt = GameObject.Find("Canvas/pnl_mision/txt_agy_v").GetComponent<Text>();
        Text petarmtxt = GameObject.Find("Canvas/pnl_mision/txt_arm_v").GetComponent<Text>();
        petnametxt.text = Petichooseyou.name;
        Debug.Log(Petichooseyou.type);
        if (Petichooseyou.type == "Normal")
            petnametxt.color = Color.white;
        else if (Petichooseyou.type == "Poco comun")
            petnametxt.color = Color.green;
        else if (Petichooseyou.type == "Raro")
            petnametxt.color = Color.blue;
        else if (Petichooseyou.type == "Legendario")
            petnametxt.color = new Color(0.8F, 0.4F, 0F);
        else if (Petichooseyou.type == "Epico")
            petnametxt.color = new Color(0.3F, 0F, 0.6F);

        petpvtxt.text = pethp.ToString();
        petstrtxt.text = petstr.ToString();
        petspetxt.text = petagy.ToString();
        petagytxt.text = petspe.ToString();
        petarmtxt.text = petarm.ToString();

        GlobalControl.Instance.oponentProfile.BattleTag = Petichooseyou.name;
        GlobalControl.Instance.oponentProfile.PlayerSprite = Petichooseyou.PetSprite;
        GlobalControl.Instance.oponentProfile.HP = pethp;
        GlobalControl.Instance.oponentProfile.Strength = petstr;
        GlobalControl.Instance.oponentProfile.Agility = petagy;
        GlobalControl.Instance.oponentProfile.Speed = petspe;
        GlobalControl.Instance.oponentProfile.Armor = petarm;
        GlobalControl.Instance.oponentProfile.critic_prob = 0.1f;

        GlobalControl.Instance.oponentProfile.Inventory = null;
        GlobalControl.Instance.oponentProfile.EquipedItems = null;

        GlobalControl.Instance.oponentProfile.EquipedGear = new List<Item>(4);
        for (int i = 0; i < 5; i++)
        {
            GlobalControl.Instance.oponentProfile.EquipedGear.Add(null);
        }
        GlobalControl.Instance.oponentProfile.EquipedGear[4] = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == 50);

        //SceneManager.LoadScene("CombatScreen");

        bool estatus = true;


        if (estatus)
            btncaptura.SetActive(true);
        else
            btncaptura.SetActive(false);


    }

    public void capurarpet()
    {
        string uid = "";
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        uid = textuserid.text.ToString();
        //uid = "8xLUp3Df6tW4wOOQOICsmmUswiq1";

        Debug.Log(uid);
        Text petnametxt = GameObject.Find("Canvas/pnl_mision/txt_name").GetComponent<Text>();
        Text petpvtxt = GameObject.Find("Canvas/pnl_mision/txt_pv_v").GetComponent<Text>();
        Text petstrtxt = GameObject.Find("Canvas/pnl_mision/txt_str_v").GetComponent<Text>();
        Text petspetxt = GameObject.Find("Canvas/pnl_mision/txt_spe_v").GetComponent<Text>();
        Text petagytxt = GameObject.Find("Canvas/pnl_mision/txt_agy_v").GetComponent<Text>();
        Text petarmtxt = GameObject.Find("Canvas/pnl_mision/txt_arm_v").GetComponent<Text>();
        string petname = petnametxt.text.ToString();
        string pethp = petpvtxt.text.ToString();
        string petstr = petstrtxt.text.ToString();
        string petagy = petagytxt.text.ToString();
        string petspe = petspetxt.text.ToString();
        string petarm = petarmtxt.text.ToString();


        Capturapet Capturapet = new Capturapet( pethp, petstr, petspe, petagy, petarm);
        string json = JsonUtility.ToJson(Capturapet);
        reference.Child("users/" + uid).Child("PETS").Child(petname).SetRawJsonValueAsync(json);
    }

    public void exitmision()
    {
        Panelmision.SetActive(false);
    }


    void Getdata()
    {
        Firebase.Database.FirebaseDatabase dbInstance = Firebase.Database.FirebaseDatabase.DefaultInstance;
        dbInstance.GetReference("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;
                    Debug.Log(dictUser["username"].ToString());
                }
            }
        });
    }

    public void Cancelmision()
    {
        Panelpreparativos.SetActive(false);
    }
}


public class Itemc
{
    public string name; // not only string, any type of data
    public int chance;  // chance of getting this Item
    public string type;
    public Sprite PetSprite;
}

public class ProportionalWheelSelection
{

    public static Random2 rnd = new Random2();
    public static Itemc SelectItem(List<Itemc> items)
    {
        // Calculate the summa of all portions.
        int poolSize = 0;
        for (int i = 0; i < items.Count; i++)
        {
            poolSize += items[i].chance;
        }

        // Get a random integer from 0 to PoolSize.
        int randomNumber = rnd.Next(0, poolSize) + 1;

        // Detect the item, which corresponds to current random number.
        int accumulatedProbability = 0;
        for (int i = 0; i < items.Count; i++)
        {
            accumulatedProbability += items[i].chance;
            if (randomNumber <= accumulatedProbability)
                return items[i];
        }
        return null;    // this code will never come while you use this programm right :)
    }
}

public class Capturapet
{
    public string HP;
    public string STR;
    public string SPE;
    public string AGY;
    public string ARM;
    public Capturapet()
    {
    }
    public Capturapet(string HP, string STR, string SPE, string AGY, string ARM)
    {
        this.HP = HP;
        this.STR = STR;
        this.SPE = SPE;
        this.AGY = AGY;
        this.ARM = ARM;
    }
}