using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShopFilters : MonoBehaviour
{
    public ShopControl sc;

    public void SetShopFilters(dropdown dp)
    {
        sc.SetFilters(dp);
    }

    public void DeleteFilters(dropdown dp)//Llama a limpiar filtros y destruye los chips instanciados
    {
        sc.ReactiveShopItems(dp);
        GameObject[] chipcontainer = GameObject.FindGameObjectsWithTag("Chip");
        foreach (GameObject chip in chipcontainer)
        {
            Destroy(chip);
        }
    }
}
