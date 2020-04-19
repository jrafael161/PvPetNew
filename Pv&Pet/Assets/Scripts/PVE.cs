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


    public int strenemy;
    public int agenemy;
    public int speenemy;
    public int armenemy;

    private int hpuser;
    private int struser;
    private int aguser;
    private int speuser;
    private int armuser;
    private int expuser;
    private int available;


    void Start()
    {
        DB();
        GetUserStats();
    }
    public void Refreshlist()
    {
        DB();
        GetUserStats();
        //Getdata();
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
        uid = "8xLUp3Df6tW4wOOQOICsmmUswiq1";
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
        Text textAgilitym = GameObject.Find("Canvas/pnl_preparativos/lbl_ag_m_v").GetComponent<Text>();
        Text textSpeedm = GameObject.Find("Canvas/pnl_preparativos/lbl_sp_m_v").GetComponent<Text>();
        Text textStrengthm = GameObject.Find("Canvas/pnl_preparativos/lbl_str_m_v").GetComponent<Text>();
        Text textArmorvm = GameObject.Find("Canvas/pnl_preparativos/lbl_arm_m_v").GetComponent<Text>();
        textAgilitym.text = agenemy.ToString();
        textSpeedm.text = speenemy.ToString();
        textStrengthm.text = strenemy.ToString();
        textArmorvm.text = armenemy.ToString();
    }
    public void gomision()
    {
        Text textAgilitym = GameObject.Find("Canvas/pnl_preparativos/lbl_ag_m_v").GetComponent<Text>();
        Text textSpeedm = GameObject.Find("Canvas/pnl_preparativos/lbl_sp_m_v").GetComponent<Text>();
        Text textStrengthm = GameObject.Find("Canvas/pnl_preparativos/lbl_sp_m_v").GetComponent<Text>();
        Text textArmorvm = GameObject.Find("Canvas/pnl_preparativos/lbl_arm_m_v").GetComponent<Text>();
        int strenemy = int.Parse(textStrengthm.text);
        int agyenemy = int.Parse(textAgilitym.text);
        int speenemy = int.Parse(textSpeedm.text);
        int armenemy = int.Parse(textArmorvm.text);

        DB();
        GetUserStats();

        Debug.Log(struser);
        Debug.Log(aguser);
        Debug.Log(speuser);
        Debug.Log(armuser);

        available = available - 1;

        string uid;
        //OBTIENE ID DE USUARIO
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        uid = textuserid.text.ToString();
        uid = "8xLUp3Df6tW4wOOQOICsmmUswiq1";
        reference.Child("users").Child(uid).Child("PVE").Child("available").SetValueAsync(available);
        Panelpreparativos.SetActive(false);
        Panelmision.SetActive(true);
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

