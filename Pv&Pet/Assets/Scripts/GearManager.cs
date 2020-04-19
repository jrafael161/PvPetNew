using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearManager : MonoBehaviour
{
    BodyZone GearSelect;
    public GlobalControl gc;
    public ItemsDBmanager itemDataBase = new ItemsDBmanager();
    GameObject Item;
    PlayerData Player;

    private void Start()
    {
        gc = FindObjectOfType<GlobalControl>();
    }

    public void InitializeInventory(GlobalControl gcReal)
    {        
        Player = gc.playeProfile;
    }

    public void SetSelectedInventory(GameObject dp)//Hace falta programar la funcion para esconder el dropdown cuando se deseleccione y que aparezca de nuevo la lista
    {
        dp.SetActive(true);
        List<string> m_DropOptions = new List<string>();
        Dropdown drp;
        drp = dp.GetComponentInChildren<Dropdown>();
        drp.ClearOptions();
        foreach (Item item in Player.Inventory)
        {
            if (item.Bz == GearSelect)
            {
                m_DropOptions.Add(item.name);
            }
        }
        drp.AddOptions(m_DropOptions);    
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
        Dropdown dp = drp.GetComponentInChildren<Dropdown>();
        Destroy(dp.GetComponent<Transform>().GetChild(3).gameObject);
        drp.gameObject.SetActive(false);        
    }

    public void HideInventory2(GameObject drp)
    {        
        drp.gameObject.SetActive(false);
    }

    public void deleteBlocker()
    {
        //Object.Destroy(GameObject.Find("Blocker"));
    }
    
}
