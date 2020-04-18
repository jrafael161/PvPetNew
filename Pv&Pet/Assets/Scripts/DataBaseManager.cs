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

    [SerializeField]
    private InputField Battletag = null;
    DatabaseReference reference;


    void Start()
    {
        DB();
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        Checkforbattletag();
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
    /*void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            user = auth.CurrentUser;
            if (signedIn)
            {
                Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
                textuserid.text = user.UserId;
                Checkforbattletag();
                Debug.Log("usuario autentificado");
            }
            else
            {
                SceneManager.LoadScene("00-Login");
            }
        }
    }*/
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        Checkforbattletag();
    }
    public void Checkforbattletag()
    {
       
        string Userid;
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        Userid = textuserid.text;
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
                        Text textusername = GameObject.Find("Canvas/Lbl_Username").GetComponent<Text>();
                        textusername.text = dictUser["username"].ToString();
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
        Checkforbattletag();
        Panel.SetActive(false);
    }
    public void OpenPanel()
    {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(true);
     }
    private void writeNewUser(string userId, string name)
    {
        User user = new User(name, "100", "1", "1", "20", "20", "20", "0", "50", "50", "1", "1", "1", "1", "1", "1", "1");
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


    }
    public void SignOut()
    {
        SceneManager.LoadScene("00-Login");

    }
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
public class User
{
    public string username;
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
    public string HeadGear;
    public string ChestGear;
    public string ArmsGear;
    public string Shield;
    public string FootsGear;
    public string Weapon;
    public User()
    {
    }
    public User(string username, string HP, string Level, string XP, string Strength, string Speed, string Agility, string Armorv, string PvPCoin, string PetCoin, string PremiumCoin, string HeadGear, string ChestGear, string FootsGear,string ArmsGear, string Weapon, string Shield)
    {
        this.username = username;
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
        this.HeadGear = HeadGear;
        this.ChestGear = ChestGear;
        this.FootsGear = FootsGear;
        this.ArmsGear = ArmsGear;
        this.Weapon = Weapon;
        this.Shield = Shield;
    }
}
