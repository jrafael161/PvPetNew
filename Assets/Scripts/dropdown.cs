using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dropdown : MonoBehaviour
{
    // Update is called once per frame    
    public GameObject chips;
    public static List<string> filters;

    void Start()
    {
        filters = new List<string>();
    }
    void Update()
    {
    }

    public void CreateChipFromDropdown(Dropdown change)//Al ejecutarse solo cuando el valor del dropdown cambia, si se selecciona de nuevo algo objeto que se acaba de seleccionar y se borra de los chips antes de seleccionar otro, este no se añadira a los chips hasta que se seleccione otro filtro.
    {
        if (!(filters.Contains(change.options[change.value].text)))
        {
            GameObject chipAux;
            Text Texto;
            chipAux = Instantiate(chips) as GameObject;
            chipAux.SetActive(true);
            chipAux.transform.SetParent(chips.transform.parent, true);
            Texto = chipAux.GetComponentInChildren<Text>();
            Texto.text = change.options[change.value].text;
            filters.Add(change.options[change.value].text);
        }
    }

    public void CreateChipFromText(string filter)//Al ejecutarse solo cuando el valor del dropdown cambia, si se selecciona de nuevo algo objeto que se acaba de seleccionar y se borra de los chips antes de seleccionar otro, este no se añadira a los chips hasta que se seleccione otro filtro.
    {
        if (!(filters.Contains(filter)))
        {
            GameObject chipAux;
            //float size; tamaño del chip a ajustar para que se muestre todo el texto
            Text Texto;
            chipAux = Instantiate(chips) as GameObject;
            chipAux.SetActive(true);
            chipAux.transform.SetParent(chips.transform.parent, true);
            /*
            do
            {
                //sumar o restar al tamaño hasta que se muestre todo el texto
            } while (CheckTextWidth());            
            */
            Texto = chipAux.GetComponentInChildren<Text>();
            Texto.text = filter;
            filters.Add(filter);
        }
    }

    public void RemoveFromFilters(GameObject chips)
    {
        Text texto;
        texto = chips.GetComponentInChildren<Text>();
        filters.Remove(texto.text);
    }

    public List<string> GetFilters()
    {
        return filters;
    }

    public void ClearFilterList()
    {
        filters.Clear();
    }

    bool CheckTextWidth()
    {
        //checar si el texto se muestra correctamente, de lo contrario modificar el tamaño
        //float preferredWidth = LayoutUtility.GetPreferredWidth(text.rectTransform);
        //float parentWidth = parentRect.rect.width;
        //return (preferredWidth > (parentWidth - longestCharWidth));
        //buscar una manera de poner estos chips hasta el ultimo en el grid para que no afecten a los que tienen un tamaño estandar.
        return true;
    }
}
