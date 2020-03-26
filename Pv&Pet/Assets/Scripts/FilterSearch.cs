using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FilterSearch : MonoBehaviour
{
   
    public void SendKeyword(dropdown dp)
    {
        Text keyword = GetComponentInChildren<Text>();
        if(!(keyword.text==""))
            dp.CreateChipFromText(keyword.text);
    }

}
