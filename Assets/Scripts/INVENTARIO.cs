using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class INVENTARIO : MonoBehaviour
{
    DatabaseReference reference;

    // Start is called before the first frame update
    void Start()
    {
        DB();
        GetUserStats();
    }

    void DB()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pvpet-f0b05.firebaseio.com/Players/Pa5UU16uCzt6X1E1DJ6a");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    void GetUserStats()
    {
        string uid = GlobalControl.Instance.playeProfile.PlayerID;
        //OBTIENE ID DE USUARIO
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        uid = textuserid.text.ToString();
        
        //stats
        Text textHP = GameObject.Find("Canvas/lbl_ph_v").GetComponent<Text>();
        Text textAgility = GameObject.Find("Canvas/lbl_ag_v").GetComponent<Text>();
        Text textSpeed = GameObject.Find("Canvas/lbl_sp_v").GetComponent<Text>();
        Text textStrength = GameObject.Find("Canvas/lbl_str_v").GetComponent<Text>();
        Text textArmorv = GameObject.Find("Canvas/lbl_arm_v").GetComponent<Text>();

        //equipo
        Text helmet = GameObject.Find("Canvas/lbl_item1").GetComponent<Text>();
        Text armor = GameObject.Find("Canvas/lbl_item2").GetComponent<Text>();
        Text foot = GameObject.Find("Canvas/lbl_item3").GetComponent<Text>();
        Text arm = GameObject.Find("Canvas/lbl_item4").GetComponent<Text>();
        Text weapon = GameObject.Find("Canvas/lbl_item5").GetComponent<Text>();
        Text shield = GameObject.Find("Canvas/lbl_item6").GetComponent<Text>();

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
                    Dictionary<string, System.Object> Inventory = (Dictionary<string, System.Object>)attributes["PVE"];
                    textHP.text = attributes["HP"].ToString();
                    textAgility.text = attributes["Agility"].ToString();
                    textSpeed.text = attributes["Speed"].ToString();
                    textStrength.text = attributes["Strength"].ToString();
                    textArmorv.text =  attributes["Armorv"].ToString();

                    helmet.text = attributes["HeadGear"].ToString();
                    armor.text = attributes["ChestGear"].ToString();
                    foot.text = attributes["FootsGear"].ToString();
                    arm.text = attributes["ArmsGear"].ToString();
                    weapon.text = attributes["Weapon"].ToString();
                    shield.text = attributes["Shield"].ToString();


                }
                else
                {
                    Debug.LogError("DataManager: Database for the user not available.");
                }
            }
        });
    }


}
