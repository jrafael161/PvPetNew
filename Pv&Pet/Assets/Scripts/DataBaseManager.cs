using System.Collections;
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
    public static Firebase.Auth.FirebaseAuth auth;
    public static Firebase.Auth.FirebaseUser user;
    private string displayName;
    private bool signedIn;
    private bool registered;
    public GameObject Panel;

    public GameObject img1;
    public GameObject img2;
    public GameObject img3;
    public GameObject img4;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;


    public GameObject profilepic;


    [SerializeField]
    private InputField Battletag = null;
    DatabaseReference reference;

    void Start()
    {
        DB();
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        Checkforbattletag(textuserid.text.ToString());
    }

    void DB()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pvpet-f0b05.firebaseio.com/Players/Pa5UU16uCzt6X1E1DJ6a");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        Checkforbattletag(textuserid.text.ToString());
    }

    public void chechprofileimg1()
    {
        img1.SetActive(true);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(false);
    }
    public void chechprofileimg2()
    {
        img1.SetActive(false);
        img2.SetActive(true);
        img3.SetActive(false);
        img4.SetActive(false);
    }
    public void chechprofileimg3()
    {
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(true);
        img4.SetActive(false);
    }
    public void chechprofileimg4()
    {
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(true);
    }

    public void Checkforbattletag(string Userid)
    {
        Debug.Log("Checando bt");
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
                        Text textusername = GameObject.Find("Canvas/bg_main/Lbl_Username").GetComponent<Text>();
                        textusername.text = dictUser["username"].ToString();

                        Debug.Log(dictUser["profilepic"].ToString());
                        profilepic.SetActive(true);


                        if (dictUser["profilepic"].ToString() =="Profile_1")
                        {
                            Debug.Log(dictUser["profilepic"].ToString());

                            profilepic.GetComponent<Image>().sprite = sprite1;
                        }
                        if (dictUser["profilepic"].ToString() == "Profile_2")
                        {
                            Debug.Log(dictUser["profilepic"].ToString());

                            profilepic.GetComponent<Image>().sprite = sprite2;
                        }
                        if (dictUser["profilepic"].ToString() == "Profile_3")
                        {
                            Debug.Log(dictUser["profilepic"].ToString());

                            profilepic.GetComponent<Image>().sprite = sprite3;
                        }
                        if (dictUser["profilepic"].ToString() == "Profile_4")
                        {
                            Debug.Log(dictUser["profilepic"].ToString());

                            profilepic.GetComponent<Image>().sprite = sprite4;
                        }
                        registered = true;
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
    public void Btn_go()
    {
        string Userid;
        string Battletaguser = Battletag.text;
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        Userid = textuserid.text;
        writeNewUser(Userid, Battletaguser);
        Debug.Log("uid:"+ Userid);

        Debug.Log("Usuario creado");
        Checkforbattletag(Userid);
        Panel.SetActive(false);
    }

    public void OpenPanel()
    {
        bool isActive = Panel.activeSelf;
        Panel.SetActive(true);
    }
    
    private void writeNewUser(string userId, string name)
    {
        bool profile1 = img1.activeSelf;
        bool profile2 = img2.activeSelf;
        bool profile3 = img3.activeSelf;
        bool profile4 = img4.activeSelf;
        string profileimg = "Profile_1";

        if(profile1)
            profileimg = "Profile_1";
        if (profile2)
            profileimg = "Profile_2";
        if (profile3)
            profileimg = "Profile_3";
        if (profile4)
            profileimg = "Profile_4";

        User user = new User(name, profileimg, "100", "1", "1", "20", "20", "20", "0", "50", "50","1");
        string json = JsonUtility.ToJson(user);
        reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
        
        EquipedItems equipedItems = new EquipedItems("1");
        json = JsonUtility.ToJson(equipedItems);
        reference.Child("users/" + userId).Child("EquipedItems").SetRawJsonValueAsync(json);
        
        Inventory inventory = new Inventory("1");
        json = JsonUtility.ToJson(inventory);
        reference.Child("users/" + userId).Child("Inventory").SetRawJsonValueAsync(json);

        PveU PveU = new PveU("10");
        json = JsonUtility.ToJson(PveU);
        reference.Child("users/" + userId).Child("PVE").SetRawJsonValueAsync(json);

        Equipedgear Equipedgear = new Equipedgear("1","2","3","4","5");
        json = JsonUtility.ToJson(Equipedgear);
        reference.Child("users/" + userId).Child("Equipedgear").SetRawJsonValueAsync(json);


    }
    public void SignOut()
    {
        SceneManager.LoadScene("00-Login");

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
public class EquipedItems
{
    public string item;
    public EquipedItems()
    {
    }
    public EquipedItems(string item)
    {
        this.item = item;
    }
}
public class Inventory
{
    public string item;
    public Inventory()
    {
    }
    public Inventory(string item)
    {
        this.item = item;
    }
}
public class PveU
{
    public string available;

    public PveU()
    {
    }
    public PveU(string available)
    {
        this.available = available;
    }
}
public class Equipedgear
{
    public string Head;
    public string Chest;
    public string Arms;
    public string Foots;
    public string Weapon;

    public Equipedgear()
    {
    }
    public Equipedgear(string Head, string Chest, string Arms, string Foots, string Weapon)
    {
        this.Head = Head;
        this.Chest = Chest;
        this.Arms = Arms;
        this.Foots = Foots;
        this.Weapon = Weapon;
    }
}

public class User
{
    public string username;
    public string profilepic;
    public string HP;
    public string Level;
    public string XP;
    public string Strength;
    public string Speed;
    public string Agility;
    public string Armorv;
    public string PvPCoin;
    public string PetCoin;
    public string PremiumCoin;

    public User()
    {
    }
    public User(string username,string profilepic, string HP, string Level, string XP, string Strength, string Speed, string Agility, string Armorv, string PvPCoin, string PetCoin, string PremiumCoin)
    {
        this.username = username;
        this.profilepic = profilepic;
        this.HP = HP;
        this.Level = Level;
        this.XP = XP;
        this.Strength = Strength;
        this.Speed = Speed;
        this.Agility = Agility;
        this.Armorv = Armorv;
        this.PvPCoin = PvPCoin;
        this.PetCoin = PetCoin;
        this.PremiumCoin = PremiumCoin;
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
