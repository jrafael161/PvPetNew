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

public class PVP : MonoBehaviour
{
    DatabaseReference reference;
    public GameObject FigtherButton;

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

    void DB()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pvpet-f0b05.firebaseio.com/Players/Pa5UU16uCzt6X1E1DJ6a");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    void GetUserStats()
    {
        string uid;
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        uid = "4cx9WViw9RTIKjNyW9deBgkkn7y1";

        Text textUsername = GameObject.Find("Canvas/lbl_username").GetComponent<Text>();
        Text textHP = GameObject.Find("Canvas/lbl_pv").GetComponent<Text>();
        Text textAgility = GameObject.Find("Canvas/lbl_ag").GetComponent<Text>();
        Text textSpeed = GameObject.Find("Canvas/lbl_sp").GetComponent<Text>();
        Text textStrength = GameObject.Find("Canvas/lbl_str").GetComponent<Text>();
        Text textArmorv = GameObject.Find("Canvas/lbl_arm").GetComponent<Text>();



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
                Debug.Log("DataManager: Read scores");

                Dictionary<string, System.Object> attributes = (Dictionary<string, System.Object>)snapshot.Value;

                if (snapshot.Exists)
                {
                    Debug.Log("DataManager: UID ==> " + attributes["username"].ToString());
                    Debug.Log("DataManager: HP ==> " + attributes["HP"].ToString());
                    Debug.Log("DataManager: Agility ==> " + attributes["Agility"].ToString());
                    Debug.Log("DataManager: Speed ==> " + attributes["Speed"].ToString());
                    Debug.Log("DataManager: Strength ==> " + attributes["Strength"].ToString());
                    Debug.Log("DataManager: Armorv ==> " + attributes["Armorv"].ToString());

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
        MyAwesomeCreator();
    }
    void MyAwesomeCreator()
    {
        GameObject tButton = (GameObject)Instantiate(FigtherButton);
        Button buttonCtrl = tButton.GetComponent<Button>();
        buttonCtrl.onClick.AddListener(() => FooOnClick());
    }
    void FooOnClick()
    {
        Debug.Log("as");
    }
}
