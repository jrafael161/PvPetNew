using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

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
        for (int i = 0; i < GlobalControl.Instance.petDBManager.PetDB.Count; i++)
        {
            OponentProfileAux = Instantiate(OponentProfile) as GameObject;
            OponentProfileAux.SetActive(true);
            OponentProfileAux.transform.SetParent(OponentProfile.transform.parent, false);
            profileSprite = OponentProfileAux.GetComponentInChildren<Image>();
            profileSprite.sprite = GlobalControl.Instance.petDBManager.PetDB[i].PetSprite;
            Texto = OponentProfileAux.GetComponentsInChildren<Text>();
            Texto[0].text = GlobalControl.Instance.petDBManager.PetDB[i].name;
            Texto[1].text = GlobalControl.Instance.petDBManager.PetDB[i].Pt.ToString();
        }
        Destroy(OponentProfile);
    }
}
