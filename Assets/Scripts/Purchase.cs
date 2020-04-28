using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Purchase : MonoBehaviour
{
    GameObject Item;
    int Precio;
    int ItemID;
    string Description;
    int UsingCoin;

    private void Start()
    {

    }

    public void CheckCurrency()
    {
        switch (UsingCoin)
        {
            case 1:
                if (GlobalControl.Instance.playeProfile.PvPCoin >= Precio)//Checar con cual moneda se esta realizando la compra
                    AddItemToInventory();
                else
                    Debug.Log("Tas pobre shabo");                       
                break;
            case 2:
                if (GlobalControl.Instance.playeProfile.PetCoin >= Precio)
                    AddItemToInventory();                 
                else
                    Debug.Log("Tas pobre shabo");
                break;
            case 3:
                if (GlobalControl.Instance.playeProfile.PremiumCoin >= Precio)
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
        GlobalControl.Instance.playeProfile.Inventory.Add(ItemsDBmanager.Instance.ItemDB.Find(x => x.ItemID==ItemID));
        switch (UsingCoin)
        {
            case 1:
                GlobalControl.Instance.playeProfile.PvPCoin = GlobalControl.Instance.playeProfile.PvPCoin - Precio;
                break;
            case 2:
                GlobalControl.Instance.playeProfile.PetCoin = GlobalControl.Instance.playeProfile.PetCoin - Precio;
                break;
            case 3:
                GlobalControl.Instance.playeProfile.PremiumCoin = GlobalControl.Instance.playeProfile.PremiumCoin - Precio;
                break;
            default:
                break;
        }
        GlobalControl.Instance.InitializePlayerData();
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

    public void GetItemID(Text PurchasingItemID)
    {
        ItemID = int.Parse(PurchasingItemID.text);
    }
}
