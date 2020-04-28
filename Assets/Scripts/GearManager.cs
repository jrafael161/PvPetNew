using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearManager : MonoBehaviour
{
    BodyZone GearSelect;
    GameObject Item;

    private void Start()
    {

    }

    public void SetSelectedInventory(GameObject dp)//Hace falta programar la funcion para esconder el dropdown cuando se deseleccione y que aparezca de nuevo la lista
    {
        int counter = 0;
        dp.SetActive(true);
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
                    drp.value = counter;
            }
            counter++;
        }
        drp.AddOptions(m_DropOptions);    
    }

    public void changeUserEquipedGear(string GearName)
    {

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
        GlobalControl.Instance.playeProfile.EquipedGear[(int)GearSelect] = GlobalControl.Instance.playeProfile.Inventory.Find(x => x.name == dp.options[dp.value].text);               
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
