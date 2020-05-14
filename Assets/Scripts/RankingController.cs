using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;
public class RankingController : MonoBehaviour
{
    public static Firebase.Auth.FirebaseAuth auth;
    public static Firebase.Auth.FirebaseUser user;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;

    [SerializeField]
    DatabaseReference reference;
    
    void Start()
    {
        DB();
        GetRankingList();
    }

    void DB()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pvpet-f0b05.firebaseio.com/Players/Pa5UU16uCzt6X1E1DJ6a");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void GetRankingList()
    {
        GameObject OponentProfile, OponentProfileAux;
        Text[] Texto;
        Image[] profileSprite;
        OponentProfile = GameObject.Find("OponentProfile");
        FirebaseDatabase.DefaultInstance.GetReference("Ranking").Child("top100").GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            Dictionary<string, System.Object> userids = (Dictionary<string, System.Object>)snapshot.Value;
            if (snapshot.Exists)
            {
                foreach (KeyValuePair<string, System.Object> users in userids)
                {
                    string Enemyid = users.Key;
                    Debug.Log("Enemyid:" + Enemyid);
                    FirebaseDatabase.DefaultInstance.GetReference("users").Child(Enemyid).GetValueAsync().ContinueWith(task2 =>
                    {
                        DataSnapshot snapshot2 = task2.Result;
                        Dictionary<string, System.Object> enemstats = (Dictionary<string, System.Object>)snapshot2.Value;
                        if (snapshot2.Exists)
                        {
                            OponentProfileAux = Instantiate(OponentProfile) as GameObject;
                            OponentProfileAux.SetActive(true);
                            OponentProfileAux.transform.SetParent(OponentProfile.transform.parent, false);
                            profileSprite = OponentProfileAux.GetComponentsInChildren<Image>();
                            profileSprite[0].enabled = true;
                            switch (enemstats["profilepic"].ToString())
                            {
                                case "Profile_1":
                                    profileSprite[1].sprite = sprite1;
                                    break;
                                case "Profile_2":
                                    profileSprite[1].sprite = sprite2;
                                    break;
                                case "Profile_3":
                                    profileSprite[1].sprite = sprite3;
                                    break;
                                case "Profile_4":
                                    profileSprite[1].sprite = sprite4;
                                    break;
                                default:
                                    break;
                            }
                            Texto = OponentProfileAux.GetComponentsInChildren<Text>();
                            Texto[0].text = enemstats["username"].ToString();
                            Texto[1].text = "Nivel:" + enemstats["Level"].ToString();
                            Texto[2].text = "HP:" + enemstats["HP"].ToString();
                            Texto[3].text = "STR:" + enemstats["Strength"].ToString();
                            Texto[4].text = "SPE:" + enemstats["Speed"].ToString();
                            Texto[5].text = "AGY:" + enemstats["Agility"].ToString();

                        }
                        Destroy(OponentProfile);

                    });
                }
            }
        }); 

    }
}
