using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
    public bool active;
    public List<string> filters;
    public List<ItemGObjectRelation> ShopItemRelationList;//Reemplazar por diccionario o no se necesita si se hacer por IDs
    public ItemGObjectRelation ShopItemRelation;
    public Button FiltersButton;    

    // Start is called before the first frame update
    void Start()
    {
        ShopItemRelationList = new List<ItemGObjectRelation>();
        filters = new List<string>();
        SetShopItems();
    }

    public void SetShopItems()//Inicializa los items de la tienda, no toma en cuenta filtros ya que es la primera vez que se instancian
    {
        GameObject ShopItem, ShopItemAux;
        Text[] Texto;
        Image[] img;

        ShopItem = GameObject.Find("ShopItem");
        foreach (Item item in ItemsDBmanager.Instance.ItemDB)
        {
            if (item.Name == "Pet Sword")
            {
                continue;
            }
            ShopItemAux = Instantiate(ShopItem) as GameObject;
            ShopItemAux.SetActive(true);
            ShopItemAux.transform.SetParent(ShopItem.transform.parent, false);
            img = ShopItemAux.GetComponentsInChildren<Image>();
            img[2].sprite = item.icon;
            Texto = ShopItemAux.GetComponentsInChildren<Text>();
            Texto[0].text = item.Description.ToString();
            Texto[1].text = item.PvP_Price.ToString();
            Texto[2].text = item.Pet_Price.ToString();
            Texto[3].text = item.Prem_Price.ToString();
            Texto[4].text = item.ItemID.ToString();
            ShopItemRelation = new ItemGObjectRelation(item, ShopItemAux);
            ShopItemRelationList.Add(ShopItemRelation);
        }
        Destroy(ShopItem);//Destruye el item original que sirve como plantilla para los demas items creados
    }

    public void FilteredShopItems()//Activa o desactiva los items de la tienda en base a los filtros que se tienen en este momento
    {
        List<ItemGObjectRelation> itemListAux = new List<ItemGObjectRelation>();
        itemListAux = ShopItemRelationList.FindAll(FindMatch);
        foreach (ItemGObjectRelation itemGObjectRelationAux in ShopItemRelationList)
        {
            itemGObjectRelationAux.itemGObject.SetActive(false);
        }
        foreach (ItemGObjectRelation itemGObjectAux in itemListAux)
        {
            itemGObjectAux.itemGObject.SetActive(true);
        }
    }

    public void ReactiveShopItems(dropdown dp)//Reactiva todos los items de la lista que guarda los items de la tienda
    {
        foreach (ItemGObjectRelation item in ShopItemRelationList)
        {
            item.itemGObject.SetActive(true);
        }
        dp.ClearFilterList();//Limpiando la lista que guarda los filtros seleccionados del dropdown
    }

    public void SetFilters(dropdown dp)//Se agregan a la lista de filtros los filtros seleccionados en el dropdown
    {
        filters = dp.GetFilters();
        FilteredShopItems();
    }

    public bool FindMatch(ItemGObjectRelation itemGObjectAux)//Funcion auxiliar para agregar filtros a la lista
    {
        foreach (string filter in filters)
        {
            if (itemGObjectAux.item.Bz.ToString() == filter || itemGObjectAux.item.It.ToString() == filter || itemGObjectAux.item.Name.Contains(filter.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

    public void SetShopFilters(dropdown dp)
    {
        SetFilters(dp);
    }

    public void DeleteFilters(dropdown dp)//Llama a limpiar filtros y destruye los chips instanciados
    {
        ReactiveShopItems(dp);
        GameObject[] chipcontainer = GameObject.FindGameObjectsWithTag("Chip");
        foreach (GameObject chip in chipcontainer)
        {
            Destroy(chip);
        }
    }
}

