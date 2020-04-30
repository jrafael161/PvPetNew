using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDBmanager
{
    public List<Item> ItemDB=new List<Item>();
    
    public void Set_ShopInventory()
    {

    }

    public void Set_ItemDatabase()
    {        
        for (int i = 0; ItemDB.Count < 6; i++)
        {
            ItemDB.Add(Resources.Load("Items/LeatherHelmet") as Item);
            ItemDB.Add(Resources.Load("Items/LeatherVest") as Item);
            ItemDB.Add(Resources.Load("Items/LeatherGauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/LeatherPants") as Item);
            ItemDB.Add(Resources.Load("Items/LeatherBoots") as Item);
            ItemDB.Add(Resources.Load("Items/ShortSword") as Item);
        }
    }
}
