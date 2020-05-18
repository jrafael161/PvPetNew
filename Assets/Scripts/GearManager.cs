using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearManager : MonoBehaviour
{
    BodyZone GearSelect;
    GameObject Item;
    Dropdown drpAux;
    PlayerResume playerResume;

    private void Start()
    {
        playerResume = FindObjectOfType<PlayerResume>();
    }

    public void SetSelectedInventory(GameObject dp)//Hace falta programar la funcion para esconder el dropdown cuando se deseleccione y que aparezca de nuevo la lista
    {
        int counter = 0;
        int slot=0;
        //dp.SetActive(true);
        List<string> m_DropOptions = new List<string>();
        Dropdown drp;
        drp = dp.GetComponentInChildren<Dropdown>();        
        drp.ClearOptions();
        foreach (Item item in GlobalControl.Instance.playeProfile.Inventory)
        {
            if (item.Bz == GearSelect)
            {                
                m_DropOptions.Add(item.name);
                if (GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect].name == item.name)
                {                    
                    slot = counter;
                }                                                        
                counter++;
            }            
        }
        drp.AddOptions(m_DropOptions);
        drp.value = slot;//Setear el valua hasta que ya se crearon las opciones del dropdown        
        dp.SetActive(true);
        drpAux = drp;
    }

    public void changeUserEquipedGear(Text itemName)
    {
        /*
        GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x=>x.Name == itemName.text);
        GlobalControl.Instance.playeProfile.EquipedGearIDs[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName.text).ItemID;
        GlobalControl.Instance.SavePlayerData();
        */
    }

    public void GearSelected(GameObject gear)
    {
        if (gear.name.ToLower().Contains("head"))
        {
            GearSelect = BodyZone.Head;
        }
        else if (gear.name.ToLower().Contains("chest"))
        {
            GearSelect = BodyZone.Chest;
        }
        else if (gear.name.ToLower().Contains("arms"))
        {
            GearSelect = BodyZone.Arms;
        }
        else if (gear.name.ToLower().Contains("foots"))
        {
            GearSelect = BodyZone.Foots;
        }
        else if (gear.name.ToLower().Contains("weapon"))
        {
            GearSelect = BodyZone.Weapon;
        }
    }

    public void HideInventory(GameObject drp)
    {
        if (!drp.activeSelf)
            return;
        CheckEquipedGear();
        Dropdown dp = drp.GetComponentInChildren<Dropdown>();        
        Destroy(dp.GetComponent<Transform>().GetChild(3).gameObject);        
        drp.gameObject.SetActive(false);
        playerResume.InitializePlayerData();
    }

    public void HideInventory2(GameObject drp)
    {
        drp.gameObject.SetActive(false);        
    }

    public void CheckEquipedGear()
    {
        if (drpAux == null)
            return;
        string itemName = drpAux.options[drpAux.value].text;
        switch (GearSelect)
        {            
            case BodyZone.Head:
                if (drpAux.options[drpAux.value].text != GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect].Name)
                {
                    GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName);
                    GlobalControl.Instance.playeProfile.EquipedGearIDs[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName).ItemID;
                    GlobalControl.Instance.SavePlayerData();
                }
                break;
            case BodyZone.Chest:
                if (drpAux.options[drpAux.value].text != GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect].Name)
                {
                    GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName);
                    GlobalControl.Instance.playeProfile.EquipedGearIDs[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName).ItemID;
                    GlobalControl.Instance.SavePlayerData();
                }
                break;
            case BodyZone.Arms:
                if (drpAux.options[drpAux.value].text != GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect].Name)
                {
                    GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName);
                    GlobalControl.Instance.playeProfile.EquipedGearIDs[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName).ItemID;
                    GlobalControl.Instance.SavePlayerData();
                }
                break;
            case BodyZone.Foots:
                if (drpAux.options[drpAux.value].text != GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect].Name)
                {
                    GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName);
                    GlobalControl.Instance.playeProfile.EquipedGearIDs[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName).ItemID;
                    GlobalControl.Instance.SavePlayerData();
                }
                break;
            case BodyZone.Weapon:
                if (drpAux.options[drpAux.value].text != GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect].Name)
                {
                    GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName);
                    GlobalControl.Instance.playeProfile.EquipedGearIDs[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.Name == itemName).ItemID;
                    GlobalControl.Instance.SavePlayerData();
                }
                break;
            default:
                break;
        }           
    }

    public void deleteBlocker()
    {
        //Object.Destroy(GameObject.Find("Blocker"));
    }
    
}
