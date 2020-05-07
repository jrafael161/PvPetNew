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
public class Bestiario : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Getpets();
    }

    public void Getpets()
    {
        GameObject OponentProfile, OponentProfileAux;
        Text[] Texto;
        Image profileSprite;
        OponentProfile = GameObject.Find("OponentProfile");

        List<Pet> MissionPets = new List<Pet>();
        MissionPets = GlobalControl.Instance.petDBManager.PetDB.FindAll(x => x.Mision == 2);
        for (int i = 0; i < MissionPets.Count; i++)
        {
            OponentProfileAux = Instantiate(OponentProfile) as GameObject;
            OponentProfileAux.SetActive(true);
            OponentProfileAux.transform.SetParent(OponentProfile.transform.parent, false);
            profileSprite = OponentProfileAux.GetComponentInChildren<Image>();
            profileSprite.sprite = MissionPets[i].PetSprite;
            Texto = OponentProfileAux.GetComponentsInChildren<Text>();
            Texto[0].text = MissionPets[i].name;
            Texto[1].text = MissionPets[i].Pt.ToString();
        }
        Destroy(OponentProfile);
    }
}
