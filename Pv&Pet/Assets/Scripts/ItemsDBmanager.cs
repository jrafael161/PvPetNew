using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDBmanager
{
    private static ItemsDBmanager _instance;
    public static ItemsDBmanager Instance
    {
        get { return _instance; }
    }    
    
    public void Initialize()
    {
        _instance = this;
    }

    public List<Item> ItemDB = new List<Item>();

    public void Set_ItemDatabase()
    {        
        for (int i = 0; ItemDB.Count < 49; i++)
        {
            ItemDB.Add(Resources.Load("Items/LeatherHelmet") as Item);
            ItemDB.Add(Resources.Load("Items/LeatherVest") as Item);
            ItemDB.Add(Resources.Load("Items/LeatherGauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/LeatherBoots") as Item);
            ItemDB.Add(Resources.Load("Items/ShortSword") as Item);
            ItemDB.Add(Resources.Load("Items/Copper Helmet") as Item);
            ItemDB.Add(Resources.Load("Items/Copper Vest") as Item);
            ItemDB.Add(Resources.Load("Items/Copper Gauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/Copper Boots") as Item);
            ItemDB.Add(Resources.Load("Items/Copper Sword") as Item);
            ItemDB.Add(Resources.Load("Items/Bronze Helmet") as Item);
            ItemDB.Add(Resources.Load("Items/Bronze Vest") as Item);
            ItemDB.Add(Resources.Load("Items/Bronze Gauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/Bronze Boots") as Item);
            ItemDB.Add(Resources.Load("Items/Bronze Sword") as Item);
            ItemDB.Add(Resources.Load("Items/Iron Helmet") as Item);
            ItemDB.Add(Resources.Load("Items/Iron Vest") as Item);
            ItemDB.Add(Resources.Load("Items/Iron Gauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/Iron Boots") as Item);
            ItemDB.Add(Resources.Load("Items/Iron Sword") as Item);
            ItemDB.Add(Resources.Load("Items/Steel Helmet") as Item);
            ItemDB.Add(Resources.Load("Items/Steel Vest") as Item);
            ItemDB.Add(Resources.Load("Items/Steel Gauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/Steel Boots") as Item);
            ItemDB.Add(Resources.Load("Items/Steel Sword") as Item);
            ItemDB.Add(Resources.Load("Items/Gold Helmet") as Item);
            ItemDB.Add(Resources.Load("Items/Gold Vest") as Item);
            ItemDB.Add(Resources.Load("Items/Gold Gauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/Gold Boots") as Item);
            ItemDB.Add(Resources.Load("Items/Gold Sword") as Item);
            ItemDB.Add(Resources.Load("Items/Platinum Helmet") as Item);
            ItemDB.Add(Resources.Load("Items/Platinum Vest") as Item);
            ItemDB.Add(Resources.Load("Items/Platinum Gauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/Platinum Boots") as Item);
            ItemDB.Add(Resources.Load("Items/Platinum Sword") as Item);
            ItemDB.Add(Resources.Load("Items/Titanium Helmet") as Item);
            ItemDB.Add(Resources.Load("Items/Titanium Vest") as Item);
            ItemDB.Add(Resources.Load("Items/Titanium Gauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/Titanium Boots") as Item);
            ItemDB.Add(Resources.Load("Items/Titanium Sword") as Item);
            ItemDB.Add(Resources.Load("Items/Mithril Helmet") as Item);
            ItemDB.Add(Resources.Load("Items/Mithril Vest") as Item);
            ItemDB.Add(Resources.Load("Items/Mithril Gauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/Mithril Boots") as Item);
            ItemDB.Add(Resources.Load("Items/Mithril Sword") as Item);
            ItemDB.Add(Resources.Load("Items/Adamantite Helmet") as Item);
            ItemDB.Add(Resources.Load("Items/Adamantite Vest") as Item);
            ItemDB.Add(Resources.Load("Items/Adamantite Gauntlets") as Item);
            ItemDB.Add(Resources.Load("Items/Adamantite Boots") as Item);
            ItemDB.Add(Resources.Load("Items/Adamantite Sword") as Item);

        }
    }
}
