using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_sheet : MonoBehaviour
{

    public GameObject Pn_Character_sheet;
    //Exp bar
    public Slider expbar;
    //IMG de personaje
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public GameObject imgprofile;
    //puntos de habilidad
    public GameObject puntoshabilidad;
    //HP
    public GameObject HP;
    //STR
    public GameObject STR;
    //SPE
    public GameObject SPE;
    //AGY
    public GameObject AGY;

    //aumentar stats
    public GameObject STRinc;
    public GameObject STRdec;
    public GameObject SPEinc;
    public GameObject SPEdec;
    public GameObject AGYinc;
    public GameObject AGYdec;

    //inventario
    public GameObject itemid;


    private void OpenSheet()
    {
        Animator animator = Pn_Character_sheet.GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("Open_character_sheet", true);

        if (true)
        {
            Loadexp();
            Loadstats();
            Loadimg();
            LoadInventory();
        }
    }
    private void CloseSheet()
    {
        Animator animator = Pn_Character_sheet.GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("Open_character_sheet", false);
    }
    private void Loadexp()
    {
        expbar.value = float.Parse(GlobalControl.Instance.playeProfile.XP.ToString());
        Debug.Log(GlobalControl.Instance.playeProfile.XP.ToString());
    }
    private void Loadimg()
    {
        Debug.Log(GlobalControl.Instance.playeProfile.SpriteName.ToString());
        if (GlobalControl.Instance.playeProfile.SpriteName.ToString() == "Profile_1")
            imgprofile.GetComponent<Image>().sprite = sprite1;
        if (GlobalControl.Instance.playeProfile.SpriteName.ToString() == "Profile_2")
            imgprofile.GetComponent<Image>().sprite = sprite2;
        if (GlobalControl.Instance.playeProfile.SpriteName.ToString() == "Profile_3")
            imgprofile.GetComponent<Image>().sprite = sprite3;
        if (GlobalControl.Instance.playeProfile.SpriteName.ToString() == "Profile_4")
            imgprofile.GetComponent<Image>().sprite = sprite4;
    }
    private void Loadstats()
    {
        puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.LevelUpPoints.ToString();
        HP.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.HP.ToString();
        STR.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.Strength.ToString();
        SPE.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.Speed.ToString();
        AGY.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.Agility.ToString();

        if (int.Parse(GlobalControl.Instance.playeProfile.LevelUpPoints.ToString()) > 0) 
        {
            STRinc.SetActive(true);
            STRdec.SetActive(true);
            SPEinc.SetActive(true);
            SPEdec.SetActive(true);
            AGYinc.SetActive(true);
            AGYdec.SetActive(true);
        }
        else
        {
            STRinc.SetActive(false);
            STRdec.SetActive(false);
            SPEinc.SetActive(false);
            SPEdec.SetActive(false);
            AGYinc.SetActive(false);
            AGYdec.SetActive(false);
        }
    }




    public void A_STRinc()
    {
        int puntos = int.Parse(puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text);
        if(puntos - 1 > -1)
        {
            puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text = (puntos - 1).ToString();
            int puntosstr = int.Parse(STR.GetComponent<TMPro.TextMeshProUGUI>().text);
            STR.GetComponent<TMPro.TextMeshProUGUI>().text = (puntosstr + 1).ToString();
        }
    }
    public void A_STRdec()
    {
        int puntos = int.Parse(puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text);
        if(puntos + 1 <= 50)
        {
            puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text = (puntos + 1).ToString();
            int puntosstr = int.Parse(STR.GetComponent<TMPro.TextMeshProUGUI>().text);
            STR.GetComponent<TMPro.TextMeshProUGUI>().text = (puntosstr - 1).ToString();
        }
    }
    public void A_SPEinc()
    {
        int puntos = int.Parse(puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text);
        if (puntos - 1 > -1)
        {
            puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text = (puntos - 1).ToString();
            int puntosspe = int.Parse(SPE.GetComponent<TMPro.TextMeshProUGUI>().text);
            SPE.GetComponent<TMPro.TextMeshProUGUI>().text = (puntosspe + 1).ToString();
        }
    }
    public void A_SPEdec()
    {
        int puntos = int.Parse(puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text);
        if (puntos + 1 <= 50)
        {
            puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text = (puntos + 1).ToString();
            int puntosspe = int.Parse(SPE.GetComponent<TMPro.TextMeshProUGUI>().text);
            SPE.GetComponent<TMPro.TextMeshProUGUI>().text = (puntosspe - 1).ToString();
        }
    }
    public void A_AGYinc()
    {
        int puntos = int.Parse(puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text);
        if (puntos - 1 > -1)
        {
            puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text = (puntos - 1).ToString();
            int puntosagy = int.Parse(AGY.GetComponent<TMPro.TextMeshProUGUI>().text);
            AGY.GetComponent<TMPro.TextMeshProUGUI>().text = (puntosagy + 1).ToString();
        }
    }
    public void A_AGYdec()
    {
        int puntos = int.Parse(puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text);
        if (puntos + 1 <= 50)
        {
            puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text = (puntos + 1).ToString();
            int puntosagy = int.Parse(AGY.GetComponent<TMPro.TextMeshProUGUI>().text);
            AGY.GetComponent<TMPro.TextMeshProUGUI>().text = (puntosagy - 1).ToString();
        }
    }
    public void Savestats()
    {
        GlobalControl.Instance.playeProfile.LevelUpPoints = int.Parse(puntoshabilidad.GetComponent<TMPro.TextMeshProUGUI>().text);
        GlobalControl.Instance.playeProfile.HP = int.Parse(HP.GetComponent<TMPro.TextMeshProUGUI>().text);
        GlobalControl.Instance.playeProfile.Strength = int.Parse(STR.GetComponent<TMPro.TextMeshProUGUI>().text);
        GlobalControl.Instance.playeProfile.Speed = int.Parse(SPE.GetComponent<TMPro.TextMeshProUGUI>().text);
        GlobalControl.Instance.playeProfile.Agility = int.Parse(AGY.GetComponent<TMPro.TextMeshProUGUI>().text);
    }
    public void LoadInventory()
    {
        GameObject InventorytList, InventorytListAux;
        Text[] Texto;
        Image[] profileSprite;
        InventorytList = GameObject.Find("Iteminv");
        for (int i = 0; i < GlobalControl.Instance.petDBManager.PetDB.Count; i++)
        {
            InventorytListAux = Instantiate(InventorytList) as GameObject;
            InventorytListAux.SetActive(true);
            InventorytListAux.transform.SetParent(InventorytList.transform.parent, false);
            profileSprite = InventorytListAux.GetComponentsInChildren<Image>();
            profileSprite[1].sprite = GlobalControl.Instance.petDBManager.PetDB[i].PetSprite;
            Texto = InventorytListAux.GetComponentsInChildren<Text>();
            Texto[0].text = "lalaland";
        }
        Destroy(InventorytList);
    }
    public void LoadPets()
    {
        GameObject InventorytList, InventorytListAux;
        Text[] Texto;
        Image[] profileSprite;
        InventorytList = GameObject.Find("Iteminv");
        for (int i = 0; i < GlobalControl.Instance.petDBManager.PetDB.Count; i++)
        {
            InventorytListAux = Instantiate(InventorytList) as GameObject;
            InventorytListAux.SetActive(true);
            InventorytListAux.transform.SetParent(InventorytList.transform.parent, false);
            profileSprite = InventorytListAux.GetComponentsInChildren<Image>();
            profileSprite[1].sprite = GlobalControl.Instance.petDBManager.PetDB[i].PetSprite;
            Texto = InventorytListAux.GetComponentsInChildren<Text>();
            Texto[0].text = "lalaland";
        }
        Destroy(InventorytList);
    }
    public void Showitem()
    {
        string id = itemid.GetComponent<Text>().text.ToString();
        Debug.Log(id);
        Debug.Log("asdadasd");
    }


}
