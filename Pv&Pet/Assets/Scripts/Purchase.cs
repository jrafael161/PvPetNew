using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Purchase : MonoBehaviour
{
    static GlobalControl gc;
    public ItemsDBmanager itemDataBase = new ItemsDBmanager();
    GameObject Item;
    PlayerData Player;
    int Precio;
    string Description;
    int UsingCoin;

    public static GlobalControl Gc { get => gc; set => gc = value; }

    private void Start()
    {
        Gc = FindObjectOfType<GlobalControl>();   
    }

    public void CheckCurrency(GlobalControl gcReal)
    {
        itemDataBase = Gc.itemDataBase;
        Player = Gc.playeProfile;
        switch (UsingCoin)
        {
            case 1:
                if (Player.PvPCoin >= Precio)//Checar con cual moneda se esta realizando la compra
                    AddItemToInventory();
                else
                    Debug.Log("Tas pobre shabo");                       
                break;
            case 2:
                if (Player.PetCoin >= Precio)
                    AddItemToInventory();                 
                else
                    Debug.Log("Tas pobre shabo");
                break;
            case 3:
                if (Player.PremiumCoin >= Precio)
                    AddItemToInventory();
                else
                    Debug.Log("Tas pobre shabo");
                break;
            default:
                Debug.Log("Cual moneda?");
                break;
        }
    }
    
    public void AddItemToInventory()
    {
        foreach (Item item in itemDataBase.ItemDB)
        {
            if (item.Description == Description)
            {
                Player.Inventory.Add(item);
                switch (UsingCoin)
                {
                    case 1:
                        Player.PvPCoin = Player.PvPCoin - Precio;
                        break;
                    case 2:
                        Player.PetCoin = Player.PetCoin - Precio;
                        break;
                    case 3:
                        Player.PremiumCoin = Player.PremiumCoin - Precio;
                        break;
                    default:
                        break;
                }
                break;
            }                
        }
        //Gc.SavePlayerData();
        Gc.InitializePlayerData();
    }

    public void GetCurrentCurrency(GameObject CoinType)
    {
        GameObject CoinTypeObject = CoinType;
        if (CoinTypeObject.name.ToString().Contains("PvP"))
        {
            UsingCoin = 1;
        }
        else if(CoinTypeObject.name.ToString().Contains("Pet"))
        {
            UsingCoin = 2;
        }
        else if (CoinTypeObject.name.ToString().Contains("Premium"))
        {
            UsingCoin = 3;
        }
        else
            Debug.Log("Cual moneda?");

        Text precio = CoinTypeObject.GetComponentInChildren<Text>();
        Precio = int.Parse(precio.text);
    }

    public void GetItemDescription(Text description)
    {
        Description = description.text;
    }
}
