using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShopFilters : MonoBehaviour
{
    public GlobalControl gc;
    public ShopControl sc;

    public void SetShopFilters(dropdown dp)
    {
        //gc.SetFilters(dp);
        sc.SetFilters(dp);
    }

    public void setgc(GlobalControl gcReal)
    {
        gc = gcReal.get_Instance();
    }

    public void DeleteFilters(dropdown dp)//Llama a limpiar filtros y destruye los chips instanciados
    {
        //gc.ReactiveShopItems(dp);
        sc.ReactiveShopItems(dp);
        GameObject[] chipcontainer = GameObject.FindGameObjectsWithTag("Chip");
        foreach (GameObject chip in chipcontainer)
        {
            Destroy(chip);
        }
    }
}
