using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_sheet : MonoBehaviour
{

    public GameObject Pn_Character_sheet;
    public ItemsDBmanager itemDataBase = new ItemsDBmanager();
        public PetDBManager petDBManager = new PetDBManager();

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

    //inventario y pet id
    public GameObject itemid;
    public GameObject petid;

    //inventario y pet listas
    public GameObject inventory;
    public GameObject ownpets;
    //equiped sprite
    public Sprite equiped;
    //equiped gear
    public GameObject equipedHelmet;
    public GameObject equipedBoots;
    public GameObject equipedWeapon;
    public GameObject equipedArm;
    public GameObject equipedChest;
    //equiped items
    public GameObject equipedItem1;
    public GameObject equipedItem2;
    public GameObject equipedItem3;
    public GameObject equipedItem4;
    public GameObject equipedItem5;
    public GameObject equipedItem6;
    //Detalles
    public GameObject detailsitems;
    public GameObject detailsipets;

    public GameObject detailsitemsname;
    public GameObject detailsitemstipo;
    public GameObject detailsitemsmod;
    public GameObject detailsitemsdesc;
    public GameObject detailsitemsimg;

    public GameObject detailsipetsname;
    public GameObject detailsipetsstr;
    public GameObject detailsipetsspe;
    public GameObject detailsipetsagy;
    public GameObject detailsipetsarm;
    public GameObject detailsipetshp;
    public GameObject detailsipetsimg;




    private void OpenSheet()
    {
        Animator animator = Pn_Character_sheet.GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("Open_character_sheet", true);
        Loadexp();
        Loadstats();
        Loadimg();
        LoadInventory();
        LoadPets();
        Loadgear();
        Loadequipeditems();
        Showinv();        
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
        if(puntos + 1 <= 0)
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
        if (puntos + 1 <= 0)
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
        if (puntos + 1 <= 0)
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
        
        foreach (Item item in GlobalControl.Instance.playeProfile.Inventory)
        {
            InventorytListAux = Instantiate(InventorytList) as GameObject;
            InventorytListAux.SetActive(true);
            InventorytListAux.transform.SetParent(InventorytList.transform.parent, false);
            profileSprite = InventorytListAux.GetComponentsInChildren<Image>();
            profileSprite[1].sprite = item.icon;
            if (!GlobalControl.Instance.playeProfile.EquipedGearIDs.Contains(item.ItemID))
            {
                if (!GlobalControl.Instance.playeProfile.EquipedItemsIDs.Contains(item.ItemID))
                {
                    profileSprite[2].enabled = false;
                }
            }
                
            Texto = InventorytListAux.GetComponentsInChildren<Text>();
            Texto[0].text = item.ItemID.ToString();
        }
        Destroy(InventorytList);
    }
    public void LoadPets()
    {
        GameObject InventorytList, InventorytListAux;
        Text[] Texto;
        Image[] profileSprite;
        InventorytList = GameObject.Find("Petinv");
        int counter = 0;
        foreach (Pet pet in GlobalControl.Instance.playeProfile.OwnedPets)
        {
            InventorytListAux = Instantiate(InventorytList) as GameObject;
            InventorytListAux.SetActive(true);
            InventorytListAux.transform.SetParent(InventorytList.transform.parent, false);
            profileSprite = InventorytListAux.GetComponentsInChildren<Image>();
            profileSprite[1].sprite = pet.PetSprite;
            if (GlobalControl.Instance.playeProfile.CompanionPetSlot != counter)
                profileSprite[2].enabled = false;
            
            //pet.PetID.ToString();
            Texto = InventorytListAux.GetComponentsInChildren<Text>();
            Texto[0].text = counter.ToString();
            counter++;
        }
        Destroy(InventorytList);
    }
    public void Loadgear()
    {
        equipedHelmet.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedGearIDs[0]).icon;
        equipedChest.GetComponent<Image>().sprite  = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedGearIDs[1]).icon;
        equipedArm.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedGearIDs[2]).icon;
        equipedBoots.GetComponent<Image>().sprite  = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedGearIDs[3]).icon;
        equipedWeapon.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedGearIDs[4]).icon;
    }
    public void Loadequipeditems()
    {
        if (0 < GlobalControl.Instance.playeProfile.EquipedItemsIDs.Count)
        {
            equipedItem1.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedItemsIDs[0]).icon;
        }
        if (1 < GlobalControl.Instance.playeProfile.EquipedItemsIDs.Count)
        {
            equipedItem2.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedItemsIDs[1]).icon;
        }
        if (2 < GlobalControl.Instance.playeProfile.EquipedItemsIDs.Count)
        {
            equipedItem3.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedItemsIDs[2]).icon;
        }
        if (3 < GlobalControl.Instance.playeProfile.EquipedItemsIDs.Count)
        {
            equipedItem4.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedItemsIDs[3]).icon;
        }
        if (4 < GlobalControl.Instance.playeProfile.EquipedItemsIDs.Count)
        {
            equipedItem5.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedItemsIDs[4]).icon;
        }
        if (5 < GlobalControl.Instance.playeProfile.EquipedItemsIDs.Count)
        {
            equipedItem6.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == GlobalControl.Instance.playeProfile.EquipedItemsIDs[5]).icon;
        }
    }
    public void Showitems()
    {
        string id = itemid.GetComponent<Text>().text.ToString();
        Debug.Log(id);
        detailsitems.SetActive(true);
        detailsitemsimg.GetComponent<Image>().sprite = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == int.Parse(id)).icon;
        detailsitemsname.GetComponent<TMPro.TextMeshProUGUI>().text = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == int.Parse(id)).Name;
        detailsitemstipo.GetComponent<TMPro.TextMeshProUGUI>().text = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == int.Parse(id)).It.ToString();
        detailsitemsmod.GetComponent<TMPro.TextMeshProUGUI>().text = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == int.Parse(id)).Value.ToString();
        detailsitemsdesc.GetComponent<TMPro.TextMeshProUGUI>().text = ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID == int.Parse(id)).Description;

    }

    public void Showipets()
    {
        int id = int.Parse(petid.GetComponent<Text>().text.ToString());
        detailsipets.SetActive(true);
        detailsipetsimg.GetComponent<Image>().sprite = GlobalControl.Instance.playeProfile.OwnedPets[id].PetSprite;
        detailsipetsstr.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.OwnedPets[id].Strength.ToString();
        detailsipetsspe.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.OwnedPets[id].Speed.ToString();
        detailsipetsagy.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.OwnedPets[id].Agility.ToString();
        detailsipetsarm.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.OwnedPets[id].Armor.ToString();
        detailsipetshp.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalControl.Instance.playeProfile.OwnedPets[id].HP.ToString();
    }

    public void Showinv()
    {
        inventory.SetActive(true);
        ownpets.SetActive(false);
    }
    public void Showpet()
    {
        inventory.SetActive(false);
        ownpets.SetActive(true);
    }
    
    public void Closeitems()
    {
        detailsitems.SetActive(false);
    }
    public void Closepet()
    {
        detailsipets.SetActive(false);
    }





}
