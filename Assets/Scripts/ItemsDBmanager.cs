using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDBmanager
{
    private static ItemsDBmanager _instance;
    string resourcesPath;

    public static ItemsDBmanager Instance
    {
        get { return _instance; }
    }    
    
    public void Initialize()
    {
        _instance = this;
#if UNITY_ANDROID
        resourcesPath = Application.dataPath + "Resources/";
#endif
        resourcesPath = "";
    }

    public List<Item> ItemDB = new List<Item>();

    public void Set_ItemDatabase()
    {       
            
            ItemDB.Add(Resources.Load(resourcesPath + "Items/LeatherHelmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/LeatherVest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/LeatherGauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/LeatherBoots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Weapon Slash") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/ShortSword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Copper Helmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Copper Vest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Copper Gauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Copper Boots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Copper Sword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Bronze Helmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Bronze Vest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Bronze Gauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Bronze Boots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Bronze Sword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Iron Helmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Iron Vest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Iron Gauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Iron Boots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Iron Sword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Steel Helmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Steel Vest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Steel Gauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Steel Boots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Steel Sword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Gold Helmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Gold Vest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Gold Gauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Gold Boots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Gold Sword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Platinum Helmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Platinum Vest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Platinum Gauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Platinum Boots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Platinum Sword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Titanium Helmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Titanium Vest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Titanium Gauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Titanium Boots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Titanium Sword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Mithril Helmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Mithril Vest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Mithril Gauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Mithril Boots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Mithril Sword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Adamantite Helmet") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Adamantite Vest") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Adamantite Gauntlets") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Adamantite Boots") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Adamantite Sword") as Item);
            ItemDB.Add(Resources.Load(resourcesPath + "Items/Pet Sword") as Item);
    }
}
